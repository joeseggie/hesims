namespace HESIMS.Web.Data;

/// <summary>
/// Represents applicant data.
/// </summary>
public class Applicant
{
    /// <summary>
    /// Applicant Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Applicant's first name.
    /// </summary>
    public string? Firstname { get; set; }

    /// <summary>
    /// Applicant's last name.
    /// </summary>
    public string? Lastname { get; set; }

    /// <summary>
    /// Applicant's other names.
    /// </summary>
    public string? OtherNames { get; set; }

    /// <summary>
    /// Courses applied for by the applicant.
    /// </summary>
    public IEnumerable<CourseApplication>? CourseApplications { get; set; }

    /// <summary>
    /// Applicant's student profile.
    /// </summary>
    public Student? Student { get; set; }
}
