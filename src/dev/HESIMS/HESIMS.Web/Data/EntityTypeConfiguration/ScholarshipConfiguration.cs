namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class ScholarshipConfiguration : IEntityTypeConfiguration<Scholarship>
{
    public void Configure(EntityTypeBuilder<Scholarship> builder)
    {
        builder.HasKey(scholarship => scholarship.Id);
        builder.HasIndex(scholarship => scholarship.Country).IsUnique();
        builder.Property(scholarship => scholarship.Country).IsRequired();
    }
}
