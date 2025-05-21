using Domain.Enum;
namespace Domain.Entities;

public class StudentGroup
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public Status Status { get; set; }
}
