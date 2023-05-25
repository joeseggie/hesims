namespace HESIMS.Web.Data.Services;

/// <summary>
/// Application cycle course service interface.
/// </summary>
public interface IApplicationCycleCourseService
{
    /// <summary>
    /// Get application cycle courses.
    /// </summary>
    /// <param name="applicationCycleId">Application cycle ID.</param>
    /// <returns>List of application cycle courses.</returns>
    Task<IEnumerable<ApplicationCycleCourse>> GetApplicationCycleCoursesAsync(Guid? applicationCycleId = null);

    /// <summary>
    /// Add new application cycle course.
    /// </summary>
    /// <param name="applicationCycleCourse">New application cycle course details</param>
    Task AddApplicationCycleCourseAsync(ApplicationCycleCourse applicationCycleCourse);

    /// <summary>
    /// Update application cycle course details.
    /// </summary>
    /// <param name="applicationCycleCourse">Application cycle course update details.</param>
    /// <returns>updated application cycle course details.</returns>
    Task<ApplicationCycleCourse> UpdateApplicationCycleCourseAsync(ApplicationCycleCourse applicationCycleCourse);

    /// <summary>
    /// Get application cycle course by Id.
    /// </summary>
    /// <param name="id">Application cycle course ID.</param>
    /// <returns>Application cycle course if ID exist, otherwise null</returns>
    Task<ApplicationCycleCourse?> GetApplicationCycleCourseByIdAsync(Guid id);

    /// <summary>
    /// Delete application cycle course by Id.
    /// </summary>
    Task DeleteApplicationCycleCourseAsync(Guid id);    
}

/// <summary>
/// Application cycle course service.
/// </summary>
public class ApplicationCycleCourseService : IApplicationCycleCourseService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="ApplicationCycleCourseService"/> class.
    /// </summary>
    /// <param name="dbContext">Application DB context.</param>
    public ApplicationCycleCourseService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ApplicationCycleCourse>> GetApplicationCycleCoursesAsync(Guid? applicationCycleId = null)
    {
        var applicationCycleCourses = db.ApplicationCycleCourses
                                        .Include(applicationCycleCourse => applicationCycleCourse.Course)
                                        .Include(applicationCycleCourse => applicationCycleCourse.ApplicationCycle).AsQueryable();
        if (applicationCycleId != null)
        {
            applicationCycleCourses = applicationCycleCourses.Where(applicationCycleCourse => applicationCycleCourse.ApplicationCycleId == applicationCycleId);
        }

        return await applicationCycleCourses.ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task AddApplicationCycleCourseAsync(ApplicationCycleCourse applicationCycleCourse)
    {
        await db.ApplicationCycleCourses.AddAsync(applicationCycleCourse);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<ApplicationCycleCourse> UpdateApplicationCycleCourseAsync(ApplicationCycleCourse applicationCycleCourse)
    {
        var existingApplicationCycleCourse = await GetApplicationCycleCourseByIdAsync(applicationCycleCourse.Id);
        if (existingApplicationCycleCourse == null)
        {
            throw new ArgumentException($"Application cycle course with id {applicationCycleCourse.Id} does not exist.");
        }

        db.Entry(existingApplicationCycleCourse).CurrentValues.SetValues(applicationCycleCourse);
        await db.SaveChangesAsync();
        return applicationCycleCourse;
    }

    /// <inheritdoc/>
    public async Task<ApplicationCycleCourse?> GetApplicationCycleCourseByIdAsync(Guid id)
    {
        return await db.ApplicationCycleCourses
                       .Include(applicationCycleCourse => applicationCycleCourse.Course)
                       .Include(applicationCycleCourse => applicationCycleCourse.ApplicationCycle)
                       .FirstOrDefaultAsync(applicationCycleCourse => applicationCycleCourse.Id == id);
    }

    /// <inheritdoc/>
    public async Task DeleteApplicationCycleCourseAsync(Guid id)
    {
        await db.Database.ExecuteSqlRawAsync($"DELETE FROM ApplicationCycleCourses WHERE Id = '{id}'");
    }
}