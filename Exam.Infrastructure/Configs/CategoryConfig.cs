using Exam.Domain.Entities;
using Exam.Infrastructure.Configs.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Configs;

public class CategoryConfig:BaseEntityConfig<CategoryEntity>{
    protected override void Config(EntityTypeBuilder<CategoryEntity> builder){
        builder.ToTable("Categories", schema: "public");
        
    }
}