using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Persistence.DAL.FetchStrategies;

namespace Services.Tests
{
    public class TournamentServiceTests
    {
        private Tournament _tournament;

        public TournamentServiceTests()
        {
            var tournament = new Tournament() {Title = "World Cup", TournamentId = 1};
            var tours = new List<Tour>()
            {
                new Tour(tornamentId: 1, tourNumber: 1),
                new Tour(tornamentId: 1, tourNumber: 2)
            };
            tournament.NewTours = tours;

            _tournament = tournament;
        }  
    }
}
