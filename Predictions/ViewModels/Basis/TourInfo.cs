using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels.Basis
{
    public class TourInfo
    {
        public TourInfo()
        { }

        public TourInfo(int id, int number, DateTime startDate, DateTime endDate)
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