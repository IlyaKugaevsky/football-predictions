using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class HeadToHeadMatch
    {
        public int HeadToHeadMatchId { get; set; }


        public int HomeExpertId { get; set; }
        public int AwayExpertId { get; set; }

        [ForeignKey("HomeExpertId")]
        public Expert HomeExpert { get; set; }

        [ForeignKey("AwayExpertId")]
        public Expert AwayExpert { get; set; }

        public byte HomeGoals { get; set; }
        public byte AwayGoals { get; set; }

        public bool IsOver { get; set; }
    }
}
