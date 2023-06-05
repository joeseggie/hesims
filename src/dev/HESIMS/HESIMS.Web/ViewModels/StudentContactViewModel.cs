namespace HESIMS.Web.ViewModels;

public record StudentContactViewModel
{
    /// <summary>
    /// Student contact Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Student Id.
    /// </summary>
    public Guid? StudentId { get; set; }

    /// <summary>
    /// Student's contact type.
    /// </summary>
    public string? ContactType { get; set; }

    /// <summary>
    /// Contact value.
    /// </summary>
    public string? ContactValue { get; set; }

    /// <summary>
    /// Student.
    /// </summary>
    public Student? Student { get; set; }
}