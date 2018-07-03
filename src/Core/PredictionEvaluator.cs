using System;
using System.Text.RegularExpressions;
using Core.Models;
using Core.Models.Dtos;

namespace Core
{
    public static class PredictionEvaluator
    {
        public static readonly string Pattern = @"^$|^[0-9]{1,2}:[0-9]{1,2}$";

        public static bool IsValidScore(string input)
        {
            var rgx = new Regex(Pattern);
            return rgx.IsMatch(input);
        }

        public static int GetHomeGoals(string expression) => 
            Convert.ToInt32(expression.Substring(0, expression.IndexOf(':')));

        public static int GetAwayGoals(string expression) => 
            Convert.ToInt32(expression.Substring(expression.IndexOf(':') + 1, expression.Length  - expression.IndexOf(':') - 1));

        public static int GetDifference(string expression) => 
            GetHomeGoals(expression) - GetAwayGoals(expression);

        public static bool PredictOutcome(string prediction, string score)
        {
            if ((GetDifference(prediction) * GetDifference(score) > 0) || (GetDifference(prediction) == 0 && GetDifference(score) == 0)) return true;
            return false; 
        }

        public static bool PredictDifference(string prediction, string score)
        {
            return GetDifference(prediction) == GetDifference(score);
        }

        public static bool PredictScore(string prediction, string score)
        {
            if ((GetHomeGoals(prediction) == GetHomeGoals(score)) && (GetAwayGoals(prediction) == GetAwayGoals(score))) return true;
            return false;
        }


        public static PredictionResults GetPredictionResults(string prediction, string score) //sum, score, difference, outcome
        {
            if (string.IsNullOrEmpty(prediction))
            {
                return new PredictionResults
                {
                    Sum = 0,
                    Score = false,
                    Difference = false,
                    Outcome = false
                };
            }

            var predictOutcome = PredictOutcome(prediction, score);
            var predictDifference = PredictDifference(prediction, score);
            var predictScore = PredictScore(prediction, score);

            var sum = 0;
            if (predictOutcome) sum++;
            if (predictDifference) sum++;
            if (predictScore) sum++;

            return new PredictionResults
            {
                Sum = sum,
                Score = predictScore,
                Difference = predictDifference,
                Outcome = predictOutcome
            };
        }
    }
}