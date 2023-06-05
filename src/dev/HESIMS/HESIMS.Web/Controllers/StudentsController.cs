namespace HESIMS.Web.Controllers;

/// <summary>
/// Students controller.
/// </summary>
[Route("api/students")]
[ApiController]
public class StudentsController : BaseController
{
    private readonly IStudentService studentService;

    public StudentsController(IStudentService studentService)
    {
        this.studentService = studentService;
    }
    
    /// <summary>
    /// Get student by id.
    /// </summary>
    /// <param name="id">Student id.</param>
    /// <returns>Student details.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentsByIdAsync(Guid id)
    {
        var result = await studentService.GetStudentByIdAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var student = result.Value;
        return Ok(new StudentViewModel
        {
            Id = id,
            FirstName = student?.FirstName,
            LastName = student?.LastName,
            OtherNames = student?.OtherNames,
            Address = student?.Address,
            NIN = student?.NIN
        });
    }

    /// <summary>
    /// Get students.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetStudentsAsync()
    {
        var result = await studentService.GetStudentsAsync();
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var students = result.Value;
        if (students != null && students.Count() > 0)
        {
            return Ok(students.Select(student => new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                OtherNames = student.OtherNames,
                Address = student.Address,
                NIN = student.NIN
            }));
        }

        return Ok(students);
    }

    /// <summary>
    /// Add student.
    /// </summary>
    /// <param name="student">Student details.</param>
    /// <returns>Student details.</returns>
    [HttpPost]
    public async Task<IActionResult> AddStudentAsync([FromBody] StudentViewModel student)
    {
        var result = await studentService.AddStudentAsync(new Student
        {
            Id = Guid.NewGuid(),
            FirstName = student.FirstName,
            LastName = student.LastName,
            OtherNames = student.OtherNames,
            Address = student.Address,
            NIN = student.NIN
        });
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var newStudent = result.Value;
        return CreatedAtAction(nameof(GetStudentsByIdAsync), new { id = student.Id }, newStudent);
    }

    /// <summary>
    /// Update student details.
    /// </summary>
    /// <param name="student">Student update details.</param>
    /// <returns>Updated student details.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudentAsync(Guid id, [FromBody] StudentViewModel student)
    {
        var result = await studentService.UpdateStudentAsync(new Student
        {
            Id = id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            OtherNames = student.OtherNames,
            Address = student.Address,
            NIN = student.NIN
        });
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var updatedStudent = result.Value;
        return Ok(new StudentViewModel
        {
            Id = id,
            FirstName = updatedStudent?.FirstName,
            LastName = updatedStudent?.LastName,
            OtherNames = updatedStudent?.OtherNames,
            Address = updatedStudent?.Address,
            NIN = updatedStudent?.NIN
        });
    }
}