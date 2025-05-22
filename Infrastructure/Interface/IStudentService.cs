using Domain.ApiResponse;
using Domain.Entities;
using Domain.DTO;
namespace Infrastructure.Interface;

public interface IStudentService
{
    public Task<Response<List<Student>>> GetAllStudentsAsync();
    public Task<Response<Student>> GetStudentByIdAsync(int id);
    public Task<Response<string>> CreateStudentAsync(Student student);
    public Task<Response<string>> UpdateStudentAsync(Student student);
    public Task<Response<string>> DeleteStudentAsync(int id);
    public Task<Response<List<StudentWithGroupsDto>>> GetStudentsWithGroups();
    public Task<Response<List<Student>>> GetStudentsWithoutGroups();
    public Task<Response<List<Student>>> GetDroppedOutStudents();
    public Task<Response<List<StudentWithGroupsDto>>> GetGraduatedStudents();
}
