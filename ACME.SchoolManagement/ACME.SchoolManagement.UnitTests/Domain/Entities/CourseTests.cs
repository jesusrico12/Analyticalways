using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.UnitTests.Domain.Entities
{
    public class CourseTests
    {
        [Fact]
        public void Should_Create_Course_With_Valid_Data()
        {
            // Arrange
            var name = "Math 101";
            var registrationFee = 100.00m;
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(10);

            // Act
            var course = new Course(name, registrationFee, startDate, endDate);

            // Assert
            Assert.NotNull(course);
            Assert.Equal(name, course.Name);
            Assert.Equal(registrationFee, course.RegistrationFee);
            Assert.Equal(startDate, course.StartDate);
            Assert.Equal(endDate, course.EndDate);
        }

        [Theory]
        [InlineData(null)]  // Name is null
        [InlineData("")]    // Name is empty
        [InlineData("   ")] // Name is whitespace
        public void Should_Throw_Exception_When_Name_Is_Invalid(string invalidName)
        {
            // Arrange
            var registrationFee = 100.00m;
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(10);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Course(invalidName, registrationFee, startDate, endDate));
            Assert.Equal("Course name cannot be null or empty. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_RegistrationFee_Is_Negative()
        {
            // Arrange
            var name = "Math 101";
            var invalidFee = -10.00m;
            var startDate = DateTime.UtcNow.AddDays(1);
            var endDate = DateTime.UtcNow.AddDays(10);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Course(name, invalidFee, startDate, endDate));
            Assert.Equal("Registration fee must be a positive value. (Parameter 'registrationFee')", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_StartDate_Is_After_EndDate()
        {
            // Arrange
            var name = "Math 101";
            var registrationFee = 100.00m;
            var invalidStartDate = DateTime.UtcNow.AddDays(10); // Start date after end date
            var endDate = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Course(name, registrationFee, invalidStartDate, endDate));
            Assert.Equal("Start date must be before the end date. (Parameter 'startDate')", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_EndDate_Is_In_The_Past()
        {
            // Arrange
            var name = "Math 101";
            var fee = 100m;
            var startDate = DateTime.UtcNow.AddDays(-2);  // Future date
            var endDate = DateTime.UtcNow.AddDays(-1);   // Past date

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Course(name, fee, startDate, endDate));
            Assert.Equal("The course must start in the future. (Parameter 'startDate')", exception.Message);
        }
    }
}
