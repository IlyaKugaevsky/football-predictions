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
        public MatchInfo(DateTime date, string homeTitle, string awayTitle, string predictionValue = null, string score = null)
        {
            Date = date;
            HomeTeamTitle = homeTitle;
            AwayTeamTitle = awayTitle;
            PredictionValue = predictionValue;
            Score = score;
        }

        public int Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy/HH:mm}")]
        public DateTime Date { get; set; }

        [Required]
        public string HomeTeamTitle { get; set; }

        [Required]
        public string AwayTeamTitle { get; set; }

        [RegularExpression(@"^$|^[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Некорректный счет")]
        public string PredictionValue { get; set; }

        [Required]
        [RegularExpression(@"^$|^[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Некорректный счет")]
        public string Score { get; set; }
    }
}