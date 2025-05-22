namespace Domain.DTO;

public class LeastPopularCourseDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; }
    public int StudentCount { get; set; }
}
