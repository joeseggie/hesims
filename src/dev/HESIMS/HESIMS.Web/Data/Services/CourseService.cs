namespace HESIMS.Web.Data.Services;

/// <summary>
/// Course service interface.
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Get courses.
    /// </summary>
    /// <returns>List of courses.</returns>
    Task<IEnumerable<Course>> GetCoursesAsync();

    /// <summary>
    /// Add new course.
    /// </summary>
    /// <param name="course">New course details</param>
    Task AddCourseAsync(Course course);

    /// <summary>
    /// Update course details.
    /// </summary>
    /// <param name="course">Course update details.</param>
    /// <returns>updated course details.</returns>
    Task<Course> UpdateCourseAsync(Course course);

    /// <summary>
    /// Get course by Id.
    /// </summary>
    /// <param name="id">Course ID.</param>
    /// <returns>Course if ID exist, otherwise null</returns>
    Task<Course?> GetCourseByIdAsync(Guid id);
}

/// <summary>
/// Course service.
/// </summary>
public class CourseService : ICourseService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="CourseService"/> class.
    /// </summary>
    public CourseService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await db.Courses.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task AddCourseAsync(Course course)
    {
        await db.Courses.AddAsync(course);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<Course> UpdateCourseAsync(Course course)
    {
        var existingCourse = await db.Courses.FindAsync(course.Id);
        if (existingCourse == null)
        {
            throw new ArgumentException($"Course with ID {course.Id} does not exist.");
        }

        db.Entry(existingCourse).CurrentValues.SetValues(course);
        await db.SaveChangesAsync();
        return course;
    }

    /// <inheritdoc/>
    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await db.Courses.FindAsync(id);
    }
}