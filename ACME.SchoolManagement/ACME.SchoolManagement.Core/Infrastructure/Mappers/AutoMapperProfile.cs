using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments.Dtos;
using AutoMapper;

namespace ACME.SchoolManagement.Core.Infrastructure.Mappers
{
    /// <summary>
    /// AutoMapper profile configuration for mapping between domain entities and DTOs.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Configures mappings between domain models and data transfer objects.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseWithStudentsDto>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id)) // Maps Course.Id to CourseWithStudentsDto.CourseId
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Name)) // Maps Course.Name to CourseWithStudentsDto.CourseName
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)) // Maps Course.StartDate to CourseWithStudentsDto.StartDate
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate)) // Maps Course.EndDate to CourseWithStudentsDto.EndDate
                .ForMember(dest => dest.Students, opt => opt.Ignore()); // Ignores the Students property
        }
    }
}
