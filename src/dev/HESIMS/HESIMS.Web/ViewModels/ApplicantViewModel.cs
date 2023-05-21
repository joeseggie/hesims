namespace HESIMS.Web.ViewModels;

public record ApplicantViewModel
{
    public Guid? ApplicantId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? OtherNames { get; set; }

    /// <summary>
    /// Validates the applicant view model.
    /// </summary>
    /// <param name="applicantId">Applicant Id.</param>
    /// <param name="validateId">Whether to validate the Id.</param>
    public ValidationResult Validate(Guid? routeId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        var isEmptyOrDefaultGuidApplicantId = !ApplicantId.HasValue && ApplicantId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidApplicantId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Applicant ID is required.";
        }

        if (validateId && ApplicantId != routeId)
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Applicant ID mismatch.";
        }

        if (string.IsNullOrWhiteSpace(Firstname))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "First name is required.";
        }

        if (string.IsNullOrWhiteSpace(Lastname))
        {
            validationResult.IsValid = false;
            validationResult.ErrorMessage = "Last name is required.";
        }

        return validationResult;
    }
}