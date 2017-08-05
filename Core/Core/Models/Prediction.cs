namespace Core.Core.Models
{
    public class Prediction
    {
        public Prediction()
        { }

        public Prediction(int expertId, int matchId, string value)
        {
            ExpertId = expertId;
            MatchId = matchId;
            Value = value;
        }

        public int PredictionId { get; set; }

        public string Value { get; set; }

        public int Sum { get; set; } = 0;
        public bool Score { get; set; } = false;
        public bool Difference { get; set; } = false;
        public bool Outcome { get; set; } = false;

        public bool IsClosed { get; set; } = false;

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }
    }
}