namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the applicant entity.
/// </summary>
public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    /// <summary>
    /// Configure applicant entity.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasKey(applicant => applicant.Id);
        builder.Property(applicant => applicant.Firstname).IsRequired();
        builder.Property(applicant => applicant.Lastname).IsRequired();

        builder.HasOne(applicant => applicant.Student)
               .WithOne(student => student.Applicant)
               .HasForeignKey<Student>(student => student.ApplicantId);
    }
}
