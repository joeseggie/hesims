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
    public async Task<IActionResult> GetCoursesAsync([FromQuery] string? country = null)
    {
        var courses = await courseService.GetCoursesAsync(country);
        if (courses != null && courses.Count() > 0)
        {
            return Ok(courses.Select(course => new CourseViewModel
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Institution = course.Institution,
                InstitutionCountry = course.InstitutionCountry,
                CourseLevel = course.CourseLevel,
                Duration = course.Duration
            }));
        }

        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseByIdAsync(Guid id)
    {
        var course = await courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        return Ok(new CourseViewModel
        {
            CourseId = course.Id,
            CourseName = course.Name,
            Institution = course.Institution,
            InstitutionCountry = course.InstitutionCountry,
            CourseLevel = course.CourseLevel,
            Duration = course.Duration
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
            Institution = course.Institution,
            InstitutionCountry = course.InstitutionCountry,
            CourseLevel = course.CourseLevel,
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
                 Institution = newCourse.Institution,
                 InstitutionCountry = newCourse.InstitutionCountry,
                 CourseLevel = newCourse.CourseLevel,
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

        var updatedCourse = await courseService.UpdateCourseAsync(new Course
        {
            Id = id,
            Name = course.CourseName,
            Institution = course.Institution,
            InstitutionCountry = course.InstitutionCountry,
            CourseLevel = course.CourseLevel,
            Duration = course.Duration ?? 0
        });

        return Ok(new CourseViewModel
        {
            CourseId = updatedCourse.Id,
            CourseName = updatedCourse.Name,
            Institution = updatedCourse.Institution,
            InstitutionCountry = updatedCourse.InstitutionCountry,
            CourseLevel = updatedCourse.CourseLevel,
            Duration = updatedCourse.Duration
        });
    }
}