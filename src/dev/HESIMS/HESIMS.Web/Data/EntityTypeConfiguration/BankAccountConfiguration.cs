namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the BankAccount entity.
/// </summary>
public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    /// <summary>
    /// Configure BankAccount entity.
    /// </summary>
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(bankAccount => bankAccount.Id);

        builder.Property(bankAccount => bankAccount.AccountNumber).IsRequired();        
        builder.Property(bankAccount => bankAccount.AccountName).IsRequired();
        builder.Property(bankAccount => bankAccount.Currency).IsRequired();
        builder.Property(bankAccount => bankAccount.BankName).IsRequired();
        builder.Property(bankAccount => bankAccount.BankSwiftCode).IsRequired();
        builder.Property(bankAccount => bankAccount.BranchName).IsRequired();
        builder.Property(bankAccount => bankAccount.BranchCode).IsRequired();
        builder.Property(bankAccount => bankAccount.BranchSwiftCode).IsRequired();

        builder.HasOne(bankAccount => bankAccount.Student)
            .WithMany(student => student.BankAccounts)
            .HasForeignKey(bankAccount => bankAccount.StudentId)
            .IsRequired();
    }
}