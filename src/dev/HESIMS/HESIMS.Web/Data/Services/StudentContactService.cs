namespace HESIMS.Web.Data.Services;

public interface IStudentContactService
{
    /// <summary>
    /// Add new student contact.
    /// </summary>
    /// <param name="newStudentContact">New student contact details.</param>
    /// <returns>New student contact details.</returns>
    Task<Result<StudentContact>> AddStudentContactAsync(StudentContact newStudentContact);

    /// <summary>
    /// Update student contact details.
    /// </summary>
    /// <param name="studentContactUpdate">Student contact update details.</param>
    /// <returns>Updated student contact details.</returns>
    Task<Result<StudentContact>> UpdateStudentContactAsync(StudentContact studentContactUpdate);

    /// <summary>
    /// Get student contact by Id.
    /// </summary>
    /// <param name="id">Student contact ID.</param>
    /// <returns>Student contact if ID exist, otherwise failure result.</returns>
    Task<Result<StudentContact>> GetStudentContactByIdAsync(Guid id);

    /// <summary>
    /// Get all student contacts.
    /// </summary>
    /// <returns>List of student contacts.</returns>
    Task<Result<IEnumerable<StudentContact>>> GetStudentContactsAsync();
}

/// <summary>
/// Student contact service.
/// </summary>
public class StudentContactService : IStudentContactService
{
    private readonly ApplicationDbContext db;

    /// <summary>
    /// Instantiates a new instance of <see cref="StudentContactService"/>.
    /// </summary>
    public StudentContactService(ApplicationDbContext dbContext)
    {
        this.db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<StudentContact>> AddStudentContactAsync(StudentContact newStudentContact)
    {
        var entityEntry = await db.StudentContacts.AddAsync(newStudentContact);
        await db.SaveChangesAsync();

        return Result<StudentContact>.Success(entityEntry.Entity);
    }

    /// <inheritdoc/>
    public async Task<Result<StudentContact>> UpdateStudentContactAsync(StudentContact studentContactUpdate)
    {
        var studentContactToUpdate = await db.StudentContacts.FindAsync(studentContactUpdate.Id);

        if (studentContactToUpdate == null)
        {
            return Result<StudentContact>.Failure($"Student contact with ID {studentContactUpdate.Id} not found.");
        }

        db.Entry(studentContactToUpdate).CurrentValues.SetValues(studentContactUpdate);
        await db.SaveChangesAsync();

        return Result<StudentContact>.Success(studentContactToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<StudentContact>> GetStudentContactByIdAsync(Guid id)
    {
        var studentContact = await db.StudentContacts.FindAsync(id);
        if (studentContact == null)
        {
            return Result<StudentContact>.Failure($"Student contact with ID {id} not found.");
        }

        return Result<StudentContact>.Success(studentContact);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<StudentContact>>> GetStudentContactsAsync()
    {
        var studentContacts = await db.StudentContacts.ToListAsync();
        return Result<IEnumerable<StudentContact>>.Success(studentContacts);
    }
}