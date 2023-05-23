namespace HESIMS.Web.ViewModels;

/// <summary>
/// Student view model.
/// </summary>
public record StudentViewModel
{
    public Guid? StudentId { get; set; }

    public Guid? ApplicantId { get; set; }

    public string? StudentNumber { get; set; }

    public string? Address { get; set; }

    /// <summary>
    /// Validates the student view model.
    /// </summary>
    /// <param name="studentId">Student Id.</param>
    /// <param name="validateId">Whether to validate the Id.</param>
    public ValidationResult Validate(Guid? routeId = null, bool validateId = false)
    {
        var validationResult = new ValidationResult
        {
            IsValid = true
        };

        return validationResult;
    }
}