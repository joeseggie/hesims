namespace HESIMS.Web.ViewModels;

/// <summary>
/// Scholarship application view model.
/// </summary>
public record ScholarshipApplicationViewModel
{
    /// <summary>
    /// Gets or sets the scholarship application identifier.
    /// </summary>
    /// <value>The scholarship application identifier.</value>
    public Guid? ScholarshipApplicationId { get; set; }

    /// <summary>
    /// Gets or sets the application cycle course identifier.
    /// </summary>
    /// <value>The application cycle course identifier.</value>
    public Guid? ApplicationCycleCourseId { get; set; }

    /// <summary>
    /// First name of the applicant.
    /// </summary>
    public string? Firstname { get; set; }

    /// <summary>
    /// Last name of the applicant.
    /// </summary>
    public string? Lastname { get; set; }

    /// <summary>
    /// Other names of the applicant.
    /// </summary>
    public string? OtherNames { get; set; }
}