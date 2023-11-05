namespace Exam.Domain.SeedWork;

public class Entity{
    public Guid Id { get; } = Guid.NewGuid();
    public long SystemId { get; set; }
}