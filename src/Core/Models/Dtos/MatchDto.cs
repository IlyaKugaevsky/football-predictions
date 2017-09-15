using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Dtos
{
    public class MatchDto
    {
        public MatchDto()
        { }

        public MatchDto(DateTime date, TeamDto homeTeam, TeamDto awayTeam)
        {
            Date = date;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            //HomeTeamTitle = homeTitle;
            //AwayTeamTitle = awayTitle;
        }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        public DateTime Date { get; set; }

        //[Required]
        //public string HomeTeamTitle { get; set; }

        public TeamDto HomeTeam { get; set; }

        //[Required]
        //public string AwayTeamTitle { get; set; }
        public TeamDto AwayTeam { get; set; }
    }
}