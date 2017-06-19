using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predictions.Models.Dtos;

namespace Predictions.Models
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