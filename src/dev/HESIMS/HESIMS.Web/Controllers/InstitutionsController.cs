namespace HESIMS.Web.Controllers;

/// <summary>
/// Institutions controller.
/// </summary>
[Route("api/institutions")]
[ApiController]
public class InstitutionsController : BaseController
{
    private readonly IInstitutionService institutionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstitutionsController"/> class.
    /// </summary>
    /// <param name="institutionService">Institution service.</param>
    public InstitutionsController(IInstitutionService institutionService)
    {
        this.institutionService = institutionService;
    }

    /// <summary>
    /// Get institutions.
    /// </summary>
    /// <returns>List of institutions.</returns>
    [HttpGet]
    public async Task<IActionResult> GetInstitutionsAsync([FromQuery] Guid? countryId = null)
    {
        var institutionsQueryResult = await institutionService.GetInstitutionsAsync(countryId);
        if (!institutionsQueryResult.IsSuccess)
        {
            return BadRequest(institutionsQueryResult.ErrorMessage);
        }

        var institutions = institutionsQueryResult.Value;
        if (institutions != null && institutions.Any())
        {
            return Ok(institutions.Select(institution => new InstitutionViewModel
            {
                InstitutionId = institution.Id,
                InstitutionName = institution.Name,
                CountryId = institution.CountryId,
                Country = new CountryViewModel
                {
                    CountryId = institution.Country?.Id,
                    CountryName = institution.Country?.Name,
                    CountryCode = institution.Country?.Code
                }
            }));
        }

        return Ok(institutions);
    }

    /// <summary>
    /// Get institution by id.
    /// </summary>
    /// <param name="id">Institution id.</param>
    /// <returns>Institution.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstitutionByIdAsync(Guid id)
    {
        var institutionQueryResult = await institutionService.GetInstitutionByIdAsync(id);
        if (!institutionQueryResult.IsSuccess)
        {
            return BadRequest(institutionQueryResult.ErrorMessage);
        }

        var institution = institutionQueryResult.Value;
        if (institution != null)
        {
            return Ok(new InstitutionViewModel
            {
                InstitutionId = institution.Id,
                InstitutionName = institution.Name,
                CountryId = institution.CountryId,
                Country = new CountryViewModel
                {
                    CountryId = institution.Country?.Id,
                    CountryName = institution.Country?.Name,
                    CountryCode = institution.Country?.Code
                }
            });
        }

        return Ok(institution);
    }

    /// <summary>
    /// Create institution.
    /// </summary>
    /// <param name="institution">New institution to be added.</param>
    /// <returns>Institution that has been added.</returns>
    [HttpPost]
    public async Task<IActionResult> AddInstitutionAsync(InstitutionViewModel institution)
    {
        var validationResult = institution.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var institutionQueryResult = await institutionService.AddInstitutionAsync(new Institution
        {
            Id = Guid.NewGuid(),
            Name = institution.InstitutionName,
            CountryId = institution.CountryId
        });
        if (!institutionQueryResult.IsSuccess)
        {
            return BadRequest(institutionQueryResult.ErrorMessage);
        }

        var newInstitution = institutionQueryResult.Value;
        if (newInstitution != null)
        {
            return Ok(new InstitutionViewModel
            {
                InstitutionId = newInstitution.Id,
                InstitutionName = newInstitution.Name,
                CountryId = newInstitution.CountryId,
                Country = new CountryViewModel
                {
                    CountryId = newInstitution.Country?.Id,
                    CountryName = newInstitution.Country?.Name,
                    CountryCode = newInstitution.Country?.Code
                }
            });
        }

        return Ok(newInstitution);
    }

    /// <summary>
    /// Update institution.
    /// </summary>
    /// <param name="id">Institution id.</param>
    /// <param name="institution">Institution to be updated.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInstitutionAsync(Guid id, [FromBody] InstitutionViewModel institution)
    {
        var validationResult = institution.Validate(routeId: id, validateId: true);
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var institutionQueryResult = await institutionService.UpdateInstitutionAsync(new Institution
        {
            Id = id,
            Name = institution.InstitutionName,
            CountryId = institution.CountryId
        });
        if (!institutionQueryResult.IsSuccess)
        {
            return BadRequest(institutionQueryResult.ErrorMessage);
        }

        var updatedInstitution = institutionQueryResult.Value;

        return Ok(CreatedAtAction(
            nameof(GetInstitutionByIdAsync),
            new { id = updatedInstitution?.Id },
            new InstitutionViewModel
            {
                InstitutionId = updatedInstitution?.Id,
                InstitutionName = updatedInstitution?.Name,
                CountryId = updatedInstitution?.CountryId,
                Country = new CountryViewModel
                {
                    CountryId = updatedInstitution?.Country?.Id,
                    CountryName = updatedInstitution?.Country?.Name,
                    CountryCode = updatedInstitution?.Country?.Code
                }
            }));
    }
}