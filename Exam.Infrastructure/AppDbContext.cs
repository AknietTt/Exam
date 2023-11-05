using Exam.Domain.Entities;
using Exam.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infrastructure;

public class AppDbContext : DbContext{
    public DbSet<CategoryEntity> Categories{ get; set; }
    public DbSet<LeagueEntity> Leagues{ get; set; }
    public DbSet<TeamEntity> Teams{ get; set; }
    public DbSet<UserEntity> Users{ get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new LeagueConfig());
        modelBuilder.ApplyConfiguration(new TeamConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}