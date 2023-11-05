using Exam.Domain.SeedWork;

namespace Exam.Domain.Entities;

public class TeamEntity:Entity , IAggregateRoot{
    public string Name{ get; set; }
    public string NameCode{ get; set; }
    public LeagueEntity League{ get; set; }
    
}