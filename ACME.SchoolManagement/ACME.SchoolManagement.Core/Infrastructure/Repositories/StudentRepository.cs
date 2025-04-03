using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing student entities.
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// List to store student entities.
        /// </summary>
        private readonly List<Student> _students = new();

        /// <summary>
        /// Adds a new student to the repository.
        /// </summary>
        /// <param name="student">The student entity to add.</param>
        public void Add(Student student) => _students.Add(student);

        /// <summary>
        /// Retrieves a student by their unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <returns>
        /// The student corresponding to the given identifier, or null if not found.
        /// </returns>
        public Student? GetById(Guid id) => _students.FirstOrDefault(s => s.Id == id);
    }
}
