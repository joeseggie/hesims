namespace HESIMS.Web.ViewModels;

/// <summary>
/// Application cycle course view model.
/// </summary>
public record ApplicationCycleCourseViewModel
{
    /// <summary>
    /// Gets or sets the application cycle course identifier.
    /// </summary>
    /// <value>The application cycle course identifier.</value>
    public Guid? ApplicationCycleCourseId { get; set; }

    /// <summary>
    /// Gets or sets the application cycle identifier.
    /// </summary>
    /// <value>The application cycle identifier.</value>
    public Guid? ApplicationCycleId { get; set; }

    /// <summary>
    /// Gets or sets the course identifier.
    /// </summary>
    /// <value>The course identifier
    public Guid? CourseId { get; set; }

    /// <summary>
    /// Gets or sets the course.
    /// </summary>
    public CourseViewModel? Course { get; set; }

    /// <summary>
    /// Gets or sets the application cycle.
    /// </summary>
    public ApplicationCycleViewModel? ApplicationCycle { get; set; }

    /// <summary>
    /// Validates the application cycle course view model.
    /// </summary>
    /// <param name="routeId">Application cycle course identifierin the route.</param>
    /// <param name="validateId">Whether to validate the application cycle course identifier.</param>
    public ValidationResult Validate(Guid? routeId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        var isEmptyOrDefaultGuidApplicationCycleCourseId = !ApplicationCycleCourseId.HasValue && ApplicationCycleCourseId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidApplicationCycleCourseId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle course ID is required.";
        }

        if (validateId && ApplicationCycleCourseId != routeId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle course ID mismatch.";
        }

        if (ApplicationCycleId == null || ApplicationCycleId == Guid.Empty)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle ID is required.";
        }

        if (CourseId == null || CourseId == Guid.Empty)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course ID is required.";
        }

        return validationResult;
    }
}