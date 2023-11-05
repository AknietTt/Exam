using AutoMapper;

using BCrypt.Net;

using Exam.Domain.Entities;
using Exam.Infrastructure.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Mapping {
    public class UserMapping: Profile {
        public UserMapping()
        {
            CreateMap<AuthDto , UserEntity>()
                .ForMember(x=>x.Password , opt => opt.MapFrom(src=> BCrypt.Net.BCrypt.HashPassword(src.Password)));
            CreateMap<RegisterDto, UserEntity>();

            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();



        }
    }
}
