using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing courses.
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        /// <summary>
        /// List to store course entities.
        /// </summary>
        private readonly List<Course> _courses = new();

        /// <summary>
        /// Adds a new course to the repository.
        /// </summary>
        /// <param name="course">The course entity to add.</param>
        public void Add(Course course) => _courses.Add(course);

        /// <summary>
        /// Retrieves a course by its unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the course.</param>
        /// <returns>
        /// The course corresponding to the given identifier, or null if not found.
        /// </returns>
        public Course? GetById(Guid id) => _courses.FirstOrDefault(c => c.Id == id);
    }
}
