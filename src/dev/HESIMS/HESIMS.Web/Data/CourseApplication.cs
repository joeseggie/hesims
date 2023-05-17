namespace HESIMS.Web.Data;

/// <summary>
/// Represents data on application of a course in a scholarship application cycle.
/// </summary>
public class CourseApplication
{
    /// <summary>
    /// Course application Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id of the course applied for in the scholarship application cycle.
    /// </summary>
    public Guid ApplicationCycleCourseId { get; set; }

    /// <summary>
    /// Applicant Id.
    /// </summary>
    public Guid ApplicantId { get; set; }

    /// <summary>
    /// Date and time of applying for a course scholarship.
    /// </summary>
    public DateTimeOffset ApplicationDateTime { get; set; } = DateTimeOffset.Now;

    public ApplicationCycleCourse? ApplicationCycleCourse { get; set; }

    public Applicant? Applicant { get; set; }
}
