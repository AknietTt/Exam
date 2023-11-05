using Exam.Domain.SeedWork;

namespace Exam.Domain.Entities;

public class UserEntity: Entity, IAggregateRoot{
    public string Email{ get; set; }
    public string Password { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
}