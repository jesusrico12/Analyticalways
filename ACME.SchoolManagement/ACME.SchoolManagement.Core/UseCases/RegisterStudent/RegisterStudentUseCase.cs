using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.UseCases.RegisterStudent
{
    /// <summary>
    /// Use case for registering a new student.
    /// </summary>
    public class RegisterStudentUseCase
    {
        private readonly IStudentRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterStudentUseCase"/> class.
        /// </summary>
        /// <param name="repository">The student repository for managing students.</param>
        public RegisterStudentUseCase(IStudentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Executes the use case to register a new student with specified details.
        /// </summary>
        /// <param name="name">The name of the student.</param>
        /// <param name="age">The age of the student.</param>
        /// <returns>The newly created student entity.</returns>
        public Student Execute(string name, int age)
        {
            var student = new Student(name, age);
            _repository.Add(student);
            return student;
        }
    }
}
