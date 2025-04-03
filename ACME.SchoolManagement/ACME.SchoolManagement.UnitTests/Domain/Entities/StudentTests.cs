using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.UnitTests.Domain.Entities
{
    public class StudentTests
    {
        [Fact]
        public void Should_Create_Student_When_Valid_Data_Provided()
        {
            // Arrange
            var name = "John Doe";
            var age = 18;

            // Act
            var student = new Student(name, age);

            // Assert
            Assert.NotNull(student);
            Assert.Equal(name, student.Name);
            Assert.Equal(age, student.Age);
        }

        [Theory]
        [InlineData(null)]  // Name is null
        [InlineData("")]    // Name is empty
        [InlineData("   ")] // Name is whitespace
        public void Should_Throw_Exception_When_Name_Is_Invalid(string invalidName)
        {
            // Arrange
            var age = 18;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Student(invalidName, age));
            Assert.Equal("Student name cannot be null or empty. (Parameter 'name')", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Age_Is_Less_Than_18()
        {
            // Arrange
            var name = "Jane Doe";
            var invalidAge = 17;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Student(name, invalidAge));
            Assert.Equal("Student must be at least 18 years old. (Parameter 'age')", exception.Message);
        }
    }
}