using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.DTO;
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
            : new Response<string>(null, "Course created");
    }
    public async Task<Response<string>> UpdateCourseAsync(Course course)
    {
        using var connection = await context.GetConnection();
        var sql = "update courses set Name = @Name, Description = @Description , DurationWeeks = @DurationWeeks where id = @id";
        var result = await connection.ExecuteAsync(sql, course);
        return result == null
            ? new Response<string>("Course not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Course updated");
    }
    public async Task<Response<string>> DeleteCourseAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from courses where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Course not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Course deleted");
    }

    public async Task<Response<List<StudentsPerCourseDto>>> GetStudentsPerCourse()
    {
        using var connection = await context.GetConnection();
        var sql = @"select c.id, c.title, count(distinct sg.studentId) from courses c
            join groups g on g.courseId = c.id
            join studentGroups sg on sg.groupId = g.id
            group by c.id, c.title
            order by c.title";
        var result = await connection.QueryAsync<StudentsPerCourseDto>(sql);
        return result == null
            ? new Response<List<StudentsPerCourseDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<StudentsPerCourseDto>>(result.ToList(), "Group found");
    }

    public async Task<Response<List<PopularCourseDto>>> GetMostPopularCourse()
    {
        using var connection = await context.GetConnection();
        var sql = @"select c.id, c.title, count(distinct sg.studentId) from courses c
            join groups g on g.courseId = c.Id
            join studentGroups sg on sg.groupId = g.Id
            group by c.id, c.title
            order by count(distinct sg.studentId) desc
            limit 1";
        var result = await connection.QueryAsync<PopularCourseDto>(sql);
        return result == null
            ? new Response<List<PopularCourseDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<PopularCourseDto>>(result.ToList(), "Group found");
    }

    public async Task<Response<List<LeastPopularCourseDto>>> GetLeastPopularCourses()
    {
        using var connection = await context.GetConnection();
        var sql = @"select c.id,c.title,count(distinct sg.studentId) from courses c
                left join groups g on g.courseId = c.id
                left join studentGroups sg on sg.groupId = g.id
                group by c.id, c.title
                order by StudentCount asc
                limit 3";
        var result = await connection.QueryAsync<LeastPopularCourseDto>(sql);
        return result == null
            ? new Response<List<LeastPopularCourseDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<LeastPopularCourseDto>>(result.ToList(), "Group found");
    }

    public async Task<Response<List<PopularCourseDto>>> GetTopThreeCourses()
    {
        using var connection = await context.GetConnection();
        var sql = @"select c.id, c.title, count(distinct sg.studentId) from courses c
                join groups g on g.courseId = c.id
                join studentGroups sg on sg.groupId = g.id
                group by c.id, c.title
                order by count(distinct sg.studentId) desc
                limit 3";
        var result = await connection.QueryAsync<PopularCourseDto>(sql);
        return result == null
            ? new Response<List<PopularCourseDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<PopularCourseDto>>(result.ToList(), "Group found");
    }
}
