namespace HESIMS.Web.Data;

/// <summary>
/// Represents student registration data.
/// </summary>
public class CourseRegistration
{
    /// <summary>
    /// Student registration Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Student's course being registered for.
    /// </summary>
    public Guid? StudentCourseId { get; set; }

    /// <summary>
    /// Notes on the registration.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Academic year of the registration.
    /// </summary>
    public string? AcademicYear { get; set; }

    /// <summary>
    /// Student course.
    /// </summary>
    public StudentCourse? StudentCourse { get; set; }
}