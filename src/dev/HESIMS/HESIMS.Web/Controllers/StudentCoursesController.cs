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

    [HttpGet("{studentid:guid}")]
    public async Task<IActionResult> GetStudentCoursesByStudentIdAsync(Guid studentid)
    {
        var result = await studentCourseService.GetStudentCoursesAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var studentCourses = result.Value;
        if (studentCourses != null && studentCourses.Count() > 0)
        {
            var firstStudentCourse = studentCourses.FirstOrDefault(studentCourse => studentCourse.StudentId == studentid);
            return Ok(new StudentCourseViewModel
            {
                StudentId = firstStudentCourse?.StudentId,
                CourseId = firstStudentCourse?.CourseId,
                CompletionYear = firstStudentCourse?.CompletionYear,
            });
        }
        else
        {
            return Ok(new StudentCourseViewModel
            {
                StudentId = studentid,
                CourseId = Guid.Empty,
            });
        }
    }
}