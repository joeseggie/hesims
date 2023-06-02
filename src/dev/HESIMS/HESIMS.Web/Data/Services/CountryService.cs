namespace HESIMS.Web.Data.Services;

/// <summary>
/// Country service interface.
/// </summary>
public interface ICountryService
{
    /// <summary>
    /// Add new country.
    /// </summary>
    /// <param name="country">New country details</param>
    /// <returns>New country details.</returns>
    Task<Result<Country>> AddCountryAsync(Country country);

    /// <summary>
    /// Get countries list.
    /// </summary>
    /// <returns>List of countries.</returns>
    Task<Result<IEnumerable<Country>>> GetCountriesAsync();

    /// <summary>
    /// Get country by Id.
    /// </summary>
    Task<Result<Country?>> GetCountryByIdAsync(Guid id);

    /// <summary>
    /// Country to be updated.
    /// </summary>
    /// <param name="country">Country to be updated.</param>
    /// <returns>Country with the saved changes.</returns>
    Task<Result<Country?>> UpdateCountryAsync(Country country);
}

/// <summary>
/// Country service.
/// </summary>
public class CountryService : ICountryService
{
    /// <summary>
    /// Application DB context.
    /// </summary>
    private readonly ApplicationDbContext dbContext;

    ///<summary>
    /// Initializes a new instance of the <see cref="CountryService"/> class.
    /// </summary>
    /// <param name="dbContext">ApplicationDbContext.</param>
    public CountryService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<Country>> AddCountryAsync(Country country)
    {
        dbContext.Countries.Add(country);
        await dbContext.SaveChangesAsync();

        return Result<Country>.Success(country);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<Country>>> GetCountriesAsync()
    {
        var countries = await dbContext.Countries
                                       .Include(country => country.Scholarships)
                                       .OrderBy(country => country.Name)
                                       .ToListAsync();

        return Result<IEnumerable<Country>>.Success(countries);
    }

    /// <inheritdoc/>
    public async Task<Result<Country?>> GetCountryByIdAsync(Guid id)
    {
        var country = await dbContext.Countries
                                     .Include(country => country.Scholarships)
                                     .FirstOrDefaultAsync(country => country.Id == id);

        return Result<Country?>.Success(country);
    }

    /// <inheritdoc/>
    public async Task<Result<Country?>> UpdateCountryAsync(Country country)
    {
        var existingCountry = await dbContext.Countries.FindAsync(country.Id);
        if (existingCountry == null)
        {
            return Result<Country?>.Failure($"Country with ID {country.Id} does not exist.");
        }

        dbContext.Entry(existingCountry).CurrentValues.SetValues(country);
        await dbContext.SaveChangesAsync();

        return Result<Country?>.Success(country);
    }
}