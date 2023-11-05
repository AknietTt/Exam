using Exam.Domain.SeedWork;

namespace Exam.Domain.Entities;

public class LeagueEntity: Entity , IAggregateRoot{
    public string Name{ get; set; }
    public int UserCount{ get; set; }
    public CategoryEntity Category{ get; set; }
    public List<TeamEntity> Teams{ get; set; }
}