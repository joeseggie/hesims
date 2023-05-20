namespace HESIMS.Web.ViewModels;

/// <summary>
/// Represents the view model for the application cycle.
/// </summary>
public record ApplicationCycleViewModel
{
    /// <summary>
    /// Application cycle Id.
    /// </summary>
    public Guid? ApplicationCycleId { get; set; }

    /// <summary>
    /// Application cycle scholarship Id.
    /// </summary>
    public Guid? ScholarshipId { get; set; }

    /// <summary>
    /// Application cycle academic year.
    /// </summary>
    public string? AcademicYear { get; set; }

    /// <summary>
    /// Validates the application cycle view model.
    /// </summary>
    /// <param name="applicationCycleId">Application cycle Id.</param>
    /// <param name="scholarshipId">Scholarship Id.</param>
    public ValidationResult Validate(Guid? routeId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        if (string.IsNullOrWhiteSpace(AcademicYear))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Academic year is required.";
        }

        var isEmptyOrDefaultGuidScholarshipId = !ScholarshipId.HasValue && ScholarshipId == Guid.Empty;
        if (isEmptyOrDefaultGuidScholarshipId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Scholarship ID is required.";
        }

        var isEmptyOrDefaultGuidApplicationCycleId = !ApplicationCycleId.HasValue && ApplicationCycleId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidApplicationCycleId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle ID is required.";
        }

        if (validateId && ApplicationCycleId != routeId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Application cycle ID mismatch.";
        }
        
        return validationResult;
    }
}