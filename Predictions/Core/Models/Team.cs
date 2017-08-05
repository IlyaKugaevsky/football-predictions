using Predictions.Core.Models.Dtos;

namespace Predictions.Core.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Title { get; set; }

        public TeamDto GetDto()
        {
            return new TeamDto(TeamId, Title);
        }
    }
}