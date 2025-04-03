using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.UseCases.EnrollStudent
{
    /// <summary>
    /// Use case for enrolling a student in a course.
    /// </summary>
    public class EnrollStudentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPaymentGateway _paymentGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnrollStudentUseCase"/> class.
        /// </summary>
        /// <param name="enrollmentRepository">The repository for managing enrollments.</param>
        /// <param name="studentRepository">The repository for managing students.</param>
        /// <param name="courseRepository">The repository for managing courses.</param>
        /// <param name="paymentGateway">The payment gateway for processing payments.</param>
        public EnrollStudentUseCase(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IPaymentGateway paymentGateway)
        {
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
        }

        /// <summary>
        /// Executes the use case to enroll a student into a course.
        /// </summary>
        /// <param name="studentId">The unique identifier of the student.</param>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>The newly created enrollment entity.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the student or course does not exist, or if payment fails.</exception>
        public Enrollment Execute(Guid studentId, Guid courseId)
        {
            var student = _studentRepository.GetById(studentId)
                          ?? throw new InvalidOperationException("Student not found");

            var course = _courseRepository.GetById(courseId)
                         ?? throw new InvalidOperationException("Course not found");

            if (course.RegistrationFee > 0)
            {
                if (!_paymentGateway.ProcessPayment(student.Id, course.RegistrationFee))
                    throw new InvalidOperationException("Payment failed");
            }

            var enrollment = new Enrollment(student, course, course.RegistrationFee == 0);
            _enrollmentRepository.Add(enrollment);
            return enrollment;
        }
    }
}
