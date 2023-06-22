namespace HESIMS.Web.Controllers;

/// <summary>
/// Scholarship controller.
/// </summary>
[Route("api/scholarships")]
[ApiController]
public class ScholarshipController : BaseController
{
    private readonly IScholarshipService scholarshipService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScholarshipController"/> class.
    /// </summary>
    /// <param name="scholarshipService">Scholarship service.</param>
    public ScholarshipController(IScholarshipService scholarshipService)
    {
        this.scholarshipService = scholarshipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetScholarshipsAsync([FromQuery] Guid? countryId = null)
    {
        var scholarshipsQueryResult = await scholarshipService.GetScholarshipsAsync(countryId);
        if (!scholarshipsQueryResult.IsSuccess)
        {
            return BadRequest(scholarshipsQueryResult.ErrorMessage);
        }

        var scholarships = scholarshipsQueryResult.Value;
        if (scholarships != null && scholarships.Any())
        {
            return Ok(scholarships.Select(scholarship => new ScholarshipViewModel
            {
                ScholarshipId = scholarship.Id,
                ScholarshipName = scholarship.Name,
                CountryId = scholarship.CountryId,
                Country = new CountryViewModel{
                    CountryId = scholarship.Country?.Id,
                    CountryName = scholarship.Country?.Name,
                    CountryCode = scholarship.Country?.Code
                }
            }));
        }

        return Ok(scholarships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScholarshipByIdAsync(Guid id)
    {
        var scholarshipQueryResult = await scholarshipService.GetScholarshipByIdAsync(id);
        if (!scholarshipQueryResult.IsSuccess)
        {
            return BadRequest(scholarshipQueryResult.ErrorMessage);
        }

        var scholarship = scholarshipQueryResult.Value;
        if (scholarship == null)
        {
            return NotFound();
        }

        return Ok(new ScholarshipViewModel
        {
            ScholarshipId = scholarship.Id,
            ScholarshipName = scholarship.Name,
            CountryId = scholarship.CountryId,
            Country = new CountryViewModel{
                CountryId = scholarship.Country?.Id,
                CountryName = scholarship.Country?.Name,
                CountryCode = scholarship.Country?.Code
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddScholarshipAsync([FromBody] ScholarshipViewModel scholarship)
    {
        var validationResult = scholarship.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var newScholarship = new Scholarship
        {
            Id = Guid.NewGuid(),
            Name = scholarship.ScholarshipName,
            CountryId = scholarship.CountryId
        };

        try
        {
            await scholarshipService.AddScholarshipAsync(newScholarship);
        }
        catch (DbUpdateException error)
        {
            if (error.InnerException is SqlException sqlException && (sqlException.Number == 2601 || sqlException.Number == 2627))
            {
                return BadRequest("Scholarship already exists.");
            }
        }
        
        return CreatedAtAction(
            nameof(GetScholarshipByIdAsync),
            new { id = newScholarship.Id },
            new ScholarshipViewModel
            {
                ScholarshipId = newScholarship.Id,
                ScholarshipName = newScholarship.Name,
                CountryId = newScholarship.CountryId
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScholarshipAsync(Guid id, [FromBody] ScholarshipViewModel scholarship)
    {
        var validationResult = scholarship.Validate(routeScholarshipId: id, validateId: true);
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var updateScholarshipResult = await scholarshipService.UpdateScholarshipAsync(new Scholarship
        {
            Id = id,
            Name = scholarship.ScholarshipName,
            CountryId = scholarship.CountryId
        });
        if (!updateScholarshipResult.IsSuccess)
        {
            return BadRequest(updateScholarshipResult.ErrorMessage);
        }

        var updatedScholarship = updateScholarshipResult.Value;
        return Ok(new ScholarshipViewModel
        {
            ScholarshipId = updatedScholarship?.Id,
            ScholarshipName = updatedScholarship?.Name,
            CountryId = updatedScholarship?.CountryId
        });
    }
}