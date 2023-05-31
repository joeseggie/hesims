namespace HESIMS.Web.ViewModels;

/// <summary>
/// Represents the scholarship view model.
/// </summary>
public record ScholarshipViewModel
{
    /// <summary>
    /// Scholarship Id.
    /// </summary>
    public Guid? ScholarshipId { get; set; }

    /// <summary>
    /// Scholarship name.
    /// </summary>
    public string? ScholarshipName { get; set; }

    /// <summary>
    /// Country offering the scholarship.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Validates the scholarship view model.
    /// </summary>
    /// <param name="routeScholarshipId">Scholarship Id with the request route.</param>
    /// <param name="validateId">Validate Id.</param>
    public HESIMS.Web.Common.ValidationResult Validate(Guid? routeScholarshipId = null, bool validateId = false)
    {
        var validationResult = new HESIMS.Web.Common.ValidationResult
        {
            IsValid = true
        };

        if (string.IsNullOrWhiteSpace(ScholarshipName))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Scholarship name is required.";
        }

        if (string.IsNullOrWhiteSpace(Country))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Country is required.";
        }

        var isEmptyOrDefaultGuidScholarshipId = !ScholarshipId.HasValue && ScholarshipId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidScholarshipId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Scholarship ID is required.";
        }

        if (validateId && ScholarshipId != routeScholarshipId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Scholarship ID mismatch.";
        }
        
        return validationResult;
    }
}