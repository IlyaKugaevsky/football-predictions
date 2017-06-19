using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using Predictions.Models.Dtos;
using Predictions.ViewModels.Basis;

namespace Predictions.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public string Title { get; set; }
        public string Score { get; set; } = "-";

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd.MM.yyyy | HH:mm}")]
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        //public int TourId { get; set; }
        //public Tour Tour { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public virtual List<Prediction> Predictions { get; set; }

        public Match()
        { }

        public Match(DateTime date, Team homeTeam, Team awayTeam, Tour tour)
        {
            if (homeTeam == null)
                throw new ArgumentNullException("HomeTeam");

            if (awayTeam == null)
                throw new ArgumentNullException("AwayTeam");

            Date = date;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Tour = tour;
            Score = String.Empty;
        }

        public Match(DateTime date, Team homeTeam, Team awayTeam, int tourId)
        {
            if (homeTeam == null)
                throw new ArgumentNullException("HomeTeam");

            if (awayTeam == null)
                throw new ArgumentNullException("AwayTeam");

            Date = date;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            TourId = tourId;
            Score = String.Empty;
        }

        public Match(DateTime date, int homeTeamId, int awayTeamId, int tourId)
        {
            Date = date;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            TourId = tourId;
            Score = String.Empty;
        }

        //public Match(MatchInfo matchInfo)
        //{
        //    Date = matchInfo.Date;
        //    HomeTeamId = matchInfo.;
        //    AwayTeamId = awayTeamId;
        //    TourId = tourId;
        //    Score = String.Empty;
        //}

        public MatchDto GetDto()
        {
            if (HomeTeam == null)
                throw new ArgumentNullException("HomeTeam");

            if (AwayTeam == null)
                throw new ArgumentNullException("AwayTeam");

            return new MatchDto(Date, HomeTeam.GetDto(), AwayTeam.GetDto());
        }

        public FootballScore GetFootballScore(bool editable, string emptyDisplay)
        {
            return new FootballScore
            {
                Value = (String.IsNullOrEmpty(Score) && editable == false) ? emptyDisplay : Score,
                Editable = editable
            };
        }
    }
}