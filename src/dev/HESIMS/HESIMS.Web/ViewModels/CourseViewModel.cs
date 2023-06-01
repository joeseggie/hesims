namespace HESIMS.Web.ViewModels;

/// <summary>
/// Represents the course view model.
/// </summary>
public record CourseViewModel
{
    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid? CourseId { get; set; }

    /// <summary>
    /// Course name.
    /// </summary>
    public string? CourseName { get; set; }

    /// <summary>
    /// Course duration in years.
    /// </summary>
    public int? Duration { get; set; }

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

    /// <summary>
    /// Validates the course view model.
    /// </summary>
    /// <param name="courseId">Course Id.</param>
    public Result<bool> Validate(Guid? routeCourseId = null, bool validateId = false)
    {
        var result = Result<bool>.Success(true);

        if (string.IsNullOrWhiteSpace(CourseName))
        {
            result = Result<bool>.Failure("Course name is required.");
        }

        var isEmptyOrDefaultGuidCourseId = !CourseId.HasValue || CourseId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidCourseId)
        {
            result = Result<bool>.Failure("Course ID is required.");
        }

        if (validateId && CourseId != routeCourseId)
        {
            result = Result<bool>.Failure("Course ID mismatch.");
        }
        
        return result;
    }
}