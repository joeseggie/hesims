namespace HESIMS.Web.Controllers;

[Route("api/applicants")]
[ApiController]
public class ApplicantsController : BaseController
{
    private readonly IApplicantService applicantService;

    /// <summary>
    /// Initializes a new instance of <see cref="ApplicantsController"/> class.
    /// </summary>
    /// <param name="applicantService">Applicant service.</param> 
    public ApplicantsController(IApplicantService applicantService)
    {
        this.applicantService = applicantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicantsAsync()
    {
        var applicants = await applicantService.GetApplicantsAsync();
        if (applicants != null && applicants.Count() > 0)
        {
            return Ok(applicants.Select(applicant => new ApplicantViewModel
            {
                ApplicantId = applicant.Id,
                Firstname = applicant.Firstname,
                Lastname = applicant.Lastname,
                OtherNames = applicant.OtherNames
            }));
        }

        return Ok(applicants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicantByIdAsync(Guid id)
    {
        var applicant = await applicantService.GetApplicantByIdAsync(id);
        if (applicant == null)
        {
            return NotFound();
        }

        return Ok(new ApplicantViewModel
        {
            ApplicantId = applicant.Id,
            Firstname = applicant.Firstname,
            Lastname = applicant.Lastname,
            OtherNames = applicant.OtherNames
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddApplicantAsync([FromBody] ApplicantViewModel applicant)
    {
        var validationResult = applicant.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var newApplicant = new Applicant
        {
            Id = Guid.NewGuid(),
            Firstname = applicant.Firstname,
            Lastname = applicant.Lastname,
            OtherNames = applicant.OtherNames
        };

        await applicantService.AddApplicantAsync(newApplicant);

        return CreatedAtAction(nameof(GetApplicantByIdAsync), new { id = newApplicant.Id }, new ApplicantViewModel
        {
            ApplicantId = newApplicant.Id,
            Firstname = newApplicant.Firstname,
            Lastname = newApplicant.Lastname,
            OtherNames = newApplicant.OtherNames
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplicantAsync(Guid id, [FromBody] ApplicantViewModel applicant)
    {
        var validationResult = applicant.Validate(routeId: id, validateId: true);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var updatedApplicant = await applicantService.UpdateApplicantAsync(new Applicant
        {
            Id = id,
            Firstname = applicant.Firstname,
            Lastname = applicant.Lastname,
            OtherNames = applicant.OtherNames
        });

        return Ok(new ApplicantViewModel
        {
            ApplicantId = updatedApplicant.Id,
            Firstname = updatedApplicant.Firstname,
            Lastname = updatedApplicant.Lastname,
            OtherNames = updatedApplicant.OtherNames
        });
    }
}