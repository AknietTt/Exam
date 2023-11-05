using Exam.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam.Infrastructure.Configs.Common;

public abstract class BaseEntityConfig<TDomain> : IEntityTypeConfiguration<TDomain> where TDomain : Entity
{
    protected abstract void Config(EntityTypeBuilder<TDomain> builder);

    public virtual void Configure(EntityTypeBuilder<TDomain> builder)
    {
        builder.HasKey(x => x.Id);
        Config(builder);
    }
}
