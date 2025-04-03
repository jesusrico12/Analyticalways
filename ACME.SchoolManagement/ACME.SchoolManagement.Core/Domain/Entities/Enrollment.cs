namespace ACME.SchoolManagement.Core.Domain.Entities
{
    /// <summary>
    /// Represents an enrollment of a student in a specific course.
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// Gets the unique identifier of the enrollment.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the student associated with this enrollment.
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Gets the course associated with this enrollment.
        /// </summary>
        public Course Course { get; private set; }

        /// <summary>
        /// Gets the date and time when the enrollment was created.
        /// </summary>
        public DateTime EnrollmentDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the enrollment has been paid for.
        /// </summary>
        public bool IsPaid { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enrollment"/> class.
        /// </summary>
        /// <param name="student">The student being enrolled.</param>
        /// <param name="course">The course to which the student is enrolled.</param>
        /// <param name="isPaid">Indicates whether the enrollment is paid.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="student"/> or <paramref name="course"/> is null.
        /// </exception>
        public Enrollment(Student student, Course course, bool isPaid)
        {
            Student = student ?? throw new ArgumentNullException(nameof(student));
            Course = course ?? throw new ArgumentNullException(nameof(course));
            EnrollmentDate = DateTime.UtcNow;
            IsPaid = isPaid;
        }
    }
}