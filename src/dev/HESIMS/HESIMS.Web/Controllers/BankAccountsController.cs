namespace HESIMS.Web.Controllers;

/// <summary>
/// Bank accounts controller.
/// </summary>
[Route("api/bankaccounts")]
[ApiController]
public class BankAccountsController : BaseController
{
    private readonly IBankAccountService bankAccountService;

    /// <summary>
    /// Instantiates a new instance of <see cref="BankAccountsController"/>.
    /// </summary>
    public BankAccountsController(IBankAccountService bankAccountService)
    {
        this.bankAccountService = bankAccountService;
    }

    /// <summary>
    /// Add new bank account.
    /// </summary>
    /// <param name="bankAccount">New bank account details.</param>
    /// <returns>New bank account details.</returns>
    [HttpPost]
    public async Task<IActionResult> AddBankAccountAsync([FromBody] BankAccountViewModel bankAccount)
    {
        var result = await bankAccountService.AddBankAccountAsync(new BankAccount
        {
            Id = Guid.NewGuid(),
            AccountName = bankAccount.AccountName,
            AccountNumber = bankAccount.AccountNumber,
            BankName = bankAccount.BankName,
            BranchName = bankAccount.BranchName,
            BankSwiftCode = bankAccount.BankSwiftCode,
            BranchAddress = bankAccount.BranchAddress,
            BranchCode = bankAccount.BranchCode,
            Currency = bankAccount.Currency,
            BranchSwiftCode = bankAccount.BranchSwiftCode,
            IntermediaryBankName = bankAccount.IntermediaryBankName,
            IntermediaryBankSwiftCode = bankAccount.IntermediaryBankSwiftCode,
            RoutingNumber = bankAccount.RoutingNumber,
            StudentId = bankAccount.StudentId
        });
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var newBankAccount = result.Value;
        return CreatedAtAction(nameof(GetBankAccountByIdAsync), new { id = bankAccount.BankAccountId }, newBankAccount);
    }

    /// <summary>
    /// Update bank account details.
    /// </summary>
    /// <param name="bankAccount">Bank account update details.</param>
    /// <returns>Updated bank account details.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBankAccountAsync([FromQuery] Guid id, [FromBody] BankAccountViewModel bankAccount)
    {
        var result = await bankAccountService.UpdateBankAccountAsync(new BankAccount
        {
            Id = id,
            AccountName = bankAccount.AccountName,
            AccountNumber = bankAccount.AccountNumber,
            BankName = bankAccount.BankName,
            BranchName = bankAccount.BranchName,
            BankSwiftCode = bankAccount.BankSwiftCode,
            BranchAddress = bankAccount.BranchAddress,
            BranchCode = bankAccount.BranchCode,
            Currency = bankAccount.Currency,
            BranchSwiftCode = bankAccount.BranchSwiftCode,
            IntermediaryBankName = bankAccount.IntermediaryBankName,
            IntermediaryBankSwiftCode = bankAccount.IntermediaryBankSwiftCode,
            RoutingNumber = bankAccount.RoutingNumber,
            StudentId = bankAccount.StudentId
        });
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(new BankAccountViewModel
        {
            BankAccountId = result.Value?.Id,
            AccountName = result.Value?.AccountName,
            AccountNumber = result.Value?.AccountNumber,
            BankName = result.Value?.BankName,
            BranchName = result.Value?.BranchName,
            BankSwiftCode = result.Value?.BankSwiftCode,
            BranchAddress = result.Value?.BranchAddress,
            BranchCode = result.Value?.BranchCode,
            Currency = result.Value?.Currency,
            BranchSwiftCode = result.Value?.BranchSwiftCode,
            IntermediaryBankName = result.Value?.IntermediaryBankName,
            IntermediaryBankSwiftCode = result.Value?.IntermediaryBankSwiftCode,
            RoutingNumber = result.Value?.RoutingNumber,
            StudentId = result.Value?.StudentId
        });
    }

    /// <summary>
    /// Get bank account by Id.
    /// </summary>
    /// <param name="id">Bank account ID.</param>
    /// <param name="studentId">Filter bank accounts by student ID.</param>
    /// <returns>Bank account if ID exist, otherwise failure result.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBankAccountByIdAsync(Guid id, [FromQuery] Guid? studentId = null)
    {
        var result = await bankAccountService.GetBankAccountByIdAsync(id, studentId);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(new BankAccountViewModel
        {
            BankAccountId = result.Value?.Id,
            AccountName = result.Value?.AccountName,
            AccountNumber = result.Value?.AccountNumber,
            BankName = result.Value?.BankName,
            BranchName = result.Value?.BranchName,
            BankSwiftCode = result.Value?.BankSwiftCode,
            BranchAddress = result.Value?.BranchAddress,
            BranchCode = result.Value?.BranchCode,
            Currency = result.Value?.Currency,
            BranchSwiftCode = result.Value?.BranchSwiftCode,
            IntermediaryBankName = result.Value?.IntermediaryBankName,
            IntermediaryBankSwiftCode = result.Value?.IntermediaryBankSwiftCode,
            RoutingNumber = result.Value?.RoutingNumber,
            StudentId = result.Value?.StudentId
        });
    }

    /// <summary>
    /// Get bank accounts.
    /// </summary>
    /// <param name="studentId">Filter bank accounts by student ID.</param>
    /// <returns>Bank accounts.</returns>
    [HttpGet]
    public async Task<IActionResult> GetBankAccountsAsync([FromQuery] Guid? studentId = null)
    {
        var result = await bankAccountService.GetBankAccountsAsync(studentId);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Value?.Select(bankAccount => new BankAccountViewModel
        {
            BankAccountId = bankAccount.Id,
            AccountName = bankAccount.AccountName,
            AccountNumber = bankAccount.AccountNumber,
            BankName = bankAccount.BankName,
            BranchName = bankAccount.BranchName,
            BankSwiftCode = bankAccount.BankSwiftCode,
            BranchAddress = bankAccount.BranchAddress,
            BranchCode = bankAccount.BranchCode,
            Currency = bankAccount.Currency,
            BranchSwiftCode = bankAccount.BranchSwiftCode,
            IntermediaryBankName = bankAccount.IntermediaryBankName,
            IntermediaryBankSwiftCode = bankAccount.IntermediaryBankSwiftCode,
            RoutingNumber = bankAccount.RoutingNumber,
            StudentId = bankAccount.StudentId
        }));
    }
}