namespace HESIMS.Web.ViewModels;

/// <summary>
/// Student details view model.
/// </summary>
public record StudentDetailsViewModel
{
    public ApplicantViewModel? Applicant { get; set; }

    public CourseApplicationViewModel? CourseApplication { get; set; }
}