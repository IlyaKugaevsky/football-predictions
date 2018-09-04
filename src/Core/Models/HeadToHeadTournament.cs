using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HeadToHeadTournament
    {
        public int HeadToHeadTournamentId { get; set; }

        public int ParentTournamentId { get; set; }
        public Tournament ParentTournament { get; set; }

        public virtual List<HeadToHeadTour> Tours { get; set; }
    }
}
