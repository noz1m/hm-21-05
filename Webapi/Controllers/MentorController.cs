using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MentorController(IMentorService mentorService)
{
    [HttpGet]
    public async Task<Response<List<Mentor>>> GetAllMentors()
    {
        return await mentorService.GetAllMentors();
    }

    [HttpGet ("{id}")]
    public async Task<Response<Mentor>> GetMentorById(int id)
    {
        return await mentorService.GetMentorById(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateMentor(Mentor mentor)
    {
        return await mentorService.UpdateMentor(mentor);
    }
    [HttpPost]
    public async Task<Response<string>> CreateMentor(Mentor mentor)
    {
        return await mentorService.CreateMentor(mentor);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteMentor(int id)
    {
        return await mentorService.DeleteMentor(id);
    }
}
