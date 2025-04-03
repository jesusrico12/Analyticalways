using ACME.SchoolManagement.Core.Domain.Entities;
using System;

namespace ACME.SchoolManagement.Core.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing operations related to Student entities.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Adds a new student to the repository.
        /// </summary>
        /// <param name="student">The student entity to add.</param>
        void Add(Student student);

        /// <summary>
        /// Retrieves a student by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <returns>
        /// The student associated with the given identifier, or null if no such student exists.
        /// </returns>
        Student? GetById(Guid id);
    }
}
