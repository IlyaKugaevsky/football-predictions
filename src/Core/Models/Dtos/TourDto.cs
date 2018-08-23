using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Dtos
{
    public class TourDto
    {
        public TourDto()
        { }

        public TourDto(int id, int number, DateTime startDate, DateTime endDate, bool isPlayoff)
        {
            TourId = id;
            TourNumber = number;
            StartDate = startDate;
            EndDate = endDate;
            IsPlayoff = isPlayoff;
        }

        public int TourId { get; set; }
        public int TourNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime EndDate { get; set; }

        public bool IsPlayoff { get; set; }
    }
}