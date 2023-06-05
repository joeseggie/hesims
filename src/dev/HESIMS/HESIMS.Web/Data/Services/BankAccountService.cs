namespace HESIMS.Web.Data.Services;

public interface IBankAccountService
{
    /// <summary>
    /// Add new bank account.
    /// </summary>
    /// <param name="newBankAccount">New bank account details.</param>
    /// <returns>New bank account details.</returns>
    Task<Result<BankAccount>> AddBankAccountAsync(BankAccount newBankAccount);

    /// <summary>
    /// Update bank account details.
    /// </summary>
    /// <param name="bankAccountUpdate">Bank account update details.</param>
    /// <returns>Updated bank account details.</returns>
    Task<Result<BankAccount>> UpdateBankAccountAsync(BankAccount bankAccountUpdate);

    /// <summary>
    /// Get bank account by Id.
    /// </summary>
    /// <param name="id">Bank account ID.</param>
    /// <returns>Bank account if ID exist, otherwise failure result.</returns>
    Task<Result<BankAccount>> GetBankAccountByIdAsync(Guid id);

    /// <summary>
    /// Get all bank accounts.
    /// </summary>
    /// <returns>List of bank accounts.</returns>
    Task<Result<IEnumerable<BankAccount>>> GetBankAccountsAsync();
}

/// <summary>
/// Bank account service.
/// </summary>
public class BankAccountService : IBankAccountService
{
    private readonly ApplicationDbContext db;

    /// <summary>
    /// Instantiates a new instance of <see cref="BankAccountService"/>.
    /// </summary>
    public BankAccountService(ApplicationDbContext dbContext)
    {
        this.db = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<BankAccount>> AddBankAccountAsync(BankAccount newBankAccount)
    {
        var entityEntry = await db.BankAccounts.AddAsync(newBankAccount);
        await db.SaveChangesAsync();

        return Result<BankAccount>.Success(entityEntry.Entity);
    }

    /// <inheritdoc/>
    public async Task<Result<BankAccount>> UpdateBankAccountAsync(BankAccount bankAccountUpdate)
    {
        var bankAccountToUpdate = await db.BankAccounts.FindAsync(bankAccountUpdate.Id);
        if (bankAccountToUpdate == null)
        {
            return Result<BankAccount>.Failure($"Bank account with ID {bankAccountUpdate.Id} not found.");
        }

        db.Entry(bankAccountToUpdate).CurrentValues.SetValues(bankAccountUpdate);
        await db.SaveChangesAsync();

        return Result<BankAccount>.Success(bankAccountToUpdate);
    }

    /// <inheritdoc/>
    public async Task<Result<BankAccount>> GetBankAccountByIdAsync(Guid id)
    {
        var bankAccount = await db.BankAccounts.FindAsync(id);
        if (bankAccount == null)
        {
            return Result<BankAccount>.Failure($"Bank account with ID {id} not found.");
        }

        return Result<BankAccount>.Success(bankAccount);
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<BankAccount>>> GetBankAccountsAsync()
    {
        var bankAccounts = await db.BankAccounts.ToListAsync();
        return Result<IEnumerable<BankAccount>>.Success(bankAccounts);
    }
}