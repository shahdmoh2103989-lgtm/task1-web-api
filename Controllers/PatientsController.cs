using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using task1_web_api.Data;
using task1_web_api.Models;
using task1_web_api.Common;
using task1_web_api.DTOs;

namespace task1_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PatientsController> _logger;
        private readonly IMapper _mapper;

        public PatientsController(
            AppDbContext context,
            ILogger<PatientsController> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // Get All Patients with Pagination
        [HttpGet]
        public async Task<IActionResult> GetAll(
            int page = 1, int pageSize = 5)
        {
            _logger.LogInformation("Getting all patients");

            if (page < 1 || pageSize < 1)
            {
                return BadRequest(
                    Result<string>.Failure(
                        "Page and PageSize must be greater than 0"));
            }

            var patients = await _context.Patients
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var patientDtos =
                _mapper.Map<List<PatientDto>>(patients);

            return Ok(
                Result<List<PatientDto>>.Success(
                    patientDtos,
                    "Patients retrieved successfully"));
        }

        // Get Patient By ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation(
                "Getting patient with ID: {PatientId}", id);

            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                _logger.LogWarning(
                    "Patient with ID: {PatientId} not found", id);

                return NotFound(
                    Result<string>.Failure("Patient not found"));
            }

            var patientDto = _mapper.Map<PatientDto>(patient);

            return Ok(
                Result<PatientDto>.Success(
                    patientDto,
                    "Patient retrieved successfully"));
        }

        // Search Patients by Name
        [HttpGet("search")]
        public async Task<IActionResult> Search(string name)
        {
            _logger.LogInformation(
                "Searching patients by name: {Name}", name);

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(
                    Result<string>.Failure("Name is required"));
            }

            var patients = await _context.Patients
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            var patientDtos =
                _mapper.Map<List<PatientDto>>(patients);

            return Ok(
                Result<List<PatientDto>>.Success(
                    patientDtos,
                    "Search completed successfully"));
        }

        // Filter Patients by Date of Birth
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(
            DateTime? dateOfBirth)
        {
            _logger.LogInformation(
                "Filtering patients by date of birth");

            var query = _context.Patients.AsQueryable();

            if (dateOfBirth.HasValue)
            {
                query = query.Where(p =>
                    p.DateOfBirth.Date == dateOfBirth.Value.Date);
            }

            var patients = await query.ToListAsync();

            var patientDtos =
                _mapper.Map<List<PatientDto>>(patients);

            return Ok(
                Result<List<PatientDto>>.Success(
                    patientDtos,
                    "Filtering completed successfully"));
        }
    }
}