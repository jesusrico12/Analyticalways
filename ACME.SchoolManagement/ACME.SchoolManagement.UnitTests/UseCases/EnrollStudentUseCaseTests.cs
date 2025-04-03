using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.UseCases.EnrollStudent;

namespace ACME.SchoolManagement.UnitTests.UseCases
{
    public class EnrollStudentUseCaseTests : UseCaseTestsBase
    {
        [Fact]
        public void Should_Enroll_Student_In_Free_Course()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var student = new Student("John Doe", 18);
            var course = new Course("Free Course", 0m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            StudentRepositoryMock.Setup(r => r.GetById(studentId)).Returns(student);
            CourseRepositoryMock.Setup(r => r.GetById(courseId)).Returns(course);

            var useCase = new EnrollStudentUseCase(
                EnrollmentRepositoryMock.Object,
                StudentRepositoryMock.Object,
                CourseRepositoryMock.Object,
                PaymentGatewayMock.Object);

            // Act
            var enrollment = useCase.Execute(studentId, courseId);

            // Assert
            Assert.NotNull(enrollment);
            Assert.Equal(student, enrollment.Student);
            Assert.Equal(course, enrollment.Course);
            Assert.True(enrollment.IsPaid);
        }

        [Fact]
        public void Should_Throw_Exception_When_Student_Not_Found()
        {
            // Arrange
            var studentId = Guid.NewGuid();

            StudentRepositoryMock.Setup(r => r.GetById(studentId)).Returns((Student?)null);

            var useCase = new EnrollStudentUseCase(
                EnrollmentRepositoryMock.Object,
                StudentRepositoryMock.Object,
                CourseRepositoryMock.Object,
                PaymentGatewayMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => useCase.Execute(studentId, Guid.NewGuid()));
            Assert.Equal("Student not found", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Course_Not_Found()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var student = new Student("John Doe", 18);
            var course = new Course("Free Course", 0m, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            var courseId = Guid.NewGuid();

            CourseRepositoryMock.Setup(r => r.GetById(courseId)).Returns((Course?)null);
            StudentRepositoryMock.Setup(r => r.GetById(studentId)).Returns(student);

            var useCase = new EnrollStudentUseCase(
                EnrollmentRepositoryMock.Object,
                StudentRepositoryMock.Object,
                CourseRepositoryMock.Object,
                PaymentGatewayMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => useCase.Execute(studentId, Guid.NewGuid()));
            Assert.Equal("Course not found", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Payment_Fails()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var student = new Student("John Doe", 18);
            var course = new Course("Paid Course", 100m, DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(3));

            StudentRepositoryMock.Setup(r => r.GetById(studentId)).Returns(student);
            CourseRepositoryMock.Setup(r => r.GetById(courseId)).Returns(course);
            PaymentGatewayMock.Setup(pg => pg.ProcessPayment(student.Id, 100m)).Returns(false);

            var useCase = new EnrollStudentUseCase(
                EnrollmentRepositoryMock.Object,
                StudentRepositoryMock.Object,
                CourseRepositoryMock.Object,
                PaymentGatewayMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => useCase.Execute(studentId, courseId));
            Assert.Equal("Payment failed", exception.Message);
        }
    }
}