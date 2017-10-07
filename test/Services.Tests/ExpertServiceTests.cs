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
        private readonly IQueryable<Expert> _experts;

        public ExpertServiceTests()
        {
            _experts = new List<Expert>()
            {
                new Expert() {ExpertId = 1, Nickname = "Spiderman"},
                new Expert() {ExpertId = 2, Nickname = "Batman"}
            }
            .AsQueryable();
        }

        [Fact]
        public void Should_Fetch_All_Experts()
        {

            var mockSet = new Mock<DbSet<Expert>>();

            mockSet.As<IQueryable<Expert>>().Setup(m => m.Provider).Returns(_experts.Provider);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.Expression).Returns(_experts.Expression);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.ElementType).Returns(_experts.ElementType);
            mockSet.As<IQueryable<Expert>>().Setup(m => m.GetEnumerator()).Returns(_experts.GetEnumerator());

            var mockContext = new Mock<PredictionsContext>();
            mockContext.Setup(c => c.Experts).Returns(mockSet.Object);

            var expertService = new ExpertService(mockContext.Object);
            var fetchedExperts = expertService.GetExperts();

            Assert.Equal(1, fetchedExperts[0].ExpertId);
            Assert.Equal(2, fetchedExperts[1].ExpertId);
            Assert.Equal("Spiderman", fetchedExperts[0].Nickname);
            Assert.Equal("Batman", fetchedExperts[1].Nickname);
        }

    }
}
