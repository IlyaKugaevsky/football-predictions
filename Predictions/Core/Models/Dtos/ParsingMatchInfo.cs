using System;

namespace Predictions.Core.Models.Dtos
{
    public class ParsingMatchInfo
    {
        public ParsingMatchInfo(DateTime date, string homeTeamTitle, string awayTeamTitle)
        {
            Date = date;
            HomeTeamTitle = homeTeamTitle;
            AwayTeamTitle = awayTeamTitle;
        }

        public DateTime Date { get; }
        public string HomeTeamTitle { get; }
        public string AwayTeamTitle { get; }
    }
}