using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Infrastructure.Mappers;
using ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments.Dtos;
using AutoMapper;

namespace ACME.SchoolManagement.UnitTests.Infrastructure.Mappers
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Should_Configure_Mapping_Correctly_Between_Course_And_CourseWithStudentsDto()
        {
            // Arrange
            var course = new Course(
                "Math 101",
                100.00m,
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow.AddDays(10)
            );

            // Act
            var dto = _mapper.Map<CourseWithStudentsDto>(course);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal(course.Id, dto.CourseId);
            Assert.Equal(course.Name, dto.CourseName);
            Assert.Equal(course.StartDate, dto.StartDate);
            Assert.Equal(course.EndDate, dto.EndDate);
            Assert.Empty(dto.Students); // Should remain empty since it is ignored by the mapper
        }
    }
}
