namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class ScholarshipConfiguration : IEntityTypeConfiguration<Scholarship>
{
    public void Configure(EntityTypeBuilder<Scholarship> builder)
    {
        builder.HasKey(scholarship => scholarship.Id);

        builder.HasOne(scholarship => scholarship.Country)
            .WithMany(country => country.Scholarships)
            .HasForeignKey(scholarship => scholarship.CountryId);
            
        builder.Property(scholarship => scholarship.Name).IsRequired();
    }
}
