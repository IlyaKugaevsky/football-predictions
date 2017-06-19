namespace Predictions.Models.Dtos
{
    public class TeamDto
    {
        public TeamDto()
        { }

        public TeamDto(int teamId, string title)
        {
            TeamId = teamId;
            Title = title;
        }

        public int TeamId { get; set; }
        public string Title { get; set; }

    }
}