using AutoMapper;
using task1_web_api.DTOs;
using task1_web_api.Repositories;

namespace task1_web_api.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(
            IPatientRepository patientRepository,
            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> GetAllAsync(
            int page, int pageSize)
        {
            var patients = await _patientRepository.GetAllAsync();

            var pagedPatients = patients
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return _mapper.Map<List<PatientDto>>(pagedPatients);
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            return patient == null
                ? null
                : _mapper.Map<PatientDto>(patient);
        }

        public async Task<List<PatientDto>> SearchByNameAsync(
            string name)
        {
            var patients =
                await _patientRepository.SearchByNameAsync(name);

            return _mapper.Map<List<PatientDto>>(patients);
        }

        public async Task<List<PatientDto>> FilterByDateOfBirthAsync(
            DateTime dateOfBirth)
        {
            var patients =
                await _patientRepository.FilterByDateOfBirthAsync(dateOfBirth);

            return _mapper.Map<List<PatientDto>>(patients);
        }
    }
}