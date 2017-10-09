using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Moq;
using Persistence.DAL;
using Services.Services;
using Services.Helpers;
using Xunit;
using MatchModel = Core.Models.Match; // because of 'Match' class in Xunit

namespace Services.Tests
{
    public class MatchServiceTests
    {
        private readonly List<Tour> _toursTestData;
        private readonly List<MatchModel> _mathesTestData;
        private readonly List<Team> _teamsTestData;

        private readonly Mock<DbSet<MatchModel>> _matchesMockSet;
        private readonly Mock<DbSet<Tour>> _toursMockSet;
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
            mockContext.Setup(c => c.Matches).Returns(_matchesMockSet.Object);
            mockContext.Setup(c => c.Tours).Returns(_toursMockSet.Object);
            return mockContext;
        }

        public MatchServiceTests()
        {
            var team1 = new Team() { TeamId = 1, Title = "Spartak" };
            var team2 = new Team() { TeamId = 2, Title = "CSKA" };

            var match1 = new MatchModel() { MatchId = 1, HomeTeam = team1, AwayTeam = team2, TourId = 1 };
            var match2 = new MatchModel() { MatchId = 2, HomeTeam = team2, AwayTeam = team1, TourId = 1 };
            var match3 = new MatchModel() { MatchId = 3, HomeTeam = team2, AwayTeam = team1, TourId = 2 };

            var tour1 = new Tour()
            {
                TourId = 1,
                Matches = new List<MatchModel>() { match1, match2 }
            };
            var tour2 = new Tour()
            {
                TourId = 2, 
                Matches = new List<MatchModel>() { match3 }
            };

            var tournament1 = new Tournament()
            {
                TournamentId = 1,
            };
            var tournament2 = new Tournament()
            {
                TournamentId = 2,
                NewTours = new List<Tour>() { tour1, tour2 }
            };

            _teamsTestData = new List<Team>() { team1, team2 };
            _mathesTestData = new List<MatchModel>() { match1, match2, match3 };
            _toursTestData = new List<Tour>() { tour1, tour2 };

            _matchesMockSet = CreateMockSet<MatchModel>(_mathesTestData);
            _toursMockSet = CreateMockSet<Tour>(_toursTestData);
            _mockContext = CreateMockContext();
        }

        [Fact]
        public void GetMatchesByTour_Should_Return_Proper_Collection()
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



    }
}
