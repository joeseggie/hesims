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
    }
}