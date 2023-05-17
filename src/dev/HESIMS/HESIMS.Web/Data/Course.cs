namespace HESIMS.Web.Data;

/// <summary>
/// Represents course data.
/// </summary>
public class Course
{
    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Course name.
    /// </summary>
    public string? Name { get; set; }

    public IEnumerable<ApplicationCycleCourse>? ApplicationCycleCourses { get; set; }
}
