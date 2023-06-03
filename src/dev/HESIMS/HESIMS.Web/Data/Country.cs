namespace HESIMS.Web.Data;

/// <summary>
/// Model represent Country data.
/// </summary>
public class Country
{
    /// <summary>
    /// Country's Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Country's name.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Country's code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Country's scholarships.
    /// </summary>
    public IEnumerable<Scholarship>? Scholarships { get; set; }

    /// <summary>
    /// Country's institutions.
    /// </summary>
    public IEnumerable<Institution>? Institutions { get; set; }
}