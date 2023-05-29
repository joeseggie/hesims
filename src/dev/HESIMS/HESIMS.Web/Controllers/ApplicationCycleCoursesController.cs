namespace HESIMS.Web.Controllers;

/// <summary>
/// Application cycle courses controller.
/// </summary>
[Route("api/application-cycle-courses")]
[ApiController]
public class ApplicationCycleCoursesController : BaseController
{
    private readonly IApplicationCycleCourseService applicationCycleCourseService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationCycleCoursesController"/> class.
    /// </summary>
    /// <param name="applicationCycleCourseService">Application cycle course service.</param>
    public ApplicationCycleCoursesController(IApplicationCycleCourseService applicationCycleCourseService)
    {
        this.applicationCycleCourseService = applicationCycleCourseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationCycleCoursesAsync([FromQuery] Guid? cycle = null)
    {
        var applicationCycleCourses = await applicationCycleCourseService.GetApplicationCycleCoursesAsync(applicationCycleId: cycle);
        if (applicationCycleCourses != null && applicationCycleCourses.Count() > 0)
        {
            return Ok(applicationCycleCourses.Select(applicationCycleCourse => new ApplicationCycleCourseViewModel
            {
                ApplicationCycleCourseId = applicationCycleCourse.Id,
                ApplicationCycleId = applicationCycleCourse.ApplicationCycleId,
                CourseId = applicationCycleCourse.CourseId,
                Course = new CourseViewModel
                {
                    CourseId = applicationCycleCourse?.Course?.Id,
                    CourseName = applicationCycleCourse?.Course?.Name,
                    Duration = applicationCycleCourse?.Course?.Duration,
                    CourseLevel = applicationCycleCourse?.Course?.CourseLevel,
                    Institution = applicationCycleCourse?.Course?.Institution,
                    InstitutionCountry = applicationCycleCourse?.Course?.InstitutionCountry
                },
                ApplicationCycle = new ApplicationCycleViewModel
                {
                    ApplicationCycleId = applicationCycleCourse?.ApplicationCycle?.Id,
                    ScholarshipId = applicationCycleCourse?.ApplicationCycle?.ScholarshipId,
                    AcademicYear = applicationCycleCourse?.ApplicationCycle?.AcademicYear,
                    Status = applicationCycleCourse?.ApplicationCycle?.Status,
                    Scholarship = new ScholarshipViewModel
                    {
                        ScholarshipId = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Id,
                        ScholarshipName = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Name,
                        Country = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Country
                    }
                }
            }));
        }

        return Ok(applicationCycleCourses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationCycleCourseByIdAsync(Guid id)
    {
        return await QueryForApplicationCycleCourseAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> AddApplicationCycleCourseAsync([FromBody] ApplicationCycleCourseViewModel applicationCycleCourse)
    {
        var validationResult = applicationCycleCourse.Validate();
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var applicationCycleId = GetValueFromNullableGuid(applicationCycleCourse.ApplicationCycleId);
        var courseId = GetValueFromNullableGuid(applicationCycleCourse.CourseId);
        var newApplicationCycleCourse = new ApplicationCycleCourse
        {
            Id = Guid.NewGuid(),
            ApplicationCycleId = applicationCycleId,
            CourseId = courseId
        };

        await applicationCycleCourseService.AddApplicationCycleCourseAsync(newApplicationCycleCourse);

        return CreatedAtAction(nameof(GetApplicationCycleCourseByIdAsync), new { id = newApplicationCycleCourse.Id }, new ApplicationCycleCourseViewModel
        {
            ApplicationCycleCourseId = newApplicationCycleCourse.Id,
            ApplicationCycleId = newApplicationCycleCourse.ApplicationCycleId,
            CourseId = newApplicationCycleCourse.CourseId
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplicationCycleCourseAsync(Guid id, [FromBody] ApplicationCycleCourseViewModel applicationCycleCourse)
    {
        var validationResult = applicationCycleCourse.Validate(routeId: id, validateId: true);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var applicationCycleId = GetValueFromNullableGuid(applicationCycleCourse.ApplicationCycleId);
        var courseId = GetValueFromNullableGuid(applicationCycleCourse.CourseId);
        var updatedApplicationCycleCourse = await applicationCycleCourseService.UpdateApplicationCycleCourseAsync(new ApplicationCycleCourse
        {
            Id = id,
            ApplicationCycleId = applicationCycleId,
            CourseId = courseId
        });

        return Ok(await QueryForApplicationCycleCourseAsync(updatedApplicationCycleCourse.Id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplicationCycleCourseAsync(Guid id)
    {
        var applicationCycleCourse = await applicationCycleCourseService.GetApplicationCycleCourseByIdAsync(id);
        if (applicationCycleCourse == null)
        {
            return NotFound();
        }

        await applicationCycleCourseService.DeleteApplicationCycleCourseAsync(applicationCycleCourse.Id);

        return Ok();
    }

    private async Task<IActionResult> QueryForApplicationCycleCourseAsync(Guid applicationCycleCourseId)
    {
        var applicationCycleCourse = await applicationCycleCourseService.GetApplicationCycleCourseByIdAsync(applicationCycleCourseId);
        if (applicationCycleCourse == null)
        {
            return NotFound();
        }

        return Ok(new ApplicationCycleCourseViewModel
        {
            ApplicationCycleCourseId = applicationCycleCourse.Id,
            ApplicationCycleId = applicationCycleCourse.ApplicationCycleId,
            CourseId = applicationCycleCourse.CourseId,
            Course = new CourseViewModel
            {
                CourseId = applicationCycleCourse?.Course?.Id,
                CourseName = applicationCycleCourse?.Course?.Name,
                Duration = applicationCycleCourse?.Course?.Duration,
                CourseLevel = applicationCycleCourse?.Course?.CourseLevel,
                Institution = applicationCycleCourse?.Course?.Institution,
                InstitutionCountry = applicationCycleCourse?.Course?.InstitutionCountry
            },
            ApplicationCycle = new ApplicationCycleViewModel
            {
                ApplicationCycleId = applicationCycleCourse?.ApplicationCycle?.Id,
                ScholarshipId = applicationCycleCourse?.ApplicationCycle?.ScholarshipId,
                AcademicYear = applicationCycleCourse?.ApplicationCycle?.AcademicYear,
                Status = applicationCycleCourse?.ApplicationCycle?.Status,
                Scholarship = new ScholarshipViewModel
                {
                    ScholarshipId = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Id,
                    ScholarshipName = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Name,
                    Country = applicationCycleCourse?.ApplicationCycle?.Scholarship?.Country
                }
            }
        });
    }
}