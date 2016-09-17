using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Predictions.Models;

namespace Predictions.Models
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TourId { get; set; }
        public bool IsClosed { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]

        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = false)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM.dd.yyyy HH:mm}")]
        [Column(TypeName = "DateTime2")]
        public DateTime EndDate { get; set; }
        public virtual List<Match> Matches { get; set; }
    }
}