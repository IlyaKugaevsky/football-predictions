using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Persistence.DAL.EntityFrameworkExtensions;
using Persistence;
using Core.Models;
using Core.Models.Dtos;
using System.Data.Entity;


namespace Persistence.Tests
{
    public class ExpertExtensionsTests
    {
        private IEnumerable<Expert> CreateCollection()
        {
            var p1 = new Prediction(1, 1, "1:0");
            var p2 = new Prediction(2, 2, "0:5");

            var e1 = new Expert()
            {
                ExpertId = 1,
                Nickname = "Spiderman",
                Predictions = new List<Prediction>() { p1 },
            };

            var e2 = new Expert()
            {
                ExpertId = 2,
                Nickname = "Batman",
                Predictions = new List<Prediction>() { p2 },
            };

            return new List<Expert>() {e1, e2};
        }

        //[Fact]
        //public Should_Fetch_Predictions()
        //{
        //    var experts = CreateCollection().AsQueryable();

        //    var dbSetMock = new Mock<DbSet<Expert>>();

        //    dbSetMock.As<IQueryable<Expert>>().Setup(m => m.Provider).Returns(experts.Provider);
        //    dbSetMock.As<IQueryable<Expert>>().Setup(m => m.Expression).Returns(experts.Expression);
        //    dbSetMock.As<IQueryable<Expert>>().Setup(m => m.ElementType).Returns(experts.ElementType);
        //    dbSetMock.As<IQueryable<Expert>>().Setup(m => m.GetEnumerator()).Returns(experts.GetEnumerator());

        //    var mockContext = new Mock<PredictionsContext>();
        //    mockContext.Setup(c => c.Experts).Returns(dbSetMock.Object);

        //    var fetchStrategies = new IFetchStrategy<Expert>[] { new ExpertsFetchPredictions() };


        //}
    }
}
