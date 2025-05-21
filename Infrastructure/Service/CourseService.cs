using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
namespace Infrastructure.Service;

public class CourseService(DataContext context) : ICourseService
{
    public async Task<Response<List<Course>>> GetAllCoursesAsync()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from courses";
        var result = await connection.QueryAsync<Course>(sql);
        return result == null
            ? new Response<List<Course>>("Courses not found", HttpStatusCode.NotFound)
            : new Response<List<Course>>(result.ToList(), "Courses found");
    }
    public async Task<Response<Course>> GetCourseByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "select * from courses where id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<Course>(sql, new { id });
        return result == null
            ? new Response<Course>("Courses not found", HttpStatusCode.NotFound)
            : new Response<Course>(result, "Courses found");
    }
    public async Task<Response<string>> CreateCourseAsync(Course course)
    {
        using var connection = await context.GetConnection();
        var sql = "insert into courses (Name, Description,DurationWeeks) values (@Name, @Description, @DurationWeeks)";
        var result = await connection.ExecuteAsync(sql, course);
        return result == null
            ? new Response<string>("Course not created", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Course created");
    }
    public async Task<Response<string>> UpdateCourseAsync(Course course)
    {
        using var connection = await context.GetConnection();
        var sql = "update courses set Name = @Name, Description = @Description , DurationWeeks = @DurationWeeks where id = @id";
        var result = await connection.ExecuteAsync(sql, course);
        return result == null
            ? new Response<string>("Course not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Course updated");
    }
    public async Task<Response<string>> DeleteCourseAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from courses where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Course not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Course deleted");
    }
}
