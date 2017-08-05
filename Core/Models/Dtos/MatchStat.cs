namespace Core.Models.Dtos
{
    public class MatchStat
    {
        public MatchStat(string homeTeamTitle, string awayTeamTitle, double averageSum, int differentPredictions)
        {
            HomeTeamTitle = homeTeamTitle;
            AwayTeamTitle = awayTeamTitle;
            AverageSum = averageSum;
            DifferentPredictions = differentPredictions;
        }
        public string HomeTeamTitle { get; set; }
        public string AwayTeamTitle { get; set; }
        public double AverageSum { get; set; }
        public int DifferentPredictions { get; set; }
    }
}