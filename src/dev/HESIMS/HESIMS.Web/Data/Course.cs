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
    public string? CourseLevel { get; set; }

    /// <summary>
    /// Insitution offering the course.
    /// </summary>
    public string? Institution { get; set; }

    /// <summary>
    /// Country of the institution.
    /// </summary>
    public string? InstitutionCountry { get; set; }
}
