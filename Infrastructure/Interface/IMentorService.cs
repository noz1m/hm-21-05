using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Interface;

public interface IMentorService
{
    public Task<Response<List<Mentor>>> GetAllMentors();
    public Task<Response<Mentor>> GetMentorById(int id);
    public Task<Response<string>> CreateMentor(Mentor mentor);
    public Task<Response<string>> UpdateMentor(Mentor mentor);
    public Task<Response<string>> DeleteMentor(int id);
}
