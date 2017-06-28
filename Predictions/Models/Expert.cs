using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.Models
{
    public class Expert
    {
        public int ExpertId { get; set; }
        public string Nickname { get; set; }

        public int Outcomes { get; set; }
        public int Differences { get; set; }
        public int Scores { get; set; }
        public int Sum { get; set; }

        public virtual List<Prediction> Predictions { get; set; }

        public int GetPredictionsSum()
        {
            if (Predictions == null) throw new NullReferenceException(Nickname + " predictions");
            return Predictions.Select(p => p.Sum).Sum();
        }
    }
}