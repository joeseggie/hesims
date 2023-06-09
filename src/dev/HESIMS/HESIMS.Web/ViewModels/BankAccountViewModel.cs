namespace HESIMS.Web.ViewModels;

public record BankAccountViewModel
{
    /// <summary>
    /// Bank account id.
    /// </summary>
    public Guid? BankAccountId { get; set; }

    /// <summary>
    /// Bank account name.
    /// </summary>
    public string? AccountName { get; set; }

    /// <summary>
    /// Bank account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Bank account currency.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Bank name.
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// Bank SWIFT code.
    /// </summary>
    public string? BankSwiftCode { get; set; }

    /// <summary>
    /// Bank branch name.
    /// </summary>
    public string? BranchName { get; set; }

    /// <summary>
    /// Bank branch address.
    /// </summary>
    public string? BranchAddress { get; set; }

    /// <summary>
    /// Bank branch code.
    /// </summary>
    public string? BranchCode { get; set; }

    /// <summary>
    /// Bank branch SWIFT code.
    /// </summary>
    public string? BranchSwiftCode { get; set; }

    /// <summary>
    /// Intermediary bank name.
    /// </summary>
    public string? IntermediaryBankName { get; set; }

    /// <summary>
    /// Intermediary bank SWIFT code.
    /// </summary>
    public string? IntermediaryBankSwiftCode { get; set; }

    /// <summary>
    /// Bank account's routing number.
    /// </summary>
    public string? RoutingNumber { get; set; }

    /// <summary>
    /// Student id.
    /// </summary>
    public Guid? StudentId { get; set; }

    /// <summary>
    /// Student owning this bank account.
    /// </summary>
    public StudentViewModel? Student { get; set; }
}