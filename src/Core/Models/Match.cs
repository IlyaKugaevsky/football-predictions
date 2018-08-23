using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Models.Dtos;

namespace Core.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public string Title { get; set; }
        public string Score { get; set; } = string.Empty;

        public int? PlayoffWinner { get; set; } = null;

        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public virtual List<Prediction> Predictions { get; set; }

        public Match()
        { }

        public Match(DateTime date, Team homeTeam, Team awayTeam, Tour tour)
        {
            Date = date;
            HomeTeam = homeTeam ?? throw new ArgumentNullException("HomeTeam");
            AwayTeam = awayTeam ?? throw new ArgumentNullException("AwayTeam");
            Tour = tour;
            Score = string.Empty;
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
            Score = string.Empty;
        }

        public Match(DateTime date, int homeTeamId, int awayTeamId, int tourId)
        {
            Date = date;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            TourId = tourId;
            Score = string.Empty;
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
                throw new ArgumentNullException("HomeTeam cannot be null");

            if (AwayTeam == null)
                throw new ArgumentNullException("AwayTeam cannot be null");

            return new MatchDto(Date, HomeTeam.GetDto(), AwayTeam.GetDto());
        }

        public FootballScoreViewModel GetFootballScoreViewModel(bool editable, string emptyDisplay)
        {
            return new Dtos.FootballScoreViewModel
            {
                Score = (string.IsNullOrEmpty(Score) && editable == false) ? emptyDisplay : Score,
                Editable = editable
            };
        }

        public FootballScore GetFootballScore()
        {
            return new FootballScore(Score, PlayoffWinner);
        }

        public int GetPredictionsSum()
        {
            if (Predictions == null) throw new NullReferenceException("Predictions");
            return Predictions.Select(p => p.Sum).Sum();
        }
    }
}