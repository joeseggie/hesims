namespace HESIMS.Web.Controllers;

/// <summary>
/// Courses controller.
/// </summary>
[Route("api/courses")]
[ApiController]
public class CoursesController : BaseController
{
    private readonly ICourseService courseService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CoursesController"/> class.
    /// </summary>
    public CoursesController(ICourseService courseService)
    {
        this.courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCoursesAsync([FromQuery] Guid? courseLevelId = null, [FromQuery] Guid? institutionId = null)
    {
        var coursesQueryResult = await courseService.GetCoursesAsync(courseLevelId, institutionId);
        if (!coursesQueryResult.IsSuccess)
        {
            return BadRequest(coursesQueryResult.ErrorMessage);
        }

        var courses = coursesQueryResult.Value;
        if (courses != null && courses.Count() > 0)
        {
            return Ok(courses.Select(course => new CourseViewModel
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Duration = course.Duration,
                InstitutionId = course.Institution?.Id,
                CourseLevelId = course.CourseLevel?.Id,
                Institution = new InstitutionViewModel
                {
                    InstitutionId = course.Institution?.Id,
                    InstitutionName = course.Institution?.Name,
                    CountryId = course.Institution?.Country?.Id,
                    Country = new CountryViewModel
                    {
                        CountryId = course.Institution?.Country?.Id,
                        CountryName = course.Institution?.Country?.Name,
                        CountryCode = course.Institution?.Country?.Code
                    }
                },
                CourseLevel = new CourseLevelViewModel
                {
                    CourseLevelId = course.CourseLevel?.Id,
                    CourseLevelDescription = course.CourseLevel?.Description
                }
            }));
        }

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseByIdAsync(Guid id)
    {
        var courseQueryResult = await courseService.GetCourseByIdAsync(id);
        if (!courseQueryResult.IsSuccess)
        {
            return BadRequest(courseQueryResult.ErrorMessage);
        }

        var course = courseQueryResult.Value;
        if (course == null)
        {
            return NotFound("Course not found.");
        }

        return Ok(new CourseViewModel
        {
            CourseId = course.Id,
            CourseName = course.Name,
            Duration = course.Duration,
            InstitutionId = course.Institution?.Id,
            CourseLevelId = course.CourseLevel?.Id,
            Institution = new InstitutionViewModel
            {
                InstitutionId = course.Institution?.Id,
                InstitutionName = course.Institution?.Name,
                CountryId = course.Institution?.Country?.Id,
                Country = new CountryViewModel
                {
                    CountryId = course.Institution?.Country?.Id,
                    CountryName = course.Institution?.Country?.Name,
                    CountryCode = course.Institution?.Country?.Code
                }
            },
            CourseLevel = new CourseLevelViewModel
            {
                CourseLevelId = course.CourseLevel?.Id,
                CourseLevelDescription = course.CourseLevel?.Description
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCourseAsync([FromBody] CourseViewModel course)
    {
        var validationResult = course.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var newCourse = new Course
        {
            Id = Guid.NewGuid(),
            Name = course.CourseName,
            InstitutionId = course.InstitutionId,
            CourseLevelId = course.CourseLevelId,
            Duration = course.Duration ?? 0
        };

        try
        {
            await courseService.AddCourseAsync(newCourse);
        }
        catch (DbUpdateException error)
        {
            var sqlException = error.InnerException as SqlException;
            if (sqlException != null && (sqlException.Number == 2601 || sqlException.Number == 2627))
            {
                return BadRequest("Course already exists.");
            }
        }

        return CreatedAtAction(
             nameof(GetCourseByIdAsync),
             new { id = newCourse.Id },
             new CourseViewModel
             {
                 CourseId = newCourse.Id,
                 CourseName = newCourse.Name,
                 InstitutionId = newCourse.InstitutionId,
                 CourseLevelId = newCourse.CourseLevelId,
                 Duration = newCourse.Duration
             });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourseAsync(Guid id, [FromBody] CourseViewModel course)
    {
        var validationResult = course.Validate(routeCourseId: id, validateId: true);
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var updatedCourseResult = await courseService.UpdateCourseAsync(new Course
        {
            Id = id,
            Name = course.CourseName,
            InstitutionId = course.InstitutionId,
            CourseLevelId = course.CourseLevelId,
            Duration = course.Duration ?? 0
        });
        if (!updatedCourseResult.IsSuccess)
        {
            return BadRequest(updatedCourseResult.ErrorMessage);
        }

        var updatedCourse = updatedCourseResult.Value;
        return Ok(new CourseViewModel
        {
            CourseId = updatedCourse?.Id,
            CourseName = updatedCourse?.Name,
            InstitutionId = updatedCourse?.InstitutionId,
            CourseLevelId = updatedCourse?.CourseLevelId,
            Duration = updatedCourse?.Duration
        });
    }
}