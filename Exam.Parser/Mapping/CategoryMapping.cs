using AutoMapper;

using Exam.Domain.Entities;
using Exam.Parser.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser.Mapping {
    public class CategoryMapping:Profile {
        public CategoryMapping(){
            CreateMap<CategoryRefDto, CategoryEntity>()
                .ForMember(x => x.SystemId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Alpha, opt => opt.MapFrom(x => x.Alpha2));
        }
    }
}
