namespace HESIMS.Web.Data.Services;

/// <summary>
/// Course service interface.
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Get courses.
    /// </summary>
    /// <param name="courseLevelId">Course level ID.</param>
    /// <param name="institutionId">Institution ID.</param>
    /// <returns>List of courses.</returns>
    Task<Result<IEnumerable<Course>>> GetCoursesAsync(Guid? courseLevelId = null, Guid? institutionId = null);

    /// <summary>
    /// Add new course.
    /// </summary>
    /// <param name="newCourse">New course details</param>
    Task<Result<Course>> AddCourseAsync(Course newCourse);

    /// <summary>
    /// Update course details.
    /// </summary>
    /// <param name="courseUpdate">Course update details.</param>
    /// <returns>updated course details.</returns>
    Task<Result<Course>> UpdateCourseAsync(Course courseUpdate);

    /// <summary>
    /// Get course by Id.
    /// </summary>
    /// <param name="id">Course ID.</param>
    /// <returns>Course if ID exist, otherwise null</returns>
    Task<Result<Course?>> GetCourseByIdAsync(Guid id);
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
    public async Task<Result<IEnumerable<Course>>> GetCoursesAsync(Guid? courseLevelId = null, Guid? institutionId = null)
    {
        var courses = db.Courses
                        .Include(course => course.CourseLevel)
                        .Include(course => course.Institution)
                        .ThenInclude(institution => institution!.Country)
                        .AsQueryable();

        if (courseLevelId != null)
        {
            courses = courses.Where(course => course.CourseLevelId == courseLevelId);
        }

        if (institutionId != null)
        {
            courses = courses.Where(course => course.InstitutionId == institutionId);
        }

        return Result<IEnumerable<Course>>.Success(await courses.ToListAsync());
    }


    /// <inheritdoc/>
    public async Task<Result<Course>> AddCourseAsync(Course newCourse)
    {
        var escapedCourseName = newCourse.Name?.ToLower();
        var isDuplicateCourseName = db.Institutions
                                      .Any(institution => institution.Name != null && institution.Name.ToLower() == escapedCourseName);
        if (isDuplicateCourseName)
        {
            return Result<Course>.Failure($"Course with name {newCourse.Name} already exists.");
        }

        db.Courses.Add(newCourse);
        await db.SaveChangesAsync();

        return Result<Course>.Success(newCourse);
    }

    /// <inheritdoc/>
    public async Task<Result<Course>> UpdateCourseAsync(Course courseUpdate)
    {
        var courseToUpdate = await db.Courses.FindAsync(courseUpdate.Id);
        if (courseToUpdate == null)
        {
            return Result<Course>.Failure($"Course with ID {courseUpdate.Id} does not exist.");
        }

        db.Entry(courseToUpdate).CurrentValues.SetValues(courseUpdate);
        await db.SaveChangesAsync();

        return Result<Course>.Success(courseToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<Course?>> GetCourseByIdAsync(Guid id)
    {
        var course = await db.Courses
                             .Include(course => course.CourseLevel)
                             .Include(course => course.Institution)
                             .ThenInclude(institution => institution!.Country)
                             .FirstOrDefaultAsync(course => course.Id == id);
        if (course == null)
        {
            return Result<Course?>.Failure($"Course with ID {id} does not exist.");
        }

        return Result<Course?>.Success(course);
    }
}