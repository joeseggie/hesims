namespace HESIMS.Web.Controllers;

/// <summary>
/// Application cycles controller.
/// </summary>
[Route("api/application-cycles")]
[ApiController]
public class ApplicationCyclesController : BaseController
{
    private readonly IApplicationCycleService applicationCycleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationCyclesController"/> class.
    /// </summary>
    /// <param name="applicationCycleService">Application cycle service.</param>
    public ApplicationCyclesController(IApplicationCycleService applicationCycleService)
    {
        this.applicationCycleService = applicationCycleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationCyclesAsync([FromQuery] Guid? scholarship = null, [FromQuery] string? year = null)
    {
        var applicationCycles = await applicationCycleService.GetApplicationCyclesAsync(scholarship, year);
        if (applicationCycles != null && applicationCycles.Count() > 0)
        {
            return Ok(applicationCycles.Select(applicationCycle => new ApplicationCycleViewModel
            {
                ApplicationCycleId = applicationCycle.Id,
                ScholarshipId = applicationCycle.ScholarshipId,
                AcademicYear = applicationCycle.AcademicYear,
                Status = applicationCycle.Status
            }));
        }

        return Ok(applicationCycles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationCycleByIdAsync(Guid id)
    {
        var applicationCycle = await applicationCycleService.GetApplicationCycleByIdAsync(id);
        if (applicationCycle == null)
        {
            return NotFound();
        }

        return Ok(new ApplicationCycleViewModel
        {
            ApplicationCycleId = applicationCycle.Id,
            ScholarshipId = applicationCycle.ScholarshipId,
            AcademicYear = applicationCycle.AcademicYear,
            Status = applicationCycle.Status
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddApplicationCycleAsync([FromBody] ApplicationCycleViewModel applicationCycle)
    {
        var validationResult = applicationCycle.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var scholarshipId = GetValueFromNullableGuid(applicationCycle.ScholarshipId);
        var newApplicationCycle = new ApplicationCycle
        {
            Id = Guid.NewGuid(),
            ScholarshipId = scholarshipId,
            AcademicYear = applicationCycle.AcademicYear,
            Status = applicationCycle.Status
        };

        await applicationCycleService.AddApplicationCycleAsync(newApplicationCycle);

        return Ok(new ApplicationCycleViewModel
        {
            ApplicationCycleId = newApplicationCycle.Id,
            ScholarshipId = newApplicationCycle.ScholarshipId,
            AcademicYear = newApplicationCycle.AcademicYear,
            Status = newApplicationCycle.Status
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplicationCycleAsync(Guid id, [FromBody] ApplicationCycleViewModel applicationCycle)
    {
        var validationResult = applicationCycle.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var scholarshipId = GetValueFromNullableGuid(applicationCycle.ScholarshipId);
        var updatedApplicationCycle = new ApplicationCycle
        {
            Id = id,
            ScholarshipId = scholarshipId,
            AcademicYear = applicationCycle.AcademicYear,
            Status = applicationCycle.Status
        };

        await applicationCycleService.UpdateApplicationCycleAsync(updatedApplicationCycle);

        return Ok(new ApplicationCycleViewModel
        {
            ApplicationCycleId = updatedApplicationCycle.Id,
            ScholarshipId = updatedApplicationCycle.ScholarshipId,
            AcademicYear = updatedApplicationCycle.AcademicYear,
            Status = updatedApplicationCycle.Status
        });
    }
}