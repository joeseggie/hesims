namespace HESIMS.Web.Data;

/// <summary>
/// Represents scholarship application cycle course data.
/// </summary>
public class ApplicationCycleCourse
{
    /// <summary>
    /// Scholarship application cycle course Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Application cycle Id.
    /// </summary>
    public Guid ApplicationCycleId { get; set; }

    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Application cycle.
    /// </summary>
    public ApplicationCycle? ApplicationCycle { get; set; }

    /// <summary>
    /// Course.
    /// </summary>
    public Course? Course { get; set; }

    public IEnumerable<CourseApplication>? CourseApplications { get; set; }
}
