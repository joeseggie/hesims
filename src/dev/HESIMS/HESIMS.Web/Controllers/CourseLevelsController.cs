namespace HESIMS.Web.Controllers;

/// <summary>
/// Course Levels Controller
/// </summary>
[Route("api/courselevels")]
[ApiController]
public class CourseLevelsController : BaseController
{
    private readonly ICourseLevelService courseLevelService;

    public CourseLevelsController(ICourseLevelService courseLevelService)
    {
        this.courseLevelService = courseLevelService;
    }

    /// <summary>
    /// Get all course levels
    /// </summary>
    /// <returns>List of course levels</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var courseLevelsQueryResult = await courseLevelService.GetCourseLevelsAsync();
        if (!courseLevelsQueryResult.IsSuccess)
        {
            return BadRequest(courseLevelsQueryResult.ErrorMessage);
        }

        var courseLevels = courseLevelsQueryResult.Value;
        if (courseLevels != null && courseLevels.Count() > 0)
        {
            return Ok(courseLevels.Select(courseLevel => new CourseLevelViewModel
            {
                CourseLevelId = courseLevel.Id,
                CourseLevelDescription = courseLevel.Description
            }));
        }
        
        return Ok(courseLevels);
    }

    /// <summary>
    /// Get course level by id
    /// </summary>
    /// <param name="id">Course level id</param>
    /// <returns>Course level</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var courseLevelQueryResult = await courseLevelService.GetCourseLevelByIdAsync(id);
        if (!courseLevelQueryResult.IsSuccess)
        {
            return BadRequest(courseLevelQueryResult.ErrorMessage);
        }

        var courseLevel = courseLevelQueryResult.Value;
        if (courseLevel == null)
        {
            return NotFound();
        }

        return Ok(new CourseLevelViewModel
        {
            CourseLevelId = courseLevel.Id,
            CourseLevelDescription = courseLevel.Description
        });
    }

    /// <summary>
    /// Add course level
    /// </summary>
    /// <param name="courseLevel">New course level view model</param>
    /// <returns>New course level</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CourseLevelViewModel courseLevel)
    {
        var validationResult = courseLevel.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var addCourseLevelResult = await courseLevelService.AddCourseLevelAsync(new CourseLevel
        {
            Id = Guid.NewGuid(),
            Description = courseLevel.CourseLevelDescription
        });
        if (!addCourseLevelResult.IsSuccess)
        {
            return BadRequest(addCourseLevelResult.ErrorMessage);
        }

        var newCourseLevel = addCourseLevelResult.Value;
        if (newCourseLevel != null)
        {
            return Ok(new CourseLevelViewModel
            {
                CourseLevelId = newCourseLevel.Id,
                CourseLevelDescription = newCourseLevel.Description
            });
        }

        return Ok(newCourseLevel);
    }
}