using AutoMapper;

using Exam.Domain.Entities;
using Exam.Parser.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser.Mapping {
    public class LeagueMapping:Profile {
        public LeagueMapping()
        {
            CreateMap<LeagueRefDto, LeagueEntity>()
                .ForMember(x => x.SystemId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
