using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Infrastructure.Repositories;

namespace ACME.SchoolManagement.UnitTests.Infrastructure.Repositories
{
    public class StudentRepositoryTests
    {
        [Fact]
        public void Should_Add_And_Retrieve_Student_By_Id()
        {
            // Arrange
            var repository = new StudentRepository();
            var student = new Student("John Doe", 18);

            // Act
            repository.Add(student);
            var retrievedStudent = repository.GetById(student.Id);

            // Assert
            Assert.NotNull(retrievedStudent);
            Assert.Equal(student.Id, retrievedStudent.Id);
            Assert.Equal("John Doe", retrievedStudent.Name);
        }

        [Fact]
        public void Should_Return_Null_When_Student_With_Id_Not_Exists()
        {
            // Arrange
            var repository = new StudentRepository();
            var nonExistentId = Guid.NewGuid();

            // Act
            var retrievedStudent = repository.GetById(nonExistentId);

            // Assert
            Assert.Null(retrievedStudent);
        }
    }
}