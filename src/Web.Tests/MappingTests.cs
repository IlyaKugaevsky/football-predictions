using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Models.Dtos;
using Xunit;
using FootballMatch = Core.Models.Match;

namespace Web.Tests
{
    public class MappingTests
    {
        [Fact]
        void MatchToMatchDto_Should_Map_Correcly()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<FootballMatch, MatchDto>();
                cfg.CreateMap<Team, TeamDto>();
            });
            config.AssertConfigurationIsValid();

            var team1 = new Team() { TeamId = 1, Title = "Spartak" };
            var team2 = new Team() { TeamId = 2, Title = "CSKA" };
            var match = new Match() { Date = DateTime.MinValue, HomeTeam = team1, AwayTeam = team2, MatchId = 1};

            var mapper = config.CreateMapper();
            var matchDto = mapper.Map<Match, MatchDto>(match);

            Assert.Equal(matchDto.Date, match.Date);
            Assert.Equal(matchDto.HomeTeam.Title, match.HomeTeam.Title);
            Assert.Equal(matchDto.HomeTeam.TeamId, match.HomeTeam.TeamId);
            Assert.Equal(matchDto.AwayTeam.TeamId, match.AwayTeam.TeamId);
        }
    }
}
