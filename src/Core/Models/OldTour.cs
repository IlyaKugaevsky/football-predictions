using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Dtos;

namespace Core.Models
{
    public class OldTour
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OldTourId { get; set; }

        //public Tournament Tournament { get; set; }
        //public int TournamentId { get; set; }

        public int OldTourNumber { get; set; }

        public bool IsClosed { get; set; }
        public bool IsPlayoff { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Column(TypeName = "DateTime2")]
        public DateTime EndDate { get; set; }

        //public virtual List<Match> Matches { get; set; }


        public TourDto GetTourDto()
        {
            return new TourDto(OldTourId, OldTourNumber, StartDate, EndDate, IsPlayoff);
        }
    }
}