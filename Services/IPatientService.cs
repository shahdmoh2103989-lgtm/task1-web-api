using task1_web_api.DTOs;

namespace task1_web_api.Services
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllAsync(int page, int pageSize);

        Task<PatientDto?> GetByIdAsync(int id);

        Task<List<PatientDto>> SearchByNameAsync(string name);

        Task<List<PatientDto>> FilterByDateOfBirthAsync(
            DateTime dateOfBirth);
    }
}