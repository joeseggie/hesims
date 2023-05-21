namespace HESIMS.Web.Data.Services;

/// <summary>
/// Application cycle service interface.
/// </summary>
public interface IApplicationCycleService
{
    /// <summary>
    /// Get application cycles.
    /// </summary>
    Task<IEnumerable<ApplicationCycle>> GetApplicationCyclesAsync();

    /// <summary>
    /// Add new application cycle.
    /// </summary>
    /// <param name="applicationCycle">New application cycle details</param>
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
    public async Task<IEnumerable<ApplicationCycle>> GetApplicationCyclesAsync()
    {
        return await db.ApplicationCycles.ToListAsync();
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