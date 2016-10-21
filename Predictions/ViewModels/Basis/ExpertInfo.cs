using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels.Basis
{
    public class ExpertInfo
    {
        public ExpertInfo()
        { }

        public ExpertInfo(string nickname, int sum, int score, int difference, int outcome)
        {
            Nickname = nickname;
            Sum = sum;
            Scores = score;
            Differences = difference;
            Outcomes = outcome;
        }

        public string  Nickname { get; set; }

        public int Sum { get; set; }
        public int Scores { get; set; }
        public int Differences { get; set; }
        public int Outcomes { get; set; }
    }
}