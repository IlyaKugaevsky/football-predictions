using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Moq;
using Xunit;
using Services.Services;
using System.Data.Entity;
using Persistence.DAL;

namespace Services.Tests
{
    public class ExpertServiceTests
    {
        private readonly List<Expert> _testData;
        private readonly Mock<DbSet<Expert>> _mockSet;
        private readonly Mock<PredictionsContext> _mockContext;

        private List<Expert> CreateTestData()
        {
            return new List<Expert>()
            {
                new Expert() {ExpertId = 1, Nickname = "Spiderman"},
                new Expert() {ExpertId = 2, Nickname = "Batman"}
            };
        }

        private Mock<DbSet<Expert>> CreateMockSet()
        {
            var mockSet = new Mock<DbSet<Expert>>();

            mockSet.As<IQueryable<Expert>>().Setup(m => m.Provider).Returns(_testData.AsQueryable().Provider);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.Expression).Returns(_testData.AsQueryable().Expression);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.ElementType).Returns(_testData.AsQueryable().ElementType);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.GetEnumerator()).Returns(_testData.AsQueryable().GetEnumerator());

            return mockSet;
        }


        private Mock<PredictionsContext> CreateMockContext()
        {
            var mockContext = new Mock<PredictionsContext>();
            mockContext.Setup(c => c.Experts).Returns(_mockSet.Object);
            return mockContext;
        }


        public ExpertServiceTests()
        {
            _testData = CreateTestData();
            _mockSet = CreateMockSet();
            _mockContext = CreateMockContext();
        }

        [Fact]
        public void GetExperts_Should_Fetch_All_Experts()
        {
            var expertService = new ExpertService(_mockContext.Object);
            var fetchedExperts = expertService.GetExperts();

            Assert.Equal(_testData, fetchedExperts);
        }

    }
}
