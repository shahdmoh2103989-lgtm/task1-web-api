using AutoMapper;
using task1_web_api.DTOs;
using task1_web_api.Models;

namespace task1_web_api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();
        }
    }
} 