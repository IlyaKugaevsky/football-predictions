using System;
using System.ComponentModel.DataAnnotations;

namespace Predictions.Models.Dtos
{
    public class NewTourDto
    {
        public NewTourDto()
        { }

        public NewTourDto(int id, int number, DateTime startDate, DateTime endDate)
        {
            TourId = id;
            TourNumber = number;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int TourId { get; set; }
        public int TourNumber { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime StartDate { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime EndDate { get; set; }
    }
}