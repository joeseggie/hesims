namespace HESIMS.Web.ViewModels;

public record StudentCourseViewModel
{
    /// <summary>
    /// Student course Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Student Id.
    /// </summary>
    public Guid? StudentId { get; set; }

    /// <summary>
    /// Course Id.
    /// </summary>
    public Guid? CourseId { get; set; }

    /// <summary>
    /// Scholarship Id.
    /// </summary>
    public Guid? ScholarshipId { get; set; }

    /// <summary>
    /// Year when the student entered the course.
    /// </summary>
    public string? EntryYear { get; set; }

    /// <summary>
    /// Year when the student completed the course.
    /// </summary>
    public string? CompletionYear { get; set; }

    /// <summary>
    /// Institution's registration number for the student on the course.
    /// </summary>
    public string? CourseRegistrationNumber { get; set; }

    /// <summary>
    /// Student.
    /// </summary>
    public Student? Student { get; set; }

    /// <summary>
    /// Course.
    /// </summary>
    public Course? Course { get; set; }

    /// <summary>
    /// Scholarship.
    /// </summary>
    public Scholarship? Scholarship { get; set; }
}