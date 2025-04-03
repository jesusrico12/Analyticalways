using ACME.SchoolManagement.Core.Domain.Entities;
using System;

namespace ACME.SchoolManagement.Core.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing operations related to Course entities.
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Adds a new course to the repository.
        /// </summary>
        /// <param name="course">The course entity to add.</param>
        void Add(Course course);

        /// <summary>
        /// Retrieves a course by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course.</param>
        /// <returns>
        /// The course associated with the given identifier, or null if no such course exists.
        /// </returns>
        Course? GetById(Guid id);
    }
}
