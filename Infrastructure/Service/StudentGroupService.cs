using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
namespace Infrastructure.Service;

public class StudentGroupService(DataContext context) : IStudentGroupService
{
    public async Task<Response<List<StudentGroup>>> GetAllStudentGroup()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from studentGroups";
        var result = await connection.QueryAsync<StudentGroup>(sql);
        return result == null
            ? new Response<List<StudentGroup>>("StudentGroups not found", HttpStatusCode.NotFound)
            : new Response<List<StudentGroup>>(result.ToList(), "StudentGroups found");
    }
    public async Task<Response<StudentGroup>> GetStudentGroupById(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "select * from studentGroups where id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<StudentGroup>(sql, new { id });
        return result == null
            ? new Response<StudentGroup>("StudentGroup not found", HttpStatusCode.NotFound)
            : new Response<StudentGroup>(result, "StudentGroup found");
    }
    public async Task<Response<string>> CreateStudentGroup(StudentGroup studentGroup)
    {
        using var connection = await context.GetConnection();
        var sql1 = @"select * from students where id = @id";
        var result1 = await connection.QueryFirstOrDefaultAsync<Student>(sql1, new { studentGroup.StudentId });
        if (result1 == null)
        {
            return new Response<string>("Student not found", HttpStatusCode.NotFound);
        }
        var sql2 = @"select * from groups where id = @id";
        var result2 = await connection.QueryFirstOrDefaultAsync<Group>(sql2, new { studentGroup.GroupId });
        if (result2 == null)
        {
            return new Response<string>("Group not found", HttpStatusCode.NotFound);
        }
        var sql = "insert into studentGroups (GroupId, StudentId, Status) values (@GroupId, @StudentId, @Status)";
        var result = await connection.ExecuteAsync(sql, studentGroup);
        return result == null
            ? new Response<string>("StudentGroup not created", HttpStatusCode.BadRequest)
            : new Response<string>(null, "StudentGroup created");
    }
    public async Task<Response<string>> UpdateStudentGroup(StudentGroup studentGroup)
    {
        using var connection = await context.GetConnection();
        var sql1 = @"select * from students where id = @id";
        var result1 = await connection.QueryFirstOrDefaultAsync<Student>(sql1, new { studentGroup.StudentId });
        if (result1 == null)
        {
            return new Response<string>("Student not found", HttpStatusCode.NotFound);
        }
        var sql2 = @"select * from groups where id = @id";
        var result2 = await connection.QueryFirstOrDefaultAsync<Group>(sql2, new { studentGroup.GroupId });
        if (result2 == null)
        {
            return new Response<string>("Group not found", HttpStatusCode.NotFound);
        }
        var sql = "update studentGroups set GroupId = @GroupId, StudentId = @StudentId , Status = @Status where id = @id";
        var result = await connection.ExecuteAsync(sql, studentGroup);
        return result == null
            ? new Response<string>("StudentGroup not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null, "StudentGroup updated");
    }

    public async Task<Response<string>> DeleteStudentGroup(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from studentGroups where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("StudentGroup not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null, "StudentGroup deleted");
    }

    // public async Task<Response<List<StudentPerGroupDto>>> GetStudentsPerCourse()
    // {
   
    // }
}
