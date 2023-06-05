namespace HESIMS.Web.Data;

/// <summary>
/// Represents course level data.
/// </summary>
public class CourseLevel
{
    /// <summary>
    /// Course level Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Course level description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Courses with this course level.
    /// </summary>
    public IEnumerable<Course>? Courses { get; set; }
}