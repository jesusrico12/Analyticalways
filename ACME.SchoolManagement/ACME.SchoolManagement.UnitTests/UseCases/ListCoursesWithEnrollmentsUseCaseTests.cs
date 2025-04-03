using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments.Dtos;
using ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments;

namespace ACME.SchoolManagement.UnitTests.UseCases
{
    public class ListCoursesWithEnrollmentsUseCaseTests : UseCaseTestsBase
    {
        [Fact]
        public void Should_List_Courses_With_Enrollments()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);

            var course = new Course("Math 101", 100m, DateTime.UtcNow.AddDays(3), DateTime.UtcNow.AddDays(10));
            var student = new Student("John Doe", 18);
            var enrollment = new Enrollment(student, course, true);

            var enrollments = new List<Enrollment> { enrollment };
            EnrollmentRepositoryMock.Setup(r => r.GetEnrollmentsBetweenDates(startDate, endDate)).Returns(enrollments);

            var courseDto = new CourseWithStudentsDto { CourseName = "Math 101", Students = new List<Student>() { student} };
            MapperMock.Setup(m => m.Map<CourseWithStudentsDto>(course)).Returns(courseDto);

            var useCase = new ListCoursesWithEnrollmentsUseCase(EnrollmentRepositoryMock.Object, MapperMock.Object);

            // Act
            var result = useCase.Execute(startDate, endDate);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Equal(courseDto.CourseName, result.First().CourseName);
            Assert.Equal(courseDto.Students[0].Id, result.First().Students[0].Id);
            Assert.Equal(courseDto.EndDate, result.First().EndDate);
            Assert.Equal(courseDto.StartDate, result.First().StartDate);
            Assert.Equal(courseDto.CourseId, result.First().CourseId);
        }

        [Fact]
        public void Should_Return_Empty_If_No_Enrollments_Found()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-1);
            var endDate = DateTime.UtcNow.AddDays(1);

            EnrollmentRepositoryMock.Setup(r => r.GetEnrollmentsBetweenDates(startDate, endDate)).Returns(new List<Enrollment>());

            var useCase = new ListCoursesWithEnrollmentsUseCase(EnrollmentRepositoryMock.Object, MapperMock.Object);

            // Act
            var result = useCase.Execute(startDate, endDate);

            // Assert
            Assert.Empty(result);
        }
    }
}