using ACME.SchoolManagement.Core.Domain.Interfaces;
using AutoMapper;
using Moq;

namespace ACME.SchoolManagement.UnitTests.UseCases
{
    public abstract class UseCaseTestsBase
    {
        protected readonly Mock<ICourseRepository> CourseRepositoryMock;
        protected readonly Mock<IStudentRepository> StudentRepositoryMock;
        protected readonly Mock<IEnrollmentRepository> EnrollmentRepositoryMock;
        protected readonly Mock<IPaymentGateway> PaymentGatewayMock;
        protected readonly Mock<IMapper> MapperMock;

        protected UseCaseTestsBase()
        {
            CourseRepositoryMock = new Mock<ICourseRepository>();
            StudentRepositoryMock = new Mock<IStudentRepository>();
            EnrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            PaymentGatewayMock = new Mock<IPaymentGateway>();
            MapperMock = new Mock<IMapper>();
        }
    }
}
