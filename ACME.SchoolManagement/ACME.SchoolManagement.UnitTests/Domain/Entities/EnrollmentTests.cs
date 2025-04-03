using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.UnitTests.Domain.Entities
{
    public class EnrollmentTests
    {
        [Fact]
        public void Should_Create_Enrollment_With_Valid_Data()
        {
            // Arrange
            var student = new Student("John Doe", 18);
            var course = new Course("Math 101", 100.00m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var isPaid = false;

            // Act
            var enrollment = new Enrollment(student, course, isPaid);

            // Assert
            Assert.NotNull(enrollment);
            Assert.Equal(student, enrollment.Student);
            Assert.Equal(course, enrollment.Course);
            Assert.Equal(isPaid, enrollment.IsPaid);
        }

        [Fact]
        public void Should_Throw_Exception_When_Student_Is_Null()
        {
            // Arrange
            Student student = null;
            var course = new Course("Math 101", 100.00m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));
            var isPaid = false;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Enrollment(student, course, isPaid));
            Assert.Equal("Value cannot be null. (Parameter 'student')", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Course_Is_Null()
        {
            // Arrange
            var student = new Student("John Doe", 18);
            Course course = null;
            var isPaid = false;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Enrollment(student, course, isPaid));
            Assert.Equal("Value cannot be null. (Parameter 'course')", exception.Message);
        }
    }
}