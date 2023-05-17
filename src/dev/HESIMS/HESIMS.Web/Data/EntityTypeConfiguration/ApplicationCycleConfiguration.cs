namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the application cycle entity.
/// </summary>
public class ApplicationCycleConfiguration : IEntityTypeConfiguration<ApplicationCycle>
{
    public void Configure(EntityTypeBuilder<ApplicationCycle> builder)
    {
        builder.Property(applicationCycle => applicationCycle.AcademicYear).IsRequired();

        builder.HasOne(applicationCycle => applicationCycle.Scholarship)
               .WithMany(scholarship => scholarship.ApplicationCycles)
               .HasForeignKey(applicationCycle => applicationCycle.ScholarshipId);
    }
}
