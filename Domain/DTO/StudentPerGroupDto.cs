namespace Domain.DTO;

public class StudentPerGroupDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public int StudentId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
