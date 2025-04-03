using ACME.SchoolManagement.Core.Domain.Entities;

namespace ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a course along with its enrolled students.
    /// </summary>
    public class CourseWithStudentsDto
    {
        /// <summary>
        /// The unique identifier of the course.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// The name of the course.
        /// </summary>
        public required string CourseName { get; set; }

        /// <summary>
        /// The start date of the course.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the course.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// A list of students enrolled in the course.
        /// </summary>
        public List<Student> Students { get; set; } = new();
    }
}
