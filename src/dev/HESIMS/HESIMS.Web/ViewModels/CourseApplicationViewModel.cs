namespace HESIMS.Web.ViewModels;

/// <summary>
/// Course application view model.
/// </summary>
public record CourseApplicationViewModel
{
    public Guid? CourseApplicationId { get; set; }

    public Guid? ApplicationCycleCourseId { get; set; }

    public Guid? ApplicantId { get; set; }

    public DateTimeOffset? ApplicationDateTime { get; set; }

    /// <summary>
    /// Validates the course application view model.
    /// </summary>
    /// <param name="routeId">Course application identifier in the route.</param>
    /// <param name="validateId">Whether to validate the course application identifier.</param>
    public ValidationResult Validate(Guid? routeId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        var isEmptyOrDefaultGuidCourseApplicationId = !CourseApplicationId.HasValue && CourseApplicationId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidCourseApplicationId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course application ID is required.";
        }

        if (validateId && CourseApplicationId != routeId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Course application ID mismatch.";
        }

        if (ApplicationCycleCourseId == null || ApplicationCycleCourseId == Guid.Empty)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle course ID is required.";
        }

        if (ApplicantId == null || ApplicantId == Guid.Empty)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Applicant ID is required.";
        }

        return validationResult;
    }
}