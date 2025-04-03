using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.UseCases.RegisterStudent;
using Moq;

namespace ACME.SchoolManagement.UnitTests.UseCases
{
    public class RegisterStudentUseCaseTests : UseCaseTestsBase
    {

        [Fact]
        public void Should_Register_Valid_Student()
        {
            // Arrange
            string name = "John Doe";
            int age = 20;

            StudentRepositoryMock.Setup(r => r.Add(It.IsAny<Student>()));

            var useCase = new RegisterStudentUseCase(StudentRepositoryMock.Object);

            // Act
            var student = useCase.Execute(name, age);

            // Assert
            Assert.NotNull(student);
            Assert.Equal(name, student.Name);
            Assert.Equal(age, student.Age);

            StudentRepositoryMock.Verify(r => r.Add(It.Is<Student>(s => s.Name == name && s.Age == age)), Times.Once);
        }

        [Theory]
        [InlineData(null)]    // Name is null
        [InlineData("")]      // Name is empty
        [InlineData("   ")]   // Name is whitespace
        public void Should_Throw_Exception_When_Name_Is_Invalid(string invalidName)
        {
            // Arrange
            int age = 20;
            var useCase = new RegisterStudentUseCase(StudentRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(invalidName, age));
            Assert.Equal("Student name cannot be null or empty. (Parameter 'name')", exception.Message);
            StudentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void Should_Throw_Exception_When_Age_Is_Less_Than_18()
        {
            // Arrange
            string name = "Jane Doe";
            int invalidAge = 17;
            var useCase = new RegisterStudentUseCase(StudentRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(name, invalidAge));
            Assert.Equal("Student must be at least 18 years old. (Parameter 'age')", exception.Message);
            StudentRepositoryMock.Verify(r => r.Add(It.IsAny<Student>()), Times.Never);
        }
    }
}