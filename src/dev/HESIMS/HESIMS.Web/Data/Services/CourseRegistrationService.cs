namespace HESIMS.Web.Data.Services;

public interface ICourseRegistrationService
{
    /// <summary>
    /// Add new course registration.
    /// </summary>
    /// <param name="newCourseRegistration">New course registration details.</param>
    /// <returns>New course registration details.</returns>
    Task<Result<CourseRegistration>> AddCourseRegistrationAsync(CourseRegistration newCourseRegistration);

    /// <summary>
    /// Update course registration details.
    /// </summary>
    /// <param name="courseRegistrationUpdate">Course registration update details.</param>
    /// <returns>Updated course registration details.</returns>
    Task<Result<CourseRegistration>> UpdateCourseRegistrationAsync(CourseRegistration courseRegistrationUpdate);

    /// <summary>
    /// Get course registration by Id.
    /// </summary>
    /// <param name="id">Course registration ID.</param>
    /// <returns>Course registration if ID exist, otherwise failure result.</returns>
    Task<Result<CourseRegistration>> GetCourseRegistrationByIdAsync(Guid id);

    /// <summary>
    /// Get all course registrations.
    /// </summary>
    /// <returns>List of course registrations.</returns>
    Task<Result<IEnumerable<CourseRegistration>>> GetCourseRegistrationsAsync();
}

/// <summary>
/// Course registration service.
/// </summary>
public class CourseRegistrationService : ICourseRegistrationService
{
    private readonly ApplicationDbContext db;

    /// <summary>
    /// Instantiates a new instance of <see cref="CourseRegistrationService"/>.
    /// </summary>
    public CourseRegistrationService(ApplicationDbContext dbContext)
    {
        this.db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<CourseRegistration>> AddCourseRegistrationAsync(CourseRegistration newCourseRegistration)
    {
        var entityEntry = await db.CourseRegistrations.AddAsync(newCourseRegistration);
        await db.SaveChangesAsync();

        return Result<CourseRegistration>.Success(entityEntry.Entity);
    }

    /// <inheritdoc/>
    public async Task<Result<CourseRegistration>> UpdateCourseRegistrationAsync(CourseRegistration courseRegistrationUpdate)
    {
        var courseRegistrationToUpdate = await db.CourseRegistrations.FindAsync(courseRegistrationUpdate.Id);
        if (courseRegistrationToUpdate == null)
        {
            return Result<CourseRegistration>.Failure($"Course registration with ID {courseRegistrationUpdate.Id} not found.");
        }

        db.Entry(courseRegistrationToUpdate).CurrentValues.SetValues(courseRegistrationUpdate);
        await db.SaveChangesAsync();

        return Result<CourseRegistration>.Success(courseRegistrationToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<CourseRegistration>> GetCourseRegistrationByIdAsync(Guid id)
    {
        var courseRegistration = await db.CourseRegistrations.FindAsync(id);
        if (courseRegistration == null)
        {
            return Result<CourseRegistration>.Failure($"Course registration with ID {id} not found.");
        }

        return Result<CourseRegistration>.Success(courseRegistration);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<CourseRegistration>>> GetCourseRegistrationsAsync()
    {
        var courseRegistrations = await db.CourseRegistrations.ToListAsync();
        return Result<IEnumerable<CourseRegistration>>.Success(courseRegistrations);
    }
}