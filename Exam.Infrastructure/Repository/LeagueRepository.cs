﻿using AutoMapper;

using Exam.Domain.Entities;
using Exam.Domain.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Repository {
    public class LeagueRepository:BaseRepository<LeagueEntity> , ILeagueRepository {
        public LeagueRepository(AppDbContext context, IMapper mapper) : base(context, mapper) {
        }
    }
}
