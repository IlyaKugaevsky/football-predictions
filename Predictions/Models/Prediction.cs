using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.Models
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        public string Value { get; set; }

        public int Sum { get; set; } = 0;
        public bool Score { get; set; } = false;
        public bool Difference { get; set; } = false;
        public bool Outcome { get; set; } = false;

        public bool IsClosed { get; set; } = false;

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}