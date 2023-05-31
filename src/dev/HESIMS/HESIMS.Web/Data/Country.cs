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
}