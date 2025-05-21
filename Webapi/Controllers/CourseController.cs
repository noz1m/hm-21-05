using Domain.ApiResponse;
using Domain.Entities;
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
