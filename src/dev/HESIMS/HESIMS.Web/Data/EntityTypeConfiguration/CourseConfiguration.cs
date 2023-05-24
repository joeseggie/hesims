namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represent configuration of the course entity.
/// </summary>
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    /// <summary>
    /// Configure course entity.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(course => course.Id);
        builder.Property(course => course.Name).IsRequired();
        builder.Property(course => course.Institution).IsRequired();
        builder.Property(course => course.InstitutionCountry).IsRequired();
    }
}
