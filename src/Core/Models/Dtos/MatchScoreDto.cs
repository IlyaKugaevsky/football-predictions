using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos
{
    public class MatchScoreDto
    {
        public MatchDto Match { get; }
        public FootballScore Score { get; }

        public MatchScoreDto(MatchDto match, FootballScore score)
        {
            Match = match;
            Score = score;
        }

    }
}
