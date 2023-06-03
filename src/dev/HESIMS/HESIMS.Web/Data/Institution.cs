namespace HESIMS.Web.Data;

/// <summary>
/// Represents Institution data.
/// </summary>
public class Institution
{
    /// <summary>
    /// Institution Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id of country where the institution is located.
    /// </summary>
    public Guid? CountryId { get; set; }

    /// <summary>
    /// Institution name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Country where the institution is located.
    /// </summary>
    public Country? Country { get; set; }
}