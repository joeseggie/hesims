namespace HESIMS.Web.ViewModels;

/// <summary>
/// Represents the course level view model.
/// </summary>
public record CourseLevelViewModel
{
    /// <summary>
    /// Course level Id.
    /// </summary>
    public Guid? CourseLevelId { get; set; }

    /// <summary>
    /// Course level description.
    /// </summary>
    public string? CourseLevelDescription { get; set; }

    /// <summary>
    /// Validates the course level view model.
    /// </summary>
    /// <param name="routeId">Id of course level in the route.</param>
    /// <param name="validateId">Whether to validate the course level Id.</param>
    /// <returns>Validation result.</returns>
    public Result<bool> Validate(Guid? routeId = null, bool validateId = false)
    {
        var result = Result<bool>.Success(true);

        if (string.IsNullOrWhiteSpace(CourseLevelDescription))
        {
            result = Result<bool>.Failure("Course level name is required.");
        }

        var isEmptyOrDefaultGuidCourseLevelId = !CourseLevelId.HasValue || CourseLevelId == Guid.Empty;
        if (validateId && isEmptyOrDefaultGuidCourseLevelId)
        {
            result = Result<bool>.Failure("Course level ID is required.");
        }

        if (validateId && CourseLevelId != routeId)
        {
            result = Result<bool>.Failure("Course level ID mismatch.");
        }

        return result;
    }
}