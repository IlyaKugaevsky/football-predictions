using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HeadToHeadTour
    {
        public int HeadToHeadTourId { get; set; }


        public int ParentTourId { get; set; }
        public Tour ParentTour { get; set; }

        public virtual List<HeadToHeadMatch> Matches { get; set; }
    }
}
