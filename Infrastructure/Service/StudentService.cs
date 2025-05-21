using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Service;

public class StudentService(DataContext context) : IStudentService
{
    public async Task<Response<List<Student>>> GetAllStudentsAsync()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from students";
        var result = await connection.QueryAsync<Student>(sql);
        return result == null
            ? new Response<List<Student>>("Students not found", HttpStatusCode.NotFound)
            : new Response<List<Student>>(result.ToList(), "Students found");
    }
    public async Task<Response<Student>> GetStudentByIdAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "select * from students where id = @id";
        var result = await connection.QueryFirstOrDefaultAsync<Student>(sql, new { id });
        return result == null
            ? new Response<Student>("Student not found", HttpStatusCode.NotFound)
            : new Response<Student>(result, "Student found");
    }
    public async Task<Response<string>> CreateStudentAsync(Student student)
    {
        using var connection = await context.GetConnection();
        var sql = "insert into students (FullName, Email, Phone, EnrollmentDate) values (@FullName, @Email, @Phone, @EnrollmentDate)";
        var result = await connection.ExecuteAsync(sql, student);
        return result == null
            ? new Response<string>("Student not created", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Student created");
    }

    public async Task<Response<string>> UpdateStudentAsync(Student student)
    {
        using var connection = await context.GetConnection();
        var sql = "update students set FullName = @FullName, Email = @Email, Phone = @Phone, EnrollmentDate = @EnrollmentDate where id = @id";
        var result = await connection.ExecuteAsync(sql, student);
        return result == null
            ? new Response<string>("Student not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Student updated");
    }
    public async Task<Response<string>> DeleteStudentAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from students where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Student not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null,"Student deleted");
    }
}
