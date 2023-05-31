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
    public Result<bool> Validate(Guid? routeScholarshipId = null, bool validateId = false)
    {
        var result = Result<bool>.Success(true);

        if (string.IsNullOrWhiteSpace(ScholarshipName))
        {
            result = Result<bool>.Failure("Scholarship name is required.");
        }

        if (string.IsNullOrWhiteSpace(Country))
        {
            result = Result<bool>.Failure("Country is required.");
        }

        var isEmptyOrDefaultGuidScholarshipId = !ScholarshipId.HasValue || ScholarshipId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidScholarshipId)
        {
            result = Result<bool>.Failure("Scholarship ID is required.");
        }

        if (validateId && ScholarshipId != routeScholarshipId)
        {
            result = Result<bool>.Failure("Scholarship ID mismatch.");
        }

        return result;
    }
}