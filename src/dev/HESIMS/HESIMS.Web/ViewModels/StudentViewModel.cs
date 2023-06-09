namespace HESIMS.Web.ViewModels;

/// <summary>
/// Student view model.
/// </summary>
public record StudentViewModel
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

    /// <summary>
    /// Student's bank accounts.
    /// </summary>
    public IEnumerable<BankAccountViewModel>? BankAccounts { get; set; }

    /// <summary>
    /// Student's courses.
    /// </summary>
    public IEnumerable<StudentCourseViewModel>? StudentCourses { get; set; }

    /// <summary>
    /// Student's contacts.
    /// </summary>
    public IEnumerable<StudentContactViewModel>? StudentContacts { get; set; }

    public Result<bool> Validate()
    {
        return Result<bool>.Success(true);
    }
}