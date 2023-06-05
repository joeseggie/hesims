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

    /// <summary>
    /// Course duration in years.
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Course level.
    /// </summary>
    public Guid? CourseLevelId { get; set; }

    /// <summary>
    /// Id of insitution offering the course.
    /// </summary>
    public Guid? InstitutionId { get; set; }

    /// <summary>
    /// Course level.
    /// </summary>
    public CourseLevel? CourseLevel { get; set; }

    /// <summary>
    /// Institution offering the course.
    /// </summary>
    public Institution? Institution { get; set; }
}
