
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentGroupService(IStudentGroupService studentGroupService)
{
    [HttpGet]
    public async Task<Response<List<StudentGroup>>> GetAllStudentGroup()
    {
        return await studentGroupService.GetAllStudentGroup();
    }
    [HttpPost]
    public async Task<Response<string>> CreateStudentGroup(StudentGroup studentGroup)
    {
        return await studentGroupService.CreateStudentGroup(studentGroup);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateStudentGroup(StudentGroup studentGroup)
    {
        return await studentGroupService.UpdateStudentGroup(studentGroup);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteStudentGroup(int id)
    {
        return await studentGroupService.DeleteStudentGroup(id);
    }
}
