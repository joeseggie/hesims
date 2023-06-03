namespace HESIMS.Web.Data.Services;

/// <summary>
/// Institution service interface.
/// </summary>
public interface IInstitutionService
{
    /// <summary>
    /// Adds an institution.
    /// </summary>
    /// <param name="institution">Institution to add.</param>
    Task<Result<Institution>> AddInstitutionAsync(Institution institution);

    /// <summary>
    /// Updates an institution.
    /// </summary>
    /// <param name="institution">Institution update.</param>
    Task<Result<Institution>> UpdateInstitutionAsync(Institution institution);

    /// <summary>
    /// Get an institution by id.
    /// </summary>
    /// <param name="id">Id of institution to get.</param>
    /// <returns>Institution.</returns>
    Task<Result<Institution>> GetInstitutionByIdAsync(Guid id);

    /// <summary>
    /// Get institutions.
    /// </summary>
    /// <returns>List of institutions.</returns>
    Task<Result<IEnumerable<Institution>>> GetInstitutionsAsync();
}

/// <summary>
/// Institution service.
/// </summary>
public class InstitutionService : IInstitutionService
{
    private readonly ApplicationDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstitutionService"/> class.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    public InstitutionService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<Institution>> AddInstitutionAsync(Institution institution)
    {
        var escapedName = institution.Name?.ToLower();
        var isDuplicateInstitutionName = dbContext.Institutions
                                                  .Any(institution => institution.Name != null && institution.Name.ToLower() == escapedName);
        if (isDuplicateInstitutionName)
        {
            return Result<Institution>.Failure($"Institution with name {institution.Name} already exists.");
        }

        dbContext.Institutions.Add(institution);
        await dbContext.SaveChangesAsync();

        return Result<Institution>.Success(institution);
    }

    /// <inheritdoc/>
    public async Task<Result<Institution>> UpdateInstitutionAsync(Institution institution)
    {
        var institutionInStore = await dbContext.Institutions.FindAsync(institution.Id);
        if (institutionInStore == null)
        {
            return Result<Institution>.Failure($"Institution with id {institution.Id} not found.");
        }

        dbContext.Entry(institutionInStore).CurrentValues.SetValues(institution);
        await dbContext.SaveChangesAsync();

        return Result<Institution>.Success(institutionInStore);
    }

    /// <inheritdoc/>
    public async Task<Result<Institution>> GetInstitutionByIdAsync(Guid id)
    {
        var institution = await dbContext.Institutions
                                         .Include(institution => institution.Country)
                                         .FirstOrDefaultAsync(institution => institution.Id == id);
        if (institution == null)
        {
            return Result<Institution>.Failure($"Institution with id {id} not found.");
        }

        return Result<Institution>.Success(institution);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<Institution>>> GetInstitutionsAsync()
    {
        var institutions = await dbContext.Institutions
                                          .Include(institution => institution.Country)
                                          .OrderBy(institution => institution.Name)
                                          .ToListAsync();

        return Result<IEnumerable<Institution>>.Success(institutions);
    }
}