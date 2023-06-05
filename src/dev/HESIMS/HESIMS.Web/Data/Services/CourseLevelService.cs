namespace HESIMS.Web.Data.Services;

/// <summary>
/// Course level service interface.
/// </summary>
public interface ICourseLevelService
{
    /// <summary>
    /// Get course levels.
    /// </summary>
    /// <returns>List of course levels.</returns>
    Task<Result<IEnumerable<CourseLevel>>> GetCourseLevelsAsync();

    /// <summary>
    /// Add new course level.
    /// </summary>
    /// <param name="newCourseLevel">New course level details</param>
    Task<Result<CourseLevel>> AddCourseLevelAsync(CourseLevel newCourseLevel);

    /// <summary>
    /// Update course level details.
    /// </summary>
    /// <param name="courseLevelUpdate">Course level update details.</param>
    /// <returns>updated course level details.</returns>
    Task<Result<CourseLevel>> UpdateCourseLevelAsync(CourseLevel courseLevelUpdate);

    /// <summary>
    /// Get course level by Id.
    /// </summary>
    /// <param name="id">Course level ID.</param>
    /// <returns>Course level if ID exist, otherwise null</returns>
    Task<Result<CourseLevel?>> GetCourseLevelByIdAsync(Guid id);
}

/// <summary>
/// Course level service.
/// </summary>
public class CourseLevelService : ICourseLevelService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="CourseLevelService"/> class.
    /// </summary>
    public CourseLevelService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<CourseLevel>>> GetCourseLevelsAsync()
    {
        var courseLevels = await db.CourseLevels.ToListAsync();
        return Result<IEnumerable<CourseLevel>>.Success(courseLevels);
    }

    /// <inheritdoc/>
    public async Task<Result<CourseLevel>> AddCourseLevelAsync(CourseLevel newCourseLevel)
    {
        var isDuplicate = db.CourseLevels.Any(courseLevel => courseLevel.Description == newCourseLevel.Description);
        if (isDuplicate)
        {
            return Result<CourseLevel>.Failure($"Course level with name {newCourseLevel.Description} already exist.");
        }

        db.CourseLevels.Add(newCourseLevel);
        await db.SaveChangesAsync();
        return Result<CourseLevel>.Success(newCourseLevel);
    }

    /// <inheritdoc/>
    public async Task<Result<CourseLevel>> UpdateCourseLevelAsync(CourseLevel courseLevelUpdate)
    {
        var courseLevelToUpdate = await db.CourseLevels.FindAsync(courseLevelUpdate.Id);
        if (courseLevelToUpdate == null)
        {
            return Result<CourseLevel>.Failure($"Course level with ID {courseLevelUpdate.Id} not found.");
        }
        
        db.Entry(courseLevelToUpdate).CurrentValues.SetValues(courseLevelUpdate);
        await db.SaveChangesAsync();

        return Result<CourseLevel>.Success(courseLevelToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<CourseLevel?>> GetCourseLevelByIdAsync(Guid id)
    {
        var courseLevel = await db.CourseLevels.FindAsync(id);
        if (courseLevel == null)
        {
            return Result<CourseLevel?>.Failure($"Course level with ID {id} not found.");
        }
        
        return Result<CourseLevel?>.Success(courseLevel);
    }
}