using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Infrastructure.Repositories;

namespace ACME.SchoolManagement.UnitTests.Infrastructure.Repositories
{
    public class EnrollmentRepositoryTests
    {
        [Fact]
        public void Should_Add_And_Retrieve_Enrollments_Between_Dates()
        {
            // Arrange
            var repository = new EnrollmentRepository();
            var student = new Student("John Doe", 18);
            var course = new Course("Math 101", 100.00m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));

            var enrollment1 = new Enrollment(student, course, false);
            var enrollment2 = new Enrollment(student, course, true);

            // Act
            repository.Add(enrollment1);
            repository.Add(enrollment2);

            var startDate = enrollment1.EnrollmentDate.AddSeconds(-1);
            var endDate = enrollment2.EnrollmentDate.AddSeconds(1);
            var enrollmentsBetween = repository.GetEnrollmentsBetweenDates(startDate, endDate);

            // Assert
            Assert.NotEmpty(enrollmentsBetween);
            Assert.Equal(2, enrollmentsBetween.Count());
            Assert.Contains(enrollment1, enrollmentsBetween);
            Assert.Contains(enrollment2, enrollmentsBetween);
        }

        [Fact]
        public void Should_Return_Empty_When_No_Enrollments_Between_Dates()
        {
            // Arrange
            var repository = new EnrollmentRepository();
            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow.AddDays(1);

            // Act
            var enrollmentsBetween = repository.GetEnrollmentsBetweenDates(startDate, endDate);

            // Assert
            Assert.Empty(enrollmentsBetween);
        }
    }
}