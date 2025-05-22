using Domain.ApiResponse;
using Domain.Entities;
using Domain.DTO;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController(IGroupService groupService)
{
    [HttpGet]
    public async Task<Response<List<Group>>> GetAllGroup()
    {
        return await groupService.GetAllGroup();
    }

    [HttpGet ("{id}")]
    public async Task<Response<Group>> GetGroupById(int id)
    {
        return await groupService.GetGroupById(id);
    }
    [HttpGet("get-students-per-group")]
    public async Task<Response<List<StudentPerGroupDto>>> GetStudentsPerGroup()
    {
        return await groupService.GetStudentsPerGroup();
    }
    [HttpGet("get-empty-groups")]
    public async Task<Response<List<Group>>> GetEmptyGroups()
    {
        return await groupService.GetEmptyGroups();
    }
    [HttpPut]
    public async Task<Response<string>> UpdateGroup(Group group)
    {
        return await groupService.UpdateGroup(group);
    }
    [HttpPost]
    public async Task<Response<string>> CreateGroup(Group group)
    {
        return await groupService.CreateGroup(group);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteGroup(int id)
    {
        return await groupService.DeleteGroup(id);
    }
}
