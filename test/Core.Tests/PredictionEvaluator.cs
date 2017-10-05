using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests
{
    public class PredictionEvaluatorTests
    {
        [Fact]
        public void First()
        {
            var x = 4;
            var y = 2 + 2;
            Assert.Equal(x, y);
        }

        [Theory]
        [InlineData("1:0", 1)]
        [InlineData("10:0", 10)]
        public void Should_Return_Home_Goals_Correctly(string score, int homeGoals)
        {
            Assert.Equal(PredictionEvaluator.GetHomeGoals(score), homeGoals);
        }
    }
}
