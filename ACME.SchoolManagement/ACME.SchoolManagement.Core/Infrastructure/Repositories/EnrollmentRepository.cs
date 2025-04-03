using ACME.SchoolManagement.Core.Domain.Entities;
using ACME.SchoolManagement.Core.Domain.Interfaces;

namespace ACME.SchoolManagement.Core.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing enrollments.
    /// </summary>
    public class EnrollmentRepository : IEnrollmentRepository
    {
        /// <summary>
        /// List to store enrollment entities.
        /// </summary>
        private readonly List<Enrollment> _enrollments = new();

        /// <summary>
        /// Adds a new enrollment to the repository.
        /// </summary>
        /// <param name="enrollment">The enrollment entity to add.</param>
        public void Add(Enrollment enrollment) => _enrollments.Add(enrollment);

        /// <summary>
        /// Retrieves enrollments that occurred between two specific dates.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>
        /// A list of enrollments within the specified date range.
        /// </returns>
        public IEnumerable<Enrollment> GetEnrollmentsBetweenDates(DateTime startDate, DateTime endDate) =>
            _enrollments.Where(e => e.EnrollmentDate >= startDate && e.EnrollmentDate <= endDate).ToList();
    }
}
