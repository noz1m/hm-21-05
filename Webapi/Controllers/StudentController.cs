using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
namespace Webapi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService)
{
    [HttpGet]
    public async Task<Response<List<Student>>> GetAllStudentsAsync()
    {
        return await studentService.GetAllStudentsAsync();
    }

    [HttpGet("{id}")]
    public async Task<Response<Student>> GetStudentByIdAsync(int id)
    {
        return await studentService.GetStudentByIdAsync(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateStudentAsync(Student student)
    {
        return await studentService.UpdateStudentAsync(student);
    }
    [HttpPost]
    public async Task<Response<string>> CreateStudentAsync(Student student)
    {
        return await studentService.CreateStudentAsync(student);
    }
    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteStudentAsync(int id)
    {
        return await studentService.DeleteStudentAsync(id);
    }
}
