using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.UseCases.RegisterCourse
{
    /// <summary>
    /// Use case for registering a new course.
    /// </summary>
    public class RegisterCourseUseCase
    {
        private readonly ICourseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterCourseUseCase"/> class.
        /// </summary>
        /// <param name="repository">The course repository for managing courses.</param>
        public RegisterCourseUseCase(ICourseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Executes the use case to register a new course with specified details.
        /// </summary>
        /// <param name="name">The name of the course.</param>
        /// <param name="registrationFee">The registration fee for the course.</param>
        /// <param name="startDate">The start date of the course.</param>
        /// <param name="endDate">The end date of the course.</param>
        /// <returns>The newly created course entity.</returns>
        public Course Execute(string name, decimal registrationFee, DateTime startDate, DateTime endDate)
        {
            var course = new Course(name, registrationFee, startDate, endDate);
            _repository.Add(course);
            return course;
        }
    }
}
