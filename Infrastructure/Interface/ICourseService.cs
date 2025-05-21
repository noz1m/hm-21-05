using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface ICourseService
{
    public Task<Response<List<Course>>> GetAllCoursesAsync();
    public Task<Response<Course>> GetCourseByIdAsync(int id);
    public Task<Response<string>> CreateCourseAsync(Course course);
    public Task<Response<string>> UpdateCourseAsync(Course course);
    public Task<Response<string>> DeleteCourseAsync(int id);
}
