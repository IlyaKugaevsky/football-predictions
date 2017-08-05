using System.Collections.Generic;

namespace Predictions.Core.Models.Dtos
{
    public class TopStats
    {
        public TopStats(List<MatchDto> mostPredictable, List<MatchDto> leastPredictbale, List<MatchDto> mostPredictions, List<MatchDto> leastPredictions)
        {
            MostPredicatble = mostPredictable;
            LeastPredicatble = leastPredictbale;
            MostPredictions = mostPredictable;
            LeastPredictions = leastPredictions;
        }

        public List<MatchDto> MostPredicatble { get; private set; }
        public List<MatchDto> LeastPredicatble { get; private set; }
        public List<MatchDto> MostPredictions { get; private set; }
        public List<MatchDto> LeastPredictions { get; private set; }

        //public Prediction MostPopularPredictionValue { get; set; }
        //public Prediction MostSuccessfulPredictionValue { get; set; }
    }
}