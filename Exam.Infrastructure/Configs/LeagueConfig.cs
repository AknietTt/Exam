using Exam.Domain.Entities;
using Exam.Infrastructure.Configs.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Configs;
public class LeagueConfig : BaseEntityConfig<LeagueEntity> {
    protected override void Config(EntityTypeBuilder<LeagueEntity> builder) {
        builder.ToTable("Leagues", schema: "public");
    }
}

