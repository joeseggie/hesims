namespace HESIMS.Web.ViewModels;

/// <summary>
/// Represents Institution data.
/// </summary>
public record InstitutionViewModel
{
    /// <summary>
    /// Institution Id.
    /// </summary>
    public Guid? InstitutionId { get; set; }

    /// <summary>
    /// Id of country where the institution is located.
    /// </summary>
    public Guid? CountryId { get; set; }

    /// <summary>
    /// Institution name.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// Country where the institution is located.
    /// </summary>
    public CountryViewModel? Country { get; set; }

    /// <summary>
    /// Validate the institution view model.
    /// </summary>
    public Result<bool> Validate(Guid? routeId = null, bool validateId = false)
    {
        if (string.IsNullOrWhiteSpace(InstitutionName))
        {
            return Result<bool>.Failure("Institution name is required.");
        }

        if (!CountryId.HasValue || CountryId == Guid.Empty)
        {
            return Result<bool>.Failure("Country ID is required.");
        }

        if (validateId)
        {
            var isEmptyOrDefaultGuidId = !InstitutionId.HasValue || InstitutionId == Guid.Empty;
            if (isEmptyOrDefaultGuidId)
            {
                return Result<bool>.Failure("Institution ID is required.");
            }

            if (InstitutionId != routeId)
            {
                return Result<bool>.Failure("Institution ID mismatch.");
            }
        }

        return Result<bool>.Success(true);
    }
}