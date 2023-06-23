namespace HESIMS.Web.Controllers;

[Route("api/studentcourses")]
[ApiController]
public class StudentCoursesController : BaseController
{
    private readonly IStudentCourseService studentCourseService;

    public StudentCoursesController(IStudentCourseService studentCourseService)
    {
        this.studentCourseService = studentCourseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentCourse(StudentCourseViewModel studentCourse)
    {
        var validationResult = studentCourse.Validate();
        if (!validationResult.IsSuccess)
        {
            return BadRequest(validationResult.ErrorMessage);
        }

        var result = await studentCourseService.AddStudentCourseAsync(new StudentCourse
        {
            Id = Guid.NewGuid(),
            StudentId = studentCourse.StudentId,
            CourseId = studentCourse.CourseId,
            ScholarshipId = studentCourse.ScholarshipId,
            EntryYear = studentCourse.EntryYear,
            CompletionYear = studentCourse.CompletionYear,
            CourseRegistrationNumber = studentCourse.CourseRegistrationNumber
        });
        
        if (!result.IsSuccess && result.Value is not null)
        {
            return BadRequest(result.ErrorMessage);
        }

        var newStudentCourse = result.Value;
        return CreatedAtAction(nameof(GetStudentCourseByIdAsync), new { id = studentCourse.Id }, newStudentCourse);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentCourseByIdAsync(Guid id)
    {
        var result = await studentCourseService.GetStudentCourseByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var newStudentCourse = result.Value;
        return Ok(new StudentCourseViewModel
        {
            Id = newStudentCourse?.Id,
            CourseId = newStudentCourse?.CourseId,
            ScholarshipId = newStudentCourse?.ScholarshipId,
            StudentId = newStudentCourse?.StudentId,
            EntryYear = newStudentCourse?.EntryYear,
            CompletionYear = newStudentCourse?.CompletionYear,
            CourseRegistrationNumber = newStudentCourse?.CourseRegistrationNumber
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetStudentCourses([FromQuery] Guid? studentId = null)
    {
        var result = await studentCourseService.GetStudentCoursesAsync(studentId);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        var studentCourses = result.Value;
        if (studentCourses is null || !studentCourses.Any())
        {
            return Ok(studentCourses);
        }

        return Ok(studentCourses.Select(studentCourse => new StudentCourseViewModel
        {
            Id = studentCourse.Id,
            CourseId = studentCourse.CourseId,
            ScholarshipId = studentCourse.ScholarshipId,
            StudentId = studentCourse.StudentId,
            EntryYear = studentCourse.EntryYear,
            CompletionYear = studentCourse.CompletionYear,
            CourseRegistrationNumber = studentCourse.CourseRegistrationNumber,
            Course = new CourseViewModel{
                CourseId = studentCourse.Course?.Id,
                CourseLevelId = studentCourse.Course?.CourseLevelId,
                CourseName = studentCourse.Course?.Name,
                InstitutionId = studentCourse.Course?.InstitutionId,
                Duration = studentCourse.Course?.Duration,
                Institution = new InstitutionViewModel{
                    InstitutionId = studentCourse.Course?.Institution?.Id,
                    InstitutionName = studentCourse.Course?.Institution?.Name,
                }
            },
            Scholarship = new ScholarshipViewModel{
                ScholarshipId = studentCourse.Scholarship?.Id,
                ScholarshipName = studentCourse.Scholarship?.Name,
                Country = new CountryViewModel{
                    CountryId = studentCourse.Scholarship?.Country?.Id,
                    CountryCode = studentCourse.Scholarship?.Country?.Code,
                    CountryName = studentCourse.Scholarship?.Country?.Name
                }
            }
        }));
    }
}