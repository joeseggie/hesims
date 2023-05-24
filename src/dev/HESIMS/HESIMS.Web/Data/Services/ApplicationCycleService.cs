namespace HESIMS.Web.Data.Services;

/// <summary>
/// Application cycle service interface.
/// </summary>
public interface IApplicationCycleService
{
    /// <summary>
    /// Get application cycles.
    /// </summary>
    /// <param name="scholarshipId">Scholarship ID.</param>
    /// <param name="academicYear">Academic year.</param>
    /// <param name="status">Application cycle status.</param>
    /// <returns>List of application cycles.</returns>
    Task<IEnumerable<ApplicationCycle>> GetApplicationCyclesAsync(Guid? scholarshipId = null, string? academicYear = null, string? status = null);

    /// <summary>
    /// Add new application cycle.
    /// </summary>
    /// <param name="applicationCycle">New application cycle details</param>
    /// <returns>Added application cycle details.</returns>
    Task AddApplicationCycleAsync(ApplicationCycle applicationCycle);

    /// <summary>
    /// Update application cycle details.
    /// </summary>
    /// <param name="applicationCycle">Application cycle update details.</param>
    /// <returns>updated application cycle details.</returns>
    Task<ApplicationCycle> UpdateApplicationCycleAsync(ApplicationCycle applicationCycle);

    /// <summary>
    /// Get application cycle by Id.
    /// </summary>
    /// <param name="id">Application cycle ID.</param>
    /// <returns>Application cycle if ID exist, otherwise null</returns>
    Task<ApplicationCycle?> GetApplicationCycleByIdAsync(Guid id);
}

public class ApplicationCycleService : IApplicationCycleService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="ApplicationCycleService"/> class.
    /// </summary>
    /// <param name="dbContext">Application DB context.</param>
    public ApplicationCycleService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ApplicationCycle>> GetApplicationCyclesAsync(Guid? scholarshipId = null, string? academicYear = null, string? status = null)
    {
        var applicationCycles = db.ApplicationCycles.AsQueryable();
        if (scholarshipId != null)
        {
            applicationCycles = applicationCycles.Where(applicationCycle => applicationCycle.ScholarshipId == scholarshipId);
        }

        if (academicYear != null)
        {
            applicationCycles = applicationCycles.Where(applicationCycle => applicationCycle.AcademicYear == academicYear);
        }

        if (status != null)
        {
            applicationCycles = applicationCycles.Where(applicationCycle => applicationCycle.Status == status);
        }

        return await applicationCycles.ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task AddApplicationCycleAsync(ApplicationCycle applicationCycle)
    {
        await db.ApplicationCycles.AddAsync(applicationCycle);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<ApplicationCycle> UpdateApplicationCycleAsync(ApplicationCycle applicationCycle)
    {
        var existingApplicationCycle = await db.ApplicationCycles.FindAsync(applicationCycle.Id);
        if (existingApplicationCycle == null)
        {
            throw new ArgumentException($"Application cycle with ID {applicationCycle.Id} does not exist.");
        }

        db.Entry(existingApplicationCycle).CurrentValues.SetValues(applicationCycle);
        await db.SaveChangesAsync();
        return applicationCycle;
    }

    /// <inheritdoc/>
    public async Task<ApplicationCycle?> GetApplicationCycleByIdAsync(Guid id)
    {
        return await db.ApplicationCycles.FindAsync(id);
    }
}