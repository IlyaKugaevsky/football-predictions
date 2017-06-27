﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Predictions.Models;

namespace Predictions.DAL.FetchStrategies.TourFetchStrategies
{
    public class Matches : IFetchStrategy<Tour>
    {
        public Expression<Func<Tour, object>> Apply()
        {
            return t => t.Matches;
        }
    }
}