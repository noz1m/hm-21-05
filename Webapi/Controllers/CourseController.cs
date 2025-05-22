using Domain.ApiResponse;
using Domain.Entities;
using Domain.DTO;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseService courseService)
{
    [HttpGet]
    public async Task<Response<List<Course>>> GetAllCoursesAsync()
    {
        return await courseService.GetAllCoursesAsync();
    }
    [HttpGet("{id}")]
    public async Task<Response<Course>> GetCourseByIdAsync(int id)
    {
        return await courseService.GetCourseByIdAsync(id);
    }
    [HttpGet("students-per-course")]
    public async Task<Response<List<StudentsPerCourseDto>>> GetStudentsPerCourse()
    {
        return await courseService.GetStudentsPerCourse();
    }
    [HttpGet("get-popular-courses")]
    public async Task<Response<List<PopularCourseDto>>> GetMostPopularCourse()
    {
        return await courseService.GetMostPopularCourse();
    }
    [HttpGet("get-least-popular-courses")]
    public async Task<Response<List<LeastPopularCourseDto>>> GetLeastPopularCourses()
    {
        return await courseService.GetLeastPopularCourses();
    }
    [HttpGet("get-top-three-courses")]
    public async Task<Response<List<PopularCourseDto>>> GetTopThreeCourses()
    {
        return await courseService.GetTopThreeCourses();
    }
    [HttpPost]
    public async Task<Response<string>> CreateCourseAsync(Course course)
    {
        return await courseService.CreateCourseAsync(course);
    }
    [HttpPut()]
    public async Task<Response<string>> UpdateCourseAsync(Course course)
    {
        return await courseService.UpdateCourseAsync(course);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteCourseAsync(int id)
    {
        return await courseService.DeleteCourseAsync(id);
    }
}
