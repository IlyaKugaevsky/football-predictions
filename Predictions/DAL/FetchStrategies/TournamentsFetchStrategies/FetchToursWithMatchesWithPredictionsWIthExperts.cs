﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Core.Models;

namespace Predictions.DAL.FetchStrategies.TournamentsFetchStrategies
{
    public class FetchToursWithMatchesWithPredictionsWithExperts : IFetchStrategy<Tournament>
    {
        public Expression<Func<Tournament, object>> Apply()
        {
            return t => t.NewTours.
                Select(tr => tr.Matches
                    .Select(m => m.Predictions
                        .Select(p => p.Expert)));
        }
    }
}