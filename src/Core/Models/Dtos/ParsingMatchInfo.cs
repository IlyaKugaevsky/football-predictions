using System;

namespace Core.Models.Dtos
{
    public class ParsingMatchInfo
    {
        public ParsingMatchInfo(DateTime date, string homeTeamTitle, string awayTeamTitle, int? playoffWinner = null)
        {
            Date = date;
            HomeTeamTitle = homeTeamTitle;
            AwayTeamTitle = awayTeamTitle;
            PlayoffWinner = playoffWinner;
        }

        public DateTime Date { get; }
        public string HomeTeamTitle { get; }
        public string AwayTeamTitle { get; }
        public int? PlayoffWinner { get; }
    }
}