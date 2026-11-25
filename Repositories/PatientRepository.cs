using Microsoft.EntityFrameworkCore;
using task1_web_api.Data;
using task1_web_api.Models;

namespace task1_web_api.Repositories
{
    public class PatientRepository
        : GenericRepository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<Patient>> SearchByNameAsync(
            string name)
        {
            return await _context.Patients
                .Where(p => p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Patient>> FilterByDateOfBirthAsync(
            DateTime dateOfBirth)
        {
            return await _context.Patients
                .Where(p => p.DateOfBirth.Date == dateOfBirth.Date)
                .ToListAsync();
        }
    }
}