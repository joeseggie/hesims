namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.HasKey(institution => institution.Id);

        builder.HasOne(institution => institution.Country)
            .WithMany(country => country.Institutions)
            .HasForeignKey(institution => institution.CountryId);
            
        builder.Property(institution => institution.Name).IsRequired();
    }
}