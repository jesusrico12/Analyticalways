using ACME.SchoolManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ACME.SchoolManagement.Core.Domain.Interfaces
{
    /// <summary>
    /// Interface for managing student enrollments in courses.
    /// </summary>
    public interface IEnrollmentRepository
    {
        /// <summary>
        /// Adds a new enrollment to the repository.
        /// </summary>
        /// <param name="enrollment">The enrollment entity to add.</param>
        void Add(Enrollment enrollment);

        /// <summary>
        /// Retrieves a list of enrollments within a specific date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>
        /// A collection of enrollments within the specified date range.
        /// </returns>
        IEnumerable<Enrollment> GetEnrollmentsBetweenDates(DateTime startDate, DateTime endDate);
    }
}
