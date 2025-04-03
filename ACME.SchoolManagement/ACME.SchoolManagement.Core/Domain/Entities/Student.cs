namespace ACME.SchoolManagement.Core.Domain.Entities
{
    /// <summary>
    /// Represents a student within the school management system.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets the unique identifier of the student.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name of the student.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the age of the student.
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        /// <param name="name">The name of the student.</param>
        /// <param name="age">The age of the student. Must be at least 18 years old.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="name"/> is null, empty, or whitespace, or <paramref name="age"/> is less than 18.
        /// </exception>
        public Student(string name, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Student name cannot be null or empty.", nameof(name));

            if (age < 18)
                throw new ArgumentException("Student must be at least 18 years old.", nameof(age));

            Id = Guid.NewGuid();
            Name = name;
            Age = age;
        }
    }
}