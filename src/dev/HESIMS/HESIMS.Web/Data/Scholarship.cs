namespace HESIMS.Web.Data;

/// <summary>
/// Represents the scholarship data.
/// </summary>
public class Scholarship
{
    /// <summary>
    /// Scholarship Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Country offering the scholarship.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Scholarship name.
    /// </summary>
    public string? Name { get; set; }
}
