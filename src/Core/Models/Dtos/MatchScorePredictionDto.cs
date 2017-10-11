using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos
{
    public class MatchScorePredictionDto
    {
        public MatchDto Match { get; }
        public FootballScore Score { get; }
        public FootballScore PredictionValue { get; }

        public MatchScorePredictionDto(MatchDto match, FootballScore score, FootballScore predictionValue)
        {
            Match = match;
            Score = score;
            PredictionValue = predictionValue;
        }
    }
}
