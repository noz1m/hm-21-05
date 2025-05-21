using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface IStudentGroupService
{
    public Task<Response<List<StudentGroup>>> GetAllStudentGroup();
    public Task<Response<StudentGroup>> GetStudentGroupById(int id);
    public Task<Response<string>> CreateStudentGroup(StudentGroup studentGroup);
    public Task<Response<string>> UpdateStudentGroup(StudentGroup studentGroup);
    public Task<Response<string>> DeleteStudentGroup(int id);
}
