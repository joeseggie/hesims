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
    public async Task<IActionResult> GetScholarshipsAsync()
    {
        var scholarships = await scholarshipService.GetScholarshipsAsync();
        if (scholarships != null && scholarships.Count() > 0)
        {
            return Ok(scholarships.Select(scholarship => new ScholarshipViewModel
            {
                ScholarshipId = scholarship.Id,
                ScholarshipName = scholarship.Name,
                Country = scholarship.Country
            }));
        }

        return Ok(scholarships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScholarshipByIdAsync(Guid id)
    {
        var scholarship = await scholarshipService.GetScholarshipByIdAsync(id);
        if (scholarship == null)
        {
            return NotFound();
        }

        return Ok(new ScholarshipViewModel
        {
            ScholarshipId = scholarship.Id,
            ScholarshipName = scholarship.Name,
            Country = scholarship.Country
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddScholarshipAsync([FromBody] ScholarshipViewModel scholarship)
    {
        var validationResult = scholarship.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var newScholarship = new Scholarship
        {
            Id = Guid.NewGuid(),
            Name = scholarship.ScholarshipName,
            Country = scholarship.Country
        };

        try
        {
            await scholarshipService.AddScholarshipAsync(newScholarship);
        }
        catch (DbUpdateException error)
        {
            var sqlException = error.InnerException as SqlException;
            if (sqlException != null && (sqlException.Number == 2601 || sqlException.Number == 2627))
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
                Country = newScholarship.Country
            });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateScholarshipAsync(Guid id, [FromBody] ScholarshipViewModel scholarship)
    {
        var validationResult = scholarship.Validate(routeScholarshipId: id, validateId: true);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var updatedScholarship = await scholarshipService.UpdateScholarshipAsync(new Scholarship
        {
            Id = id,
            Name = scholarship.ScholarshipName,
            Country = scholarship.Country
        });

        return Ok(new ScholarshipViewModel
        {
            ScholarshipId = updatedScholarship.Id,
            ScholarshipName = updatedScholarship.Name,
            Country = updatedScholarship.Country
        });
    }
}