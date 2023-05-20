namespace HESIMS.Web.Data.Services;

/// <summary>
/// Scholarship service interface.
/// </summary>
public interface IScholarshipService
{
    /// <summary>
    /// Get scholarships.
    /// </summary>
    /// <returns>List of scholarships</returns>
    Task<IEnumerable<Scholarship>> GetScholarshipsAsync();

    /// <summary>
    /// Add scholarship.
    /// </summary>
    /// <param name="scholarship">Scholarship to be added.</param>
    Task AddScholarshipAsync(Scholarship scholarship);

    /// <summary>
    /// Get scholarship given the ID.
    /// </summary>
    /// <param name="id">Scholarship ID.</param>
    /// <returns>Scholarship if ID exists; otherwise null.</returns>
    Task<Scholarship?> GetScholarshipByIdAsync(Guid id);

    /// <summary>
    /// Update scholarship.
    /// </summary>
    /// <param name="scholarship">Scholarship update to be saved.</param>
    /// <returns>Updated scholarship.</returns>
    Task<Scholarship> UpdateScholarshipAsync(Scholarship scholarship);

    /// <summary>
    /// Get scholarships offered by a country.
    /// </summary>
    /// <param name="country">Country name.</param>
    /// <returns>List of scholarships.</returns>
    Task<IEnumerable<Scholarship>> GetScholarshipsByCountryAsync(string country);
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
    public async Task AddScholarshipAsync(Scholarship scholarship)
    {
        await db.Scholarships.AddAsync(scholarship);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<Scholarship?> GetScholarshipByIdAsync(Guid id)
    {
        return await db.Scholarships.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Scholarship>> GetScholarshipsAsync()
    {
        return await db.Scholarships.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Scholarship> UpdateScholarshipAsync(Scholarship scholarship)
    {
        var existingScholarship = await GetScholarshipByIdAsync(scholarship.Id);
        if (existingScholarship == null)
        {
            throw new ArgumentException($"Scholarship with ID {scholarship.Id} does not exist.");
        }

        db.Entry(existingScholarship).CurrentValues.SetValues(scholarship);
        await db.SaveChangesAsync();
        return scholarship;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Scholarship>> GetScholarshipsByCountryAsync(string country)
    {
        return await db.Scholarships
                       .Where(scholarship => scholarship.Country == country)
                       .ToListAsync();
    }
}