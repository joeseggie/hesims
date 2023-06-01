namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represent configuration of the country entity.
/// </summary>
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    /// <summary>
    /// Configure country entity.
    /// </summary>
    /// <param name="builder">Entity type builder.</param>
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(country => country.Id);
        builder.Property(country => country.Name).IsRequired();
    }
}