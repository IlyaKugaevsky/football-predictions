using System;
using Predictions.ViewModels.Basis;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.Helpers
{
    public static class PredictionEvaluator
    {
        public static int GetHomeGoals(string expression)
        {
            return Convert.ToInt32(expression.Substring(0, expression.IndexOf(':')));
        }

        public static int GetAwayGoals(string expression)
        {
            return Convert.ToInt32(expression.Substring(expression.IndexOf(':') + 1, expression.Length  - expression.IndexOf(':') - 1));
        }

        public static int GetDifference(string expression)
        {
            return GetHomeGoals(expression) - GetAwayGoals(expression);
        }

        public static bool PredictOutcome(string prediction, string score)
        {
            if ((GetDifference(prediction) * GetDifference(score) > 0) || (GetDifference(prediction) == 0 && GetDifference(score) == 0)) return true;
            return false; 
        }

        public static bool PredictDifference(string prediction, string score)
        {
            if (GetDifference(prediction) == GetDifference(score)) return true;
            return false;
        }

        public static bool PredictScore(string prediction, string score)
        {
            if ((GetHomeGoals(prediction) == GetHomeGoals(score)) && (GetAwayGoals(prediction) == GetAwayGoals(score))) return true;
            return false;
        }


        public static PredictionResults GetPredictionResults(string prediction, string score) //sum, score, difference, outcome
        {
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

            //return Tuple.Create(sum, predictScore, predictDifference, predictOutcome);
        }
    }
}