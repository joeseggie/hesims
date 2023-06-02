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
    /// Id of the country offering the scholarship.
    /// </summary>
    public Guid? CountryId { get; set; }

    /// <summary>
    /// Country offering the scholarship.
    /// </summary>
    public CountryViewModel? Country { get; set; }

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

        if (!CountryId.HasValue || CountryId == Guid.Empty)
        {
            result = Result<bool>.Failure("Country ID is required.");
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