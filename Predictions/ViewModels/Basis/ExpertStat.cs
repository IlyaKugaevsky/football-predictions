using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;

namespace Predictions.ViewModels.Basis
{
    public class ExpertStat
    {
        public ExpertStat(string nickname, int predictionsCount, double avgSum, double sum)
        {
            Nickname = nickname;
            PredictionsCount = predictionsCount;
            AvgSum = avgSum;
            Sum = sum;
        }

        public string Nickname  { get; set; }
        public int PredictionsCount { get; set; }
        public double AvgSum { get; set; }
        public double Sum { get; set; }
    }
}