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
        
        builder.HasOne(course => course.CourseLevel)
               .WithMany(courseLevel => courseLevel.Courses)
               .HasForeignKey(course => course.CourseLevelId);
        
        builder.HasOne(course => course.Institution)
               .WithMany(institution => institution.Courses)
               .HasForeignKey(course => course.InstitutionId);
    }
}
