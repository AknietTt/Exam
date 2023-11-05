using Exam.Domain.Entities;
using Exam.Infrastructure.Configs.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Configs {
    public class UserConfig : BaseEntityConfig<UserEntity> {
        protected override void Config(EntityTypeBuilder<UserEntity> builder) {
            builder.ToTable("Users", schema: "public");
        }
    }
}
