using task1_web_api.Models;

namespace task1_web_api.Repositories
{
    public interface IPatientRepository
        : IGenericRepository<Patient>
    {
        Task<List<Patient>> SearchByNameAsync(string name);

        Task<List<Patient>> FilterByDateOfBirthAsync(
            DateTime dateOfBirth);
    }
}