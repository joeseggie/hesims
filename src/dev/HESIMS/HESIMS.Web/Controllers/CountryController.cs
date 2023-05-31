namespace HESIMS.Web.Controllers;

/// <summary>
/// Country controller.
/// </summary>
[Route("api/countries")]
[ApiController]
public class CountryController : BaseController
{
    private readonly ICountryService countryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountryController"/> class.
    /// </summary>
    public CountryController(ICountryService countryService)
    {
        this.countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountriesAsync()
    {
        var countryQueryResult = await countryService.GetCountriesAsync();
        if (!countryQueryResult.IsSuccess)
        {
            return BadRequest(countryQueryResult);
        }

        var countries = countryQueryResult.Value;
        if (countries != null && countries.Count() > 0)
        {
            return Ok(countries.Select(country => new CountryViewModel
            {
                CountryId = country.Id,
                CountryName = country.Name,
                CountryCode = country.Code
            }));
        }

        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountryByIdAsync(Guid id)
    {
        var countryQueryResult = await countryService.GetCountryByIdAsync(id);
        if (!countryQueryResult.IsSuccess)
        {
            return BadRequest(countryQueryResult);
        }

        var country = countryQueryResult.Value;
        if (country == null)
        {
            return NotFound();
        }

        return Ok(new CountryViewModel
        {
            CountryId = country.Id,
            CountryName = country.Name,
            CountryCode = country.Code
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCountryAsync([FromBody] CountryViewModel country)
    {
        var validationResult = country.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var addCountryResult = await countryService.AddCountryAsync(new Country
        {
            Id = Guid.NewGuid(),
            Name = country.CountryName,
            Code = country.CountryCode
        });
        if (!addCountryResult.IsSuccess)
        {
            return BadRequest(addCountryResult.ErrorMessage);
        }

        var newCountry = addCountryResult.Value;

        return CreatedAtAction(
            nameof(GetCountryByIdAsync),
            new { id = newCountry?.Id },
            new CountryViewModel
            {
                CountryId = newCountry?.Id,
                CountryName = newCountry?.Name,
                CountryCode = newCountry?.Code
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountryAsync(Guid id, [FromBody] CountryViewModel country)
    {
        var validationResult = country.Validate(routeId: id, validateId: true);
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult);
        }

        var countryId = await countryService.UpdateCountryAsync(new Country
        {
            Id = id,
            Name = country.CountryName,
            Code = country.CountryCode
        });

        return Ok(country);
    }
}