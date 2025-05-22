using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
namespace Infrastructure.Service;

public class StatisticsService(DataContext context) : IStatisticsService
{
    public async Task<Response<int>> GetTotalStudentsCount()
    {
        using var connection = await context.GetConnection();
        var sql = "select count(*) from students";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result == 0
            ? new Response<int>("Students not found", HttpStatusCode.NotFound)
            : new Response<int>(result, "Students found");
    }
    public async Task<Response<int>> GetTotalGroupsCount()
    {
        using var connection = await context.GetConnection();
        var sql = "select count(*) from groups";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result == 0
            ? new Response<int>("Group not found", HttpStatusCode.NotFound)
            : new Response<int>(result, "Group found");
    }
    public async Task<Response<int>> GetTotalCoursesCount()
    {
        using var connection = await context.GetConnection();
        var sql = "select count(*) from courses";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result == 0
            ? new Response<int>("Courses not found", HttpStatusCode.NotFound)
            : new Response<int>(result, "Courses found");
    }
    public async Task<Response<int>> GetTotalMentorsCount()
    {
        using var connection = await context.GetConnection();
        var sql = "select count(*) from mentors";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result == 0
            ? new Response<int>("Mentors not found", HttpStatusCode.NotFound)
            : new Response<int>(result, "Mentors found");
    }
    public async Task<Response<List<DateTime>>> GetAllStartDates()
    {
        using var connection = await context.GetConnection();
        var sql = "select dictinct(startDate) from groups";
        var result = await connection.QueryAsync<DateTime>(sql);
        return result == null
            ? new Response<List<DateTime>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<DateTime>>(result.ToList(), "Group found");
    }
}
