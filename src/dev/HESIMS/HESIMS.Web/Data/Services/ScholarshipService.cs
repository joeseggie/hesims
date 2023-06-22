namespace HESIMS.Web.Data.Services;

/// <summary>
/// Scholarship service interface.
/// </summary>
public interface IScholarshipService
{
    /// <summary>
    /// Get scholarships.
    /// </summary>
    /// <param name="countryId">Filter for scholarships in a country.</param>
    /// <returns>List of scholarships</returns>
    Task<Result<IEnumerable<Scholarship>>> GetScholarshipsAsync(Guid? countryId = null);

    /// <summary>
    /// Add scholarship.
    /// </summary>
    /// <param name="scholarship">Scholarship to be added.</param>
    Task<Result<Scholarship>> AddScholarshipAsync(Scholarship scholarship);

    /// <summary>
    /// Get scholarship given the ID.
    /// </summary>
    /// <param name="id">Scholarship ID.</param>
    /// <returns>Scholarship if ID exists; otherwise null.</returns>
    Task<Result<Scholarship?>> GetScholarshipByIdAsync(Guid id);

    /// <summary>
    /// Update scholarship.
    /// </summary>
    /// <param name="scholarship">Scholarship update to be saved.</param>
    /// <returns>Updated scholarship.</returns>
    Task<Result<Scholarship>> UpdateScholarshipAsync(Scholarship scholarship);

    /// <summary>
    /// Get scholarships offered by a country.
    /// </summary>
    /// <param name="countryId">Country Id.</param>
    /// <returns>List of scholarships.</returns>
    Task<Result<IEnumerable<Scholarship>>> GetScholarshipsByCountryAsync(Guid countryId);
}

/// <summary>
/// Scholarship service.
/// </summary>
public class ScholarshipService : IScholarshipService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="ScholarshipSerice"/> class.
    /// </summary>
    /// <param name="dbContext">Application DB context.</param>
    public ScholarshipService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc />
    public async Task<Result<Scholarship>> AddScholarshipAsync(Scholarship scholarship)
    {
        var scholarshipEntry = await db.Scholarships.AddAsync(scholarship);
        await db.SaveChangesAsync();

        return Result<Scholarship>.Success(scholarshipEntry.Entity);
    }

    /// <inheritdoc />
    public async Task<Result<Scholarship?>> GetScholarshipByIdAsync(Guid id)
    {
        var scholarship = await db.Scholarships
                                   .Include(scholarship => scholarship.Country)
                                   .FirstOrDefaultAsync(scholarship => scholarship.Id == id);

        return Result<Scholarship?>.Success(scholarship);
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Scholarship>>> GetScholarshipsAsync(Guid? countryId = null)
    {
        var scholarshipsQuery = db.Scholarships.AsQueryable();
        if (countryId is not null)
        {
            scholarshipsQuery = scholarshipsQuery.Where(scholarship => scholarship.CountryId == countryId);
        }
        var scholarships = await scholarshipsQuery
                                   .Include(scholarship => scholarship.Country)
                                   .OrderBy(scholarship => scholarship.Name)
                                   .ToListAsync();

        return Result<IEnumerable<Scholarship>>.Success(scholarships);
    }

    /// <inheritdoc />
    public async Task<Result<Scholarship>> UpdateScholarshipAsync(Scholarship scholarship)
    {
        var existingScholarship = await db.Scholarships.FindAsync(scholarship.Id);
        if (existingScholarship != null)
        {

            db.Entry(existingScholarship).CurrentValues.SetValues(scholarship);
            await db.SaveChangesAsync();
            return Result<Scholarship>.Success(scholarship);
        }

        return Result<Scholarship>.Failure("Scholarship does not exist.");
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<Scholarship>>> GetScholarshipsByCountryAsync(Guid countryId)
    {
        var scholarships = await db.Scholarships
                                   .Include(scholarship => scholarship.Country)
                                   .Where(scholarship => scholarship.CountryId == countryId)
                                   .OrderBy(scholarship => scholarship.Name)
                                   .ToListAsync();

        return Result<IEnumerable<Scholarship>>.Success(scholarships);
    }
}