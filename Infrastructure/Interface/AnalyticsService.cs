using Domain.ApiResponse;
using Domain.DTO;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface AnalyticsService
{
    public Task<Response<List<Student>>> GetCompletionRate();
}
