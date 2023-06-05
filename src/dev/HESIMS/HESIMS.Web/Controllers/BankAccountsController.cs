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
    /// <param name="newBankAccount">New bank account details.</param>
    /// <returns>New bank account details.</returns>
    [HttpPost]
    public async Task<IActionResult> AddBankAccountAsync([FromBody] BankAccount newBankAccount)
    {
        var result = await bankAccountService.AddBankAccountAsync(newBankAccount);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetBankAccountByIdAsync), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>
    /// Update bank account details.
    /// </summary>
    /// <param name="bankAccountUpdate">Bank account update details.</param>
    /// <returns>Updated bank account details.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(BankAccount), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBankAccountAsync([FromBody] BankAccount bankAccountUpdate)
    {
        var result = await bankAccountService.UpdateBankAccountAsync(bankAccountUpdate);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get bank account by Id.
    /// </summary>
    /// <param name="id">Bank account ID.</param>
    /// <returns>Bank account if ID exist, otherwise failure result.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BankAccount), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBankAccountByIdAsync(Guid id)
    {
        var result = await bankAccountService.GetBankAccountByIdAsync(id);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}