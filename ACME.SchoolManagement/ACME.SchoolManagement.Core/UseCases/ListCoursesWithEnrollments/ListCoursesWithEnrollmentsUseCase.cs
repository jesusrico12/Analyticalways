using ACME.SchoolManagement.Core.Domain.Interfaces;
using ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments.Dtos;
using AutoMapper;

namespace ACME.SchoolManagement.Core.UseCases.ListCoursesWithEnrollments
{
    /// <summary>
    /// Use case for listing courses along with their enrolled students based on a date range.
    /// </summary>
    public class ListCoursesWithEnrollmentsUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCoursesWithEnrollmentsUseCase"/> class.
        /// </summary>
        /// <param name="enrollmentRepository">The repository for accessing enrollments.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public ListCoursesWithEnrollmentsUseCase(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Executes the use case to fetch courses and their enrolled students within a specific date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>A collection of courses with their enrolled students.</returns>
        public IEnumerable<CourseWithStudentsDto> Execute(DateTime startDate, DateTime endDate)
        {
            var enrollments = _enrollmentRepository.GetEnrollmentsBetweenDates(startDate, endDate);
            var groupedEnrollments = enrollments.GroupBy(e => e.Course);

            return groupedEnrollments.Select(group =>
            {
                var courseDto = _mapper.Map<CourseWithStudentsDto>(group.Key);
                courseDto.Students = group.Select(e => e.Student).ToList();
                return courseDto;
            });
        }
    }
}
