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

        public TourInfo(int id, DateTime startDate, DateTime endDate)
        {
            TourId = id;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int TourId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy | HH:mm}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy | HH:mm}")]
        public DateTime EndDate { get; set; }
    }
}