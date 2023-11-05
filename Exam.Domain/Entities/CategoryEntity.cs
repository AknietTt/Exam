using Exam.Domain.SeedWork;

namespace Exam.Domain.Entities;

public class CategoryEntity:Entity , IAggregateRoot{
    public string Name{ get; set; }
    public string Flag{ get; set; }
    public string? Alpha{ get; set; }
    public List<LeagueEntity> Leagues{ get; set; }
}