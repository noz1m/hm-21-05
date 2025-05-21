using Domain.ApiResponse;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface IGroupService
{
    public Task<Response<List<Group>>> GetAllGroup();
    public Task<Response<Group>> GetGroupById(int id);
    public Task<Response<string>> CreateGroup(Group group);
    public Task<Response<string>> UpdateGroup(Group group);
    public Task<Response<string>> DeleteGroup(int id);
}
