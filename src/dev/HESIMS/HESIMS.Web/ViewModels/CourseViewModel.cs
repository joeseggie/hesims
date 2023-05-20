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
    /// Validates the course view model.
    /// </summary>
    /// <param name="courseId">Course Id.</param>
    public ValidationResult Validate(Guid? routeCourseId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        if (string.IsNullOrWhiteSpace(CourseName))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course name is required.";
        }

        var isEmptyOrDefaultGuidCourseId = !CourseId.HasValue && CourseId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidCourseId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course ID is required.";
        }

        if (validateId && CourseId != routeCourseId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course ID mismatch.";
        }
        
        return validationResult;
    }
}