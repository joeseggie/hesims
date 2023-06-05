namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents course level configuration.
/// </summary>
public class CourseLevelConfiguration : IEntityTypeConfiguration<CourseLevel>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<CourseLevel> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(50);
    }
}