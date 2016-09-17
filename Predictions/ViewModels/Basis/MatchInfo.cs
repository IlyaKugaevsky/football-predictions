using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models;
using System.ComponentModel.DataAnnotations;

namespace Predictions.ViewModels
{
    public class MatchInfo
    {
        public MatchInfo()
        { }

        public MatchInfo(DateTime date, string homeTitle, string awayTitle)
        {
            Date = date;
            HomeTeamTitle = homeTitle;
            AwayTeamTitle = awayTitle;
        }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime Date { get; set; }

        [Required]
        public string HomeTeamTitle { get; set; }

        [Required]
        public string AwayTeamTitle { get; set; }
    }
}