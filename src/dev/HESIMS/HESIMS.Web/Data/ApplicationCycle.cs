namespace HESIMS.Web.Data;

/// <summary>
/// Represents the application cycle data.
/// </summary>
public class ApplicationCycle
{
    /// <summary>
    /// Application cycle Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Application cycle scholarship Id.
    /// </summary>
    public Guid ScholarshipId { get; set; }

    public string? Status { get; set; }

    /// <summary>
    /// Application cycle academic year.
    /// </summary>
    public string? AcademicYear { get; set; }

    public IEnumerable<ApplicationCycleCourse>? ApplicationCycleCourses { get; set; }

    public Scholarship? Scholarship { get; set; }
}