namespace ACME.SchoolManagement.Core.Domain.Entities
{
    /// <summary>
    /// Represents a course within the school management system.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Gets the unique identifier of the course.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name of the course.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the registration fee for the course.
        /// </summary>
        public decimal RegistrationFee { get; private set; }

        /// <summary>
        /// Gets the starting date of the course.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the ending date of the course.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        /// <param name="name">The name of the course.</param>
        /// <param name="registrationFee">The registration fee for the course. Must be a positive value.</param>
        /// <param name="startDate">The starting date of the course.</param>
        /// <param name="endDate">The ending date of the course.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="name"/> is null, empty, or whitespace; 
        /// <paramref name="registrationFee"/> is less than 0; 
        /// <paramref name="startDate"/> is not before <paramref name="endDate"/>; 
        /// or <paramref name="startDate"/> is in the past.
        /// </exception>
        public Course(string name, decimal registrationFee, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(name));

            if (registrationFee < 0)
                throw new ArgumentException("Registration fee must be a positive value.", nameof(registrationFee));

            if (startDate >= endDate)
                throw new ArgumentException("Start date must be before the end date.", nameof(startDate));

            if (startDate < DateTime.UtcNow)
                throw new ArgumentException("The course must start in the future.", nameof(startDate));

            Id = Guid.NewGuid();
            Name = name;
            RegistrationFee = registrationFee;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
