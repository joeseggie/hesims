namespace HESIMS.Web.Data.Services;

/// <summary>
/// Applicant service interface.
/// </summary>
public interface IApplicantService
{
    /// <summary>
    /// Get applicants.
    /// </summary>
    /// <returns>List of applicants.</returns>
    Task<IEnumerable<Applicant>> GetApplicantsAsync();

    /// <summary>
    /// Add new applicant.
    /// </summary>
    /// <param name="applicant">New applicant details</param>
    Task AddApplicantAsync(Applicant applicant);

    /// <summary>
    /// Update applicant details.
    /// </summary>
    /// <param name="applicant">Applicant update details.</param>
    /// <returns>updated applicant details.</returns>
    Task<Applicant> UpdateApplicantAsync(Applicant applicant);

    /// <summary>
    /// Get applicant by Id.
    /// </summary>
    /// <param name="id">Applicant ID.</param>
    /// <returns>Applicant if ID exist, otherwise null</returns>
    Task<Applicant?> GetApplicantByIdAsync(Guid id);
}

/// <summary>
/// Applicant service.
/// </summary>
public class ApplicantService : IApplicantService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="ApplicantService"/> class.
    /// </summary>
    /// <param name="dbContext">Application DB context.</param>
    public ApplicantService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Applicant>> GetApplicantsAsync()
    {
        return await db.Applicants.ToListAsync();
    }
    
    /// <inheritdoc/>
    public async Task AddApplicantAsync(Applicant applicant)
    {
        await db.Applicants.AddAsync(applicant);
        await db.SaveChangesAsync();
    }
    
    /// <inheritdoc/>
    public async Task<Applicant> UpdateApplicantAsync(Applicant applicant)
    {
        db.Applicants.Update(applicant);
        await db.SaveChangesAsync();
        return applicant;
    }
    
    /// <inheritdoc/>
    public async Task<Applicant?> GetApplicantByIdAsync(Guid id)
    {
        return await db.Applicants.FindAsync(id);
    }
}
