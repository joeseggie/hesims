namespace HESIMS.Web.Data;

/// <summary>
/// Represents student data.
/// </summary>
public class Student
{
    /// <summary>
    /// Student's Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Student's first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Student's last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Student's other names.
    /// </summary>
    public string? OtherNames { get; set; }

    /// <summary>
    /// Student's date of birth.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Student's National Identification Number.
    /// </summary>
    public string? NIN { get; set; }

    public IEnumerable<BankAccount>? BankAccounts { get; set; }

    public IEnumerable<StudentCourse>? StudentCourses { get; set; }

    public IEnumerable<StudentContact>? StudentContacts { get; set; }
}