namespace HESIMS.Web.ViewModels;

/// <summary>
/// Country view model.
/// </summary>
[TypeConverter(typeof(CountryViewModelConverter))]
public record CountryViewModel
{
    /// <summary>
    /// Gets or sets the country identifier.
    /// </summary>
    public Guid? CountryId { get; set; }

    /// <summary>
    /// Gets or sets the country name.
    /// </summary>
    public string? CountryName { get; set; }

    /// <summary>
    /// Gets or sets the country code.
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the scholarships offered by the country.
    /// </summary>
    public IEnumerable<ScholarshipViewModel>? Scholarships { get; set; }

    /// <summary>
    /// Validates the country view model.
    /// </summary>
    /// <param name="routeId">Route Id.</param>
    /// <param name="validateId">Whether to validate the Id.</param>
    public Result<bool> Validate(Guid? routeId = null, bool validateId = false)
    {
        if (string.IsNullOrWhiteSpace(CountryName))
        {
            return Result<bool>.Failure("Country name is required.");
        }

        if (validateId)
        {
            var isEmptyOrDefaultGuidId = !CountryId.HasValue || CountryId == Guid.Empty;
            if (isEmptyOrDefaultGuidId)
            {
                return Result<bool>.Failure("Country ID is required.");
            }

            if (CountryId != routeId)
            {
                return Result<bool>.Failure("Country ID mismatch.");
            }
        }

        return Result<bool>.Success(true);
    }
}