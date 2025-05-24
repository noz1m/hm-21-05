using System.Data;
using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.DTO;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Service;

public class StudentService(DataContext context, IWebHostEnvironment webHostEnvironment) : IStudentService
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
        var wwwRootPath = webHostEnvironment.WebRootPath;
        var folderPath = Path.Combine(wwwRootPath, "StudentPhoto");
        var fileName = student.Photo.FileName;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var fullPath = Path.Combine(folderPath, fileName);

        await using var connection = await context.GetConnection();
        await using (var stream = File.Create(fullPath))
        {
            await student.Photo.CopyToAsync(stream);
        }
        var sql = "insert into students (FullName, Email, Phone, EnrollmentDate, photo) values (@FullName, @Email, @Phone, @EnrollmentDate, @photo)";
        var anonymousObject = new
        {
            FullName = student.FullName,
            Email = student.Email,
            Phone = student.Phone,
            EnrollmentDate = student.EnrollmentDate,
            Photo = student.Photo.FileName
        };
        var result = await connection.ExecuteAsync(sql, anonymousObject);
        return result == 0
            ? new Response<string>("Student not created", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Student created");
    }
    public async Task<Response<string>> UpdateStudentAsync(Student student)
    {
        using var connection = await context.GetConnection();
        var sql = "update students set FullName = @FullName, Email = @Email, Phone = @Phone, EnrollmentDate = @EnrollmentDate where id = @id";
        var result = await connection.ExecuteAsync(sql, student);
        return result == null
            ? new Response<string>("Student not updated", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Student updated");
    }
    public async Task<Response<string>> DeleteStudentAsync(int id)
    {
        using var connection = await context.GetConnection();
        var sql = "delete from students where id = @id";
        var result = await connection.ExecuteAsync(sql, new { id });
        return result == null
            ? new Response<string>("Student not deleted", HttpStatusCode.BadRequest)
            : new Response<string>(null, "Student deleted");
    }
    public async Task<Response<List<StudentWithGroupsDto>>> GetStudentsWithGroups()
    {
        using var connection = await context.GetConnection();
        var sql = @"select s.FullName, g.GroupName from students s
                    join studentGroups sg on s.Id = sg.StudentId
                    join groups g on sg.Id = g.Id
                    order by s.FullName";
        var result = await connection.QueryAsync<StudentWithGroupsDto>(sql);
        return result == null
            ? new Response<List<StudentWithGroupsDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<StudentWithGroupsDto>>(result.ToList(), "Group found");
    }
    public async Task<Response<List<Student>>> GetStudentsWithoutGroups()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from students where id not in (select studentId from studentGroups)";
        var result = await connection.QueryAsync<Student>(sql);
        return result == null
            ? new Response<List<Student>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<Student>>(result.ToList(), "Group found");
    }

    public async Task<Response<List<Student>>> GetDroppedOutStudents()
    {
        using var connection = await context.GetConnection();
        var sql = "select * from students where id not in (select studentId from studentGroups)";
        var result = await connection.QueryAsync<Student>(sql);
        return result == null
            ? new Response<List<Student>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<Student>>(result.ToList(), "Group found");
    }

    public async Task<Response<List<StudentWithGroupsDto>>> GetGraduatedStudents()
    {
        using var connection = await context.GetConnection();
        var sql = @"select s.id, s.fullname, s.email, s.phone, s.enrollmentDate
                    from students s
                    join studentGroups sg on sg.studentId = s.id
                    join groups g on g.id = sg.groupId
                    group by s.id, s.fullname, s.email, s.phone, s.enrollmentDate
                    having max(g.endDate) < @Today";
        var result = await connection.QueryAsync<StudentWithGroupsDto>(sql, new { Today = DateTime.Now });
        return result == null
            ? new Response<List<StudentWithGroupsDto>>("Groups not found", HttpStatusCode.NotFound)
            : new Response<List<StudentWithGroupsDto>>(result.ToList(), "Group found");
    }

}

















// using System.Net;
// using Dapper;
// using Domain.ApiResponse;
// using Domain.Entities;
// using Infrastructure.Data;
// using Infrastructure.Interfaces;
// using Microsoft.AspNetCore.Hosting;

// namespace Infrastructure.Services;

// public class CarService(DataContext context, IWebHostEnvironment webHostEnvironment) : ICarService
// {
//     public async Task<Response<List<GetCarDto>>> GetCarsAsync()
//     {
//         using (var connection = await context.GetConnectionAsync())
//         {
//             var cmd = "select * from cars";
//             var cars = await connection.QueryAsync<GetCarDto>(cmd);

//             return new Response<List<GetCarDto>>(cars.ToList(), "Cars retrieved successfully");
//         }
//     }

//     public async Task<Response<Car>> GetCarAsync(int id)
//     {
//         using (var connection = await context.GetConnectionAsync())
//         {
//             var cmd = "select * from cars where id = @id";
//             var car = await connection.QueryFirstOrDefaultAsync<Car>(cmd, new { Id = id });

//             return car == null
//                 ? new Response<Car>("Car not found", HttpStatusCode.NotFound)
//                 : new Response<Car>(car, "Car successfully retrieved");
//         }
//     }

//     public async Task<Response<string>> CreateCarAsync(Car car)
//     {

//         var wwwRootPath = webHostEnvironment.WebRootPath;
//         var folderPath = Path.Combine(wwwRootPath, "CarImages");
//         var fileName = car.Photo.FileName;

//         if (!Directory.Exists(folderPath))
//         {
//             Directory.CreateDirectory(folderPath);
//         }

//         var fullPath = Path.Combine(folderPath, fileName);

//         await using (var connection = await context.GetConnectionAsync())
//         {
//             await using (var stream = File.Create(fullPath))
//             {
//                 await car.Photo.CopyToAsync(stream);
//             }

//             var cmd = @$"insert into cars(model, manufacturer, year, pricePerDay, photo)
//                          values (@model, @manufacturer, @year, @pricePerDay, @photo);";

//             var anonymousObject = new
//             {
//                 Model = car.Model,
//                 Manufacturer = car.Manufacturer,
//                 Year = car.Year,
//                 PricePerDay = car.PricePerDay,
//                 Photo = car.Photo.FileName,
//             };
//             var result = await connection.ExecuteAsync(cmd, anonymousObject);
//             return result == 0
//                 ? new Response<string>("Some thing went wrong", HttpStatusCode.InternalServerError)
//                 : new Response<string>(null, "Car successfully created");
//         }
//     }
// }