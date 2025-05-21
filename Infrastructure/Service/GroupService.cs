using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
namespace Infrastructure.Service;

public class GroupService(DataContext context) : IGroupService
{
    public async Task<Response<List<Group>>> GetAllGroup()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from groups";
        var result = await connection.QueryAsync<Group>(sql);
        return result == null
            ? new Response<List<Group>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<Group>>(result.ToList(), "Groups found");
    }
    public async Task<Response<Group>> GetGroupById(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "select * from groups where id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<Group>(sql, new { id });
        return result == null ? new Response<Group>("Group not found", HttpStatusCode.NotFound) : new Response<Group>(result, "Group found");
    }
    public async Task<Response<string>> CreateGroup(Group group)
    {
        using var connection = await context.GetConnection();
        var sql = "insert into groups (GroupName, CourseId, MentorId, StartDate, EndDate) values (@GroupName, @CourseId, @MentorId, @StartDate, @EndDate)";
        var result = await connection.ExecuteAsync(sql, group);
        return result == null
            ? new Response<string>("Group not created", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Group created");
    }
    public async Task<Response<string>> UpdateGroup(Group group)
    {
        using var connection = await context.GetConnection();
        var sql = "update groups set GroupName = @GroupName, CourseId = @CourseId, MentorId = @MentorId, StartDate = @StartDate, EndDate = @EndDate where id = @id";
        var result = await connection.ExecuteAsync(sql, group);
        return result == null
            ? new Response<string>("Group not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Group updated");
    }
    public async Task<Response<string>> DeleteGroup(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from groups where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Group not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Group deleted");
    }
}
