namespace HESIMS.Web.Data;

/// <summary>
/// Represents bank account data.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Bank account's Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Bank account's account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Bank account's account name.
    /// </summary>
    public string? AccountName { get; set; }

    /// <summary>
    /// Bank account's currency.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Bank account's bank name.
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// Bank's swift code.
    /// </summary>
    public string? BankSwiftCode { get; set; }

    /// <summary>
    /// Bank's branch name.
    /// </summary>
    public string? BranchName { get; set; }

    /// <summary>
    /// Bank's branch address.
    /// </summary>
    public string? BranchAddress { get; set; }

    /// <summary>
    /// Bank's branch code.
    /// </summary>
    public string? BranchCode { get; set; }

    /// <summary>
    /// Bank's branch swift code.
    /// </summary>
    public string? BranchSwiftCode { get; set; }

    /// <summary>
    /// Intermediary bank name.
    /// </summary>
    public string? IntermediaryBankName { get; set; }

    /// <summary>
    /// Intermediary bank swift code.
    /// </summary>
    public string? IntermediaryBankSwiftCode { get; set; }

    /// <summary>
    /// Bank's routing number.
    /// </summary>
    public string? RoutingNumber { get; set; }

    /// <summary>
    /// Student's Id associated with this bank account.
    public Guid? StudentId { get; set; }

    /// <summary>
    /// Student associated with this bank account.
    /// </summary>
    public Student? Student { get; set; }
}