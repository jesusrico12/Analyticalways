using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.UseCases.RegisterCourse;
using Moq;

namespace ACME.SchoolManagement.UnitTests.UseCases
{
    public class RegisterCourseUseCaseTests : UseCaseTestsBase
    {
        [Fact]
        public void Should_Register_Valid_Course()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act
            var course = useCase.Execute("Math 101", 100m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            // Assert
            Assert.NotNull(course);
            Assert.Equal("Math 101", course.Name);
            Assert.Equal(100m, course.RegistrationFee);

            // Verify that the course was added to the repository
            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public void Should_Register_Course_With_Zero_RegistrationFee()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act
            var course = useCase.Execute("Free Course", 0m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            // Assert
            Assert.NotNull(course);
            Assert.Equal("Free Course", course.Name);
            Assert.Equal(0m, course.RegistrationFee);
            CourseRepositoryMock.Verify(r => r.Add(It.Is<Course>(c => c.RegistrationFee == 0m)), Times.Once);
        }

        [Theory]
        [InlineData(null)]    // Name is null
        [InlineData("")]      // Name is empty
        [InlineData("   ")]   // Name is whitespace
        public void Should_Throw_Exception_When_Name_Is_Invalid(string invalidName)
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(invalidName, 100m, DateTime.UtcNow, DateTime.UtcNow.AddDays(1)));
            Assert.Equal("Course name cannot be null or empty. (Parameter 'name')", exception.Message);

            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public void Should_Throw_Exception_When_RegistrationFee_Is_Negative()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute("Math 101", -1m, DateTime.UtcNow, DateTime.UtcNow.AddDays(1)));
            Assert.Equal("Registration fee must be a positive value. (Parameter 'registrationFee')", exception.Message);

            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public void Should_Throw_Exception_When_StartDate_Is_Not_Before_EndDate()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute("Math 101", 100m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow));
            Assert.Equal("Start date must be before the end date. (Parameter 'startDate')", exception.Message);

            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public void Should_Throw_Exception_When_Using_Past_Dates()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute("Math 101", 100m, DateTime.UtcNow.AddDays(-10), DateTime.UtcNow.AddDays(-5)));
            Assert.Equal("The course must start in the future. (Parameter 'startDate')", exception.Message);

            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public void Should_Throw_Exception_If_StartDate_Is_In_Past()
        {
            // Arrange
            var useCase = new RegisterCourseUseCase(CourseRepositoryMock.Object);

            var name = "Science 202";
            var fee = 150m;
            var startDate = DateTime.UtcNow.AddDays(-6); // Past date
            var endDate = DateTime.UtcNow.AddDays(5);    // Future date

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(name, fee, startDate, endDate));
            Assert.Equal("The course must start in the future. (Parameter 'startDate')", exception.Message);

            CourseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()), Times.Never);
        }
    }
}