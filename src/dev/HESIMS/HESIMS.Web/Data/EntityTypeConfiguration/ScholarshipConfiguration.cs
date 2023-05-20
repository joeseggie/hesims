namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class ScholarshipConfiguration : IEntityTypeConfiguration<Scholarship>
{
    public void Configure(EntityTypeBuilder<Scholarship> builder)
    {
        builder.HasKey(scholarship => scholarship.Id);

        builder.Property(scholarship => scholarship.Country).IsRequired();

        builder.HasIndex(scholarship => scholarship.Name).IsUnique();
        builder.Property(scholarship => scholarship.Name).IsRequired();
    }
}
