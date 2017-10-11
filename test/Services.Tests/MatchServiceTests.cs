using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Core.QueryExtensions;
using Moq;
using Persistence.DAL;
using Services.Services;
using Xunit;
using FootballMatch = Core.Models.Match; // because of 'Match' class in Xunit

namespace Services.Tests
{
    public class MatchServiceTests
    {
        private readonly List<Tournament> _tournamentsTestData;
        private readonly List<Tour> _toursTestData;
        private readonly List<FootballMatch> _mathesTestData;
        private readonly List<Team> _teamsTestData;

        private readonly Mock<DbSet<Tournament>> _tournamentsMockSet;
        private readonly Mock<DbSet<Tour>> _toursMockSet;
        private readonly Mock<DbSet<FootballMatch>> _matchesMockSet;
        private readonly Mock<DbSet<Team>> _teamsMockSet;

        private readonly Mock<PredictionsContext> _mockContext;

        private Mock<DbSet<T>> CreateMockSet<T> (ICollection<T> collection) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(collection.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(collection.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(collection.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(collection.AsQueryable().GetEnumerator());

            return mockSet;
        }

        private Mock<PredictionsContext> CreateMockContext()
        {
            var mockContext = new Mock<PredictionsContext>();

            _toursMockSet.Setup(t => t.Include(It.IsAny<string>())).Returns(_toursMockSet.Object);
            _tournamentsMockSet.Setup(t => t.Include(It.IsAny<string>())).Returns(_tournamentsMockSet.Object);

            mockContext.Setup(c => c.Matches).Returns(_matchesMockSet.Object);
            mockContext.Setup(c => c.Tours).Returns(_toursMockSet.Object);
            mockContext.Setup(c => c.Tournaments).Returns(_tournamentsMockSet.Object);

            return mockContext;
        }

        public MatchServiceTests()
        {
            var team1 = new Team() { TeamId = 1, Title = "Spartak" };
            var team2 = new Team() { TeamId = 2, Title = "CSKA" };

            var match1 = new FootballMatch() { MatchId = 1, HomeTeam = team1, AwayTeam = team2, TourId = 1 };
            var match2 = new FootballMatch() { MatchId = 2, HomeTeam = team2, AwayTeam = team1, TourId = 1 };
            var match3 = new FootballMatch() { MatchId = 3, HomeTeam = team2, AwayTeam = team1, TourId = 2 };

            var tour1 = new Tour()
            {
                TourId = 1,
                Matches = new List<FootballMatch>() { match1, match2 }
            };
            var tour2 = new Tour()
            {
                TourId = 2, 
                Matches = new List<FootballMatch>() { match3 }
            };

            var tournament1 = new Tournament()
            {
                TournamentId = 1,
                NewTours = new List<Tour>() { tour1 }
            };
            var tournament2 = new Tournament()
            {
                TournamentId = 2,
                NewTours = new List<Tour>() { tour1, tour2 }
            };

            _teamsTestData = new List<Team>() { team1, team2 };
            _mathesTestData = new List<FootballMatch>() { match1, match2, match3 };
            _toursTestData = new List<Tour>() { tour1, tour2 };
            _tournamentsTestData = new List<Tournament>() { tournament1, tournament2 };

            _teamsMockSet = CreateMockSet<Team>(_teamsTestData);
            _matchesMockSet = CreateMockSet<FootballMatch>(_mathesTestData);
            _toursMockSet = CreateMockSet<Tour>(_toursTestData);
            _tournamentsMockSet = CreateMockSet<Tournament>(_tournamentsTestData);

            _mockContext = CreateMockContext();
        }

        [Fact]
        public void GetMatchesByTourId_Should_Return_Proper_Collection()
        {
            const int tourId = 1;
            var matches = _toursTestData.TourById(tourId).Matches;
            var matchService = new MatchService(_mockContext.Object);

            var fetchedMatches = matchService.GetMatchesByTourId(tourId);

            Assert.Equal(matches, fetchedMatches);
        }

        [Fact]
        public void GetTourSchedule_Should_Return_Proper_Collection()
        {
            const int tourId = 1;
            var matches = _toursTestData.TourById(tourId).Matches;
            var matchService = new MatchService(_mockContext.Object);

            var fetchedMatches = matchService.GetTourSchedule(tourId);
            var allHaveBothTeams = fetchedMatches.AllHaveBothTeams();

            Assert.Equal(matches, fetchedMatches);
            Assert.True(allHaveBothTeams);
        }

        [Fact]
        public void MatchesCount_Should_Count_Correctly()
        {
            const int tourId = 1;
            var matchService = new MatchService(_mockContext.Object);

            var matchNumberFromService = matchService.MatchesCount(tourId);
            var matchNumber = _toursTestData.TourById(tourId).Matches.Count;

            Assert.Equal(matchNumber, matchNumberFromService);
        }

        [Theory]
        [InlineData("1:1", "1:0", true)]
        [InlineData("", "1:0", false)]
        [InlineData("", "", false)]
        [InlineData("1:1", "", false)]
        public void AllMatchResultsPopulated_Should_Return_True_Only_If_All_Scores_Not_NullOrEmpty(string score1, string score2, bool expected)
        {
            const int tourId = 1;
            _mathesTestData.MatchById(1).Score = score1;
            _mathesTestData.MatchById(2).Score = score2;
            var matchService = new MatchService(_mockContext.Object);

            var allMatchResultsPopulated = matchService.AllMatchScoresPopulated(tourId);

            Assert.Equal(expected, allMatchResultsPopulated);
        }

        [Fact]
        public void GenerateMatchlist_Should_Return_Proper_Collection()
        {
            const int tourId = 1;
            const int tournamentId = 2;
            var matchDtos = _tournamentsTestData.TournamentById(2).NewTours.TourById(1).Matches.ToList();
            var matchService = new MatchService(_mockContext.Object);

            var fetchedMatchDtos = matchService.GenerateMatchlist(tournamentId, tourId);

            Assert.Equal(matchDtos.First().HomeTeam.TeamId, fetchedMatchDtos.First().HomeTeam.TeamId);
            Assert.Equal(matchDtos.Last().HomeTeam.TeamId, fetchedMatchDtos.Last().HomeTeam.TeamId);
        }

        [Fact]
        public void GenerateScorelist_Should_Return_Proper_Collection()
        {
            const int tourId = 1;
            const int tournamentId = 2;
            var matchDtos = _tournamentsTestData.TournamentById(2).NewTours.TourById(1).Matches.ToList();
            var matchService = new MatchService(_mockContext.Object);

            var fetchedMatchDtos = matchService.GenerateMatchlist(tournamentId, tourId);

            Assert.Equal(matchDtos.First().HomeTeam.TeamId, fetchedMatchDtos.First().HomeTeam.TeamId);
            Assert.Equal(matchDtos.Last().HomeTeam.TeamId, fetchedMatchDtos.Last().HomeTeam.TeamId);
        }




    }
}
