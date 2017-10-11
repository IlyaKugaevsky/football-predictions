using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Core.Models;
using Core.Models.Dtos;

namespace Web.AutoMapper.Profiles
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Match, MatchDto>();
            CreateMap<Team, TeamDto>();
        }
    }
}