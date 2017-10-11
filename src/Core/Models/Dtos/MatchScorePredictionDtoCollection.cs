using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;

namespace Core.Models.Dtos
{
    public class MatchScorePredictionDtoCollection
    {
        public IReadOnlyCollection<MatchScorePredictionDto> MatchScorePredictionCollection { get;  }

        public MatchScorePredictionDtoCollection(IList<MatchDto> matches,
            IList<FootballScore> scores, IList<FootballScore> predictions)
        {
            if (matches.Count != scores.Count || scores.Count != predictions.Count)
            {
                throw new ArgumentException("Collections must be of equal size.");
            }

            var matchScorePredictionCollection = new List<MatchScorePredictionDto>();
            for (var i = 0; i < matches.Count; i++)
            {
                matchScorePredictionCollection.Add(new MatchScorePredictionDto(matches[i], scores[i], predictions[i]));
            }

            MatchScorePredictionCollection = matchScorePredictionCollection;
        }
    }
}
