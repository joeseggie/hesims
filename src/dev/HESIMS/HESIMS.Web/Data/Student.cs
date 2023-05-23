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
    /// Student's application profile Id.
    /// </summary>
    public Guid? ApplicantId { get; set; }

    /// <summary>
    /// Student's registration number.
    /// </summary>
    public string? StudentNumber { get; set; }

    /// <summary>
    /// Student's address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Student's application profile.
    /// </summary>
    public Applicant? Applicant { get; set; }
}