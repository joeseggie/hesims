namespace HESIMS.Web.Data.Services;

/// <summary>
/// Student service interface.
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Add a new student.
    /// </summary>
    Task<Result<Student>> AddStudentAsync(Student newStudent);

    /// <summary>
    /// Update an existing student.
    /// </summary>
    Task<Result<Student>> UpdateStudentAsync(Student studentUpdate);

    /// <summary>
    /// Get student by id.
    /// </summary>
    Task<Result<Student>> GetStudentByIdAsync(Guid id);

    /// <summary>
    /// Get all students.
    /// </summary>
    Task<Result<IEnumerable<Student>>> GetStudentsAsync();
}

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext db;

    public StudentService(ApplicationDbContext context)
    {
        db = context;
    }

    /// <inheritdoc/>
    public async Task<Result<Student>> AddStudentAsync(Student newStudent)
    {
        var hasDuplicateNIN = db.Students.Any(s => s.NIN == newStudent.NIN);
        if (hasDuplicateNIN)
        {
            return Result<Student>.Failure("Student with the same NIN already exists.");
        }

        db.Students.Add(newStudent);
        await db.SaveChangesAsync();
        return Result<Student>.Success(newStudent);
    }

    /// <inheritdoc/>
    public async Task<Result<Student>> UpdateStudentAsync(Student studentUpdate)
    {
        var existingStudent = await db.Students.FindAsync(studentUpdate.Id);
        if (existingStudent == null)
        {
            return Result<Student>.Failure($"Student with ID {studentUpdate.Id} was not found.");
        }

        var hasDuplicateNIN = db.Students.Any(student => student.NIN == studentUpdate.NIN && student.Id != studentUpdate.Id);
        if (hasDuplicateNIN)
        {
            return Result<Student>.Failure("Student with the same NIN already exists.");
        }

        db.Entry(existingStudent).CurrentValues.SetValues(studentUpdate);
        await db.SaveChangesAsync();

        return Result<Student>.Success(existingStudent);
    }

    /// <inheritdoc/>
    public async Task<Result<Student>> GetStudentByIdAsync(Guid id)
    {
        var student = await db.Students.FindAsync(id);
        if (student == null)
        {
            return Result<Student>.Failure($"Student with ID {id} was not found.");
        }

        return Result<Student>.Success(student);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<Student>>> GetStudentsAsync()
    {
        var students = await db.Students.ToListAsync();
        return Result<IEnumerable<Student>>.Success(students);
    }
}