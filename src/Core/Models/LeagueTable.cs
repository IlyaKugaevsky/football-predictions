using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class LeagueTableLine
    {
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Looses { get; set; }
        public int ScoredGoals { get; set; }
        public int ConcededGoals { get; set; }
    }
}
