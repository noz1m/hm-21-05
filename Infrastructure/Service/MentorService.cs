using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
namespace Infrastructure.Interface;

public class MentorService(DataContext context) : IMentorService
{
    public async Task<Response<List<Mentor>>> GetAllMentors()
    {
        using var connection = await context.GetConnection();
        var sql = @"select * from mentors";
        var result = await connection.QueryAsync<Mentor>(sql);
        return result == null
            ? new Response<List<Mentor>>("Mentors not found", HttpStatusCode.NotFound)
            : new Response<List<Mentor>>(result.ToList(),"Mentors found");
    }
    public async Task<Response<Mentor>> GetMentorById(int id)
    {
        using var connection = await context.GetConnection();
        var sql = @"select * from mentors where id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<Mentor>(sql, new { id });
        return result == null
            ? new Response<Mentor>("Mentor not found", HttpStatusCode.NotFound)
            : new Response<Mentor>(result,"Mentor found");
    }
    public async Task<Response<string>> CreateMentor(Mentor mentor)
    {
        using var connection = await context.GetConnection();
        var sql = @"insert into mentors (fullName,email,phone,specialization)
                    values (@fullName,@email,@phone,@specialization)";
        var result = await connection.ExecuteAsync(sql, mentor);
        return result == null
            ? new Response<string>("Mentor not created", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Mentor created");
    }
    public async Task<Response<string>> UpdateMentor(Mentor mentor)
    {
        using var connection = await context.GetConnection();
        var sql = @"update mentors set fullName = @fullName, email = @email, phone = @phone, specialization = @specialization where id = @id";
        var result = await connection.ExecuteAsync(sql, mentor);
        return result == null
            ? new Response<string>("Mentor not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Mentor updated");
    }
    public async Task<Response<string>> DeleteMentor(int id)
    {
        using var connection = await context.GetConnection();
        var sql = @"delete from mentors where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Mentor not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Mentor deleted");
    }

    public async Task<Response<MentorStatDto>> GetMentorWithMostStudents()
    {
        using var connection = await context.GetConnection();
        var sql = @"select m.id,m.fullName,count(distinct sg.studentId) as StudentCount from mentors m
            join groups g on g.mentorId = m.id
            join studentGroups sg on sg.groupId = g.id
            group by m.id,m.fullName
            order by StudentCount desc
            limit 1";
        var result = await connection.QueryFirstOrDefaultAsync<MentorStatDto>(sql);
        return result == null
            ? new Response<MentorStatDto>("Mentor not found", HttpStatusCode.NotFound)
            : new Response<MentorStatDto>(result,"Mentor found");
    }

    public async Task<Response<List<MentorCoursesDto>>> GetMentorsWithMultipleCourses()
    {
        using var connection = await context.GetConnection();
        var sql = @"m.id,m.fullName,count(distinct g.courseId) as CourseCount from mentors m
            join groups g on g.mentorId = m.id
            group by m.id,m.fullName
            having CourseCount > 1";
        var result = await connection.QueryAsync<MentorCoursesDto>(sql);
        return result == null
            ? new Response<List<MentorCoursesDto>>("Mentors not found", HttpStatusCode.NotFound)
            : new Response<List<MentorCoursesDto>>(result.ToList(),"Mentors found"); 
    }
}