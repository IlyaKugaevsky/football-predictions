using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.QueryExtensions
{
    public static class StringExtensions
    {
        public static string GetMeanScore(this IEnumerable<string> scores)
        {
            var homeSum = 0;
            var awaySum = 0;
            var scoreList = scores.ToList();

            foreach (var score in scoreList)
            {
                if (!PredictionEvaluator.IsValidScore(score)) throw new ArgumentException("Not a valid score");
                homeSum += PredictionEvaluator.GetHomeGoals(score);
                awaySum += PredictionEvaluator.GetAwayGoals(score);
            }

            var homeMean = (double)homeSum / scoreList.Count;
            var awayMean = (double)awaySum / scoreList.Count;

            return Math.Round(homeMean) + ":" + Math.Round(awayMean);
        }
    }
}
