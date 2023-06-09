namespace HESIMS.Web.ViewModels;

public record CourseRegistrationViewModel
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
    public StudentCourseViewModel? StudentCourse { get; set; }

    public Result<bool> Validate()
    {
        return Result<bool>.Success(true);
    }
}