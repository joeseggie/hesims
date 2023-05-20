namespace HESIMS.Web.Data.Services;

/// <summary>
/// Course application service interface.
/// </summary>
public interface ICourseApplicationService
{
    /// <summary>
    /// Get course applications.
    /// </summary>
    /// <returns>List of course applications.</returns>
    Task<IEnumerable<CourseApplication>> GetCourseApplicationsAsync();

    /// <summary>
    /// Add new course application.
    /// </summary>
    /// <param name="courseApplication">New course application details</param>
    Task AddCourseApplicationAsync(CourseApplication courseApplication);

    /// <summary>
    /// Update course application details.
    /// </summary>
    /// <param name="courseApplication">Course application update details.</param>
    /// <returns>updated course application details.</returns>
    Task<CourseApplication> UpdateCourseApplicationAsync(CourseApplication courseApplication);

    /// <summary>
    /// Get course application by Id.
    /// </summary>
    /// <param name="id">Course application ID.</param>
    /// <returns>Course application if ID exist, otherwise null</returns>
    Task<CourseApplication?> GetCourseApplicationByIdAsync(Guid id);
}

/// <summary>
/// Course application service.
/// </summary>
public class CourseApplicationService : ICourseApplicationService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="CourseApplicationService"/> class.
    /// </summary>
    public CourseApplicationService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CourseApplication>> GetCourseApplicationsAsync()
    {
        return await db.CourseApplications.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task AddCourseApplicationAsync(CourseApplication courseApplication)
    {
        await db.CourseApplications.AddAsync(courseApplication);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<CourseApplication> UpdateCourseApplicationAsync(CourseApplication courseApplication)
    {
        var existingCourseApplication = await db.CourseApplications.FindAsync(courseApplication.Id);
        if (existingCourseApplication == null)
        {
            throw new ArgumentException($"Course application with id {courseApplication.Id} not found.");
        }

        db.Entry(existingCourseApplication).CurrentValues.SetValues(courseApplication);
        await db.SaveChangesAsync();
        return courseApplication;
    }

    /// <inheritdoc/>
    public async Task<CourseApplication?> GetCourseApplicationByIdAsync(Guid id)
    {
        return await db.CourseApplications.FindAsync(id);
    }
}