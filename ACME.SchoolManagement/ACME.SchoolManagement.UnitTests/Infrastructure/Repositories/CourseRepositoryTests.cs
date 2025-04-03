using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Infrastructure.Repositories;


namespace ACME.SchoolManagement.UnitTests.Infrastructure.Repositories
{
    public class CourseRepositoryTests
    {
        [Fact]
        public void Should_Add_And_Retrieve_Course_By_Id()
        {
            // Arrange
            var repository = new CourseRepository();
            var course = new Course("Math 101", 100.00m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(10));

            // Act
            repository.Add(course);
            var retrievedCourse = repository.GetById(course.Id);

            // Assert
            Assert.NotNull(retrievedCourse);
            Assert.Equal(course.Id, retrievedCourse.Id);
            Assert.Equal("Math 101", retrievedCourse.Name);
        }

        [Fact]
        public void Should_Return_Null_When_Course_With_Id_Not_Exists()
        {
            // Arrange
            var repository = new CourseRepository();
            var nonExistentId = Guid.NewGuid();

            // Act
            var retrievedCourse = repository.GetById(nonExistentId);

            // Assert
            Assert.Null(retrievedCourse);
        }
    }
}