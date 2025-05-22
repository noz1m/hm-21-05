using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class StatisticsServiceController(IStatisticsService statisticsService)
{
    [HttpGet("get-all-start-dates")]
    public async Task<Response<List<DateTime>>> GetAllStartDates()
    {
        return await statisticsService.GetAllStartDates();
    }
    [HttpGet("get-total-groups-count")]
    public async Task<Response<int>> GetTotalGroupsCount()
    {
        return await statisticsService.GetTotalGroupsCount();
    }
    [HttpGet("get-total-students-count")]
    public async Task<Response<int>> GetTotalStudentsCount()
    {
        return await statisticsService.GetTotalStudentsCount();
    }
    [HttpGet("get-total-courses-count")]
    public async Task<Response<int>> GetTotalCoursesCount()
    {
        return await statisticsService.GetTotalCoursesCount();
    }
    [HttpGet("get-total-mentors-count")]
    public async Task<Response<int>> GetTotalMentorsCount()
    {
        return await statisticsService.GetTotalMentorsCount();
    }
}
