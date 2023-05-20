namespace HESIMS.Web.Common;

/// <summary>
/// Represents the validation result.
/// </summary>
public record ValidationResult
{
    /// <summary>
    /// Validation result is a success.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Error message if validation fails.
    /// </summary>
    public string? ErrorMessage { get; set; }
}