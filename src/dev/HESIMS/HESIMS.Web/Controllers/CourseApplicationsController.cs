namespace HESIMS.Web.Controllers;

/// <summary>
/// Course applications controller.
/// </summary>
[Route("api/course-applications")]
[ApiController]
public class CourseApplicationsController : BaseController
{
    private readonly ICourseApplicationService courseApplicationService;

    public CourseApplicationsController(ICourseApplicationService courseApplicationService)
    {
        this.courseApplicationService = courseApplicationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCourseApplicationsAsync()
    {
        var courseApplications = await courseApplicationService.GetCourseApplicationsAsync();
        if (courseApplications != null && courseApplications.Count() > 0)
        {
            return Ok(courseApplications.Select(courseApplication => new CourseApplicationViewModel
            {
                CourseApplicationId = courseApplication.Id,
                ApplicantId = courseApplication.ApplicantId,
                ApplicationCycleCourseId = courseApplication.ApplicationCycleCourseId,
                ApplicationDateTime = courseApplication.ApplicationDateTime
            }));
        }

        return Ok(courseApplications);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseApplicationByIdAsync(Guid id)
    {
        var courseApplication = await courseApplicationService.GetCourseApplicationByIdAsync(id);
        if (courseApplication == null)
        {
            return NotFound();
        }

        return Ok(new CourseApplicationViewModel
        {
            CourseApplicationId = courseApplication.Id,
            ApplicantId = courseApplication.ApplicantId,
            ApplicationCycleCourseId = courseApplication.ApplicationCycleCourseId,
            ApplicationDateTime = courseApplication.ApplicationDateTime
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCourseApplicationAsync([FromBody] CourseApplicationViewModel courseApplication)
    {
        var validationResult = courseApplication.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var applicantId = GetValueFromNullableGuid(courseApplication.ApplicantId);
        var applicationCycleCourseId = GetValueFromNullableGuid(courseApplication.ApplicationCycleCourseId);
        var newCourseApplication = new CourseApplication
        {
            Id = Guid.NewGuid(),
            ApplicantId = applicantId,
            ApplicationCycleCourseId = applicationCycleCourseId,
            ApplicationDateTime = DateTimeOffset.Now
        };

        await courseApplicationService.AddCourseApplicationAsync(newCourseApplication);

        return CreatedAtAction(nameof(GetCourseApplicationByIdAsync), new { id = newCourseApplication.Id }, new CourseApplicationViewModel
        {
            CourseApplicationId = newCourseApplication.Id,
            ApplicantId = newCourseApplication.ApplicantId,
            ApplicationCycleCourseId = newCourseApplication.ApplicationCycleCourseId,
            ApplicationDateTime = newCourseApplication.ApplicationDateTime
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourseApplicationAsync(Guid id, [FromBody] CourseApplicationViewModel courseApplication)
    {
        var validationResult = courseApplication.Validate(routeId: id, validateId: true);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var applicantId = GetValueFromNullableGuid(courseApplication.ApplicantId);
        var applicationCycleCourseId = GetValueFromNullableGuid(courseApplication.ApplicationCycleCourseId);
        var applicationDateTime = GetValueFromNullableDateTimeOffset(courseApplication.ApplicationDateTime);
        var updatedCourseApplication = await courseApplicationService.UpdateCourseApplicationAsync(new CourseApplication
        {
            Id = id,
            ApplicantId = applicantId,
            ApplicationCycleCourseId = applicationCycleCourseId,
            ApplicationDateTime = applicationDateTime
        });

        return Ok(new CourseApplicationViewModel
        {
            CourseApplicationId = updatedCourseApplication.Id,
            ApplicantId = updatedCourseApplication.ApplicantId,
            ApplicationCycleCourseId = updatedCourseApplication.ApplicationCycleCourseId,
            ApplicationDateTime = updatedCourseApplication.ApplicationDateTime
        });
    }
}