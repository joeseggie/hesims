namespace HESIMS.Web.Data.Services;

/// <summary>
/// Student course service interface.
/// </summary>
public interface IStudentCourseService
{
    /// <summary>
    /// Add new student course.
    /// </summary>
    /// <param name="newStudentCourse">New student course details.</param>
    /// <returns>New student course details.</returns>
    Task<Result<StudentCourse>> AddStudentCourseAsync(StudentCourse newStudentCourse);

    /// <summary>
    /// Update student course details.
    /// </summary>
    /// <param name="studentCourseUpdate">Student course update details.</param>
    /// <returns>Updated student course details.</returns>
    Task<Result<StudentCourse>> UpdateStudentCourseAsync(StudentCourse studentCourseUpdate);

    /// <summary>
    /// Get student course by Id.
    /// </summary>
    /// <param name="id">Student course ID.</param>
    /// <returns>Student course if ID exist, otherwise failure result.</returns>
    Task<Result<StudentCourse>> GetStudentCourseByIdAsync(Guid id);

    /// <summary>
    /// Get all student courses.
    /// </summary>
    /// <returns>List of student courses.</returns>
    Task<Result<IEnumerable<StudentCourse>>> GetStudentCoursesAsync();
}

/// <summary>
/// Student course service.
/// </summary>
public class StudentCourseService : IStudentCourseService
{
    private readonly ApplicationDbContext db;

    ///<summary>
    /// Initializes a new instance of the <see cref="StudentCourseService"/> class.
    /// </summary>
    public StudentCourseService(ApplicationDbContext dbContext)
    {
        db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<StudentCourse>> AddStudentCourseAsync(StudentCourse newStudentCourse)
    {
        db.StudentCourses.Add(newStudentCourse);
        await db.SaveChangesAsync();
        return Result<StudentCourse>.Success(newStudentCourse);
    }

    /// <inheritdoc/>
    public async Task<Result<StudentCourse>> UpdateStudentCourseAsync(StudentCourse studentCourseUpdate)
    {
        var studentCourseToUpdate = await db.StudentCourses.FindAsync(studentCourseUpdate.Id);
        if (studentCourseToUpdate == null)
        {
            return Result<StudentCourse>.Failure($"Student course with ID {studentCourseUpdate.Id} does not exist.");
        }
        
        db.Entry(studentCourseToUpdate).CurrentValues.SetValues(studentCourseUpdate);
        await db.SaveChangesAsync();

        return Result<StudentCourse>.Success(studentCourseToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<StudentCourse>> GetStudentCourseByIdAsync(Guid id)
    {
        var studentCourse = await db.StudentCourses.FindAsync(id);
        if (studentCourse == null)
        {
            return Result<StudentCourse>.Failure($"Student course with ID {id} does not exist.");
        }

        return Result<StudentCourse>.Success(studentCourse);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<StudentCourse>>> GetStudentCoursesAsync()
    {
        var studentCourses = await db.StudentCourses.ToListAsync();
        return Result<IEnumerable<StudentCourse>>.Success(studentCourses);
    }
}