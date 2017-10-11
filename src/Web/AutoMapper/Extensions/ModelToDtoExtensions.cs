using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Core.Models;
using Core.Models.Dtos;

namespace Web.AutoMapper.Extensions
{
    public static class ModelToDtoExtensions
    {
        public static IEnumerable<MatchDto> ToDtos(this IEnumerable<Match> matches, IMapper mapper) =>
            mapper.Map<IEnumerable<Match>, IEnumerable<MatchDto>>(matches);
    }
}