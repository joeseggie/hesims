namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the student entity.
/// </summary>
public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    /// <summary>
    /// Configure applicant entity.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(student => student.Id);

        builder.Property(student => student.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(student => student.LastName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(student => student.NIN)
            .HasMaxLength(50)
            .IsRequired();
    }
}