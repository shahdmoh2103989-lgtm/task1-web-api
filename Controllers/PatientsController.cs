using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task1_web_api.Common;
using task1_web_api.DTOs;
using task1_web_api.Services;
using task1_web_api.Filters;

namespace task1_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(
            IPatientService patientService,
            ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest(Result<string>.Failure(
                    "Invalid pagination values"));

            var patients =
                await _patientService.GetAllAsync(page, pageSize);

            _logger.LogInformation("Retrieved patients successfully");

            return Ok(Result<List<PatientDto>>.Success(patients));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);

            if (patient == null)
                return NotFound(
                    Result<string>.Failure("Patient not found"));

            return Ok(Result<PatientDto>.Success(patient));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name)
        {
            var patients =
                await _patientService.SearchByNameAsync(name);

            return Ok(Result<List<PatientDto>>.Success(patients));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(DateTime dateOfBirth)
        {
            var patients =
                await _patientService.FilterByDateOfBirthAsync(dateOfBirth);

            return Ok(Result<List<PatientDto>>.Success(patients));
        }
    }
}