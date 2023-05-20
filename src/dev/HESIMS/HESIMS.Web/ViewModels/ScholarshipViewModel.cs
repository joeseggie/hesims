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
    public ValidationResult Validate()
    {
        var validationResult = new ValidationResult
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
        
        return validationResult;
    }
}