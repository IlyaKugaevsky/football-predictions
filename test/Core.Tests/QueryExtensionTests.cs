using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.QueryExtensions;
using Xunit;

namespace Core.Tests
{
    public class QueryExtensionTests
    {
        [Theory]
        [InlineData("1:0", "1:0", "1:0", "1:0")]
        [InlineData("1:0", "2:0", "3:0", "2:0")]
        [InlineData("2:1", "2:1", "1:1", "2:1")]
        public void Should_calculate_maen_correctly(string score1, string score2, string score3, string expected)
        {
            var scorelist = new List<string>() { score1, score2, score3 };

            var calculated = scorelist.GetMeanScore();

            Assert.Equal(expected, calculated);
        }
    }
}
