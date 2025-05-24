using Domain.ApiResponse;
using Domain.Entities;
using Domain.DTO;
using Infrastructure.Data;
using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
    [HttpGet("get-students-with-groups")]
    public async Task<Response<List<StudentWithGroupsDto>>> GetStudentsWithGroups()
    {
        return await studentService.GetStudentsWithGroups();
    }
    [HttpGet("get-students-without-groups")]
    public async Task<Response<List<Student>>> GetStudentsWithoutGroups()
    {
        return await studentService.GetStudentsWithoutGroups();
    }
    [HttpGet("get-dropped-out-students")]
    public async Task<Response<List<Student>>> GetDroppedOutStudents()
    {
        return await studentService.GetDroppedOutStudents();
    }
    [HttpGet("get-graduated-students")]
    public async Task<Response<List<StudentWithGroupsDto>>> GetGraduatedStudents()
    {
        return await studentService.GetGraduatedStudents();
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
