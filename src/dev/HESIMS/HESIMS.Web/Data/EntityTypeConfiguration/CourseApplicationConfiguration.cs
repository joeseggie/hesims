namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the course application entity.
/// </summary>
public class CourseApplicationConfiguration : IEntityTypeConfiguration<CourseApplication>
{
    public void Configure(EntityTypeBuilder<CourseApplication> builder)
    {
        builder.HasKey(courseApplication => courseApplication.Id);
        
        builder.HasOne(courseApplication => courseApplication.Applicant)
               .WithMany(applicant => applicant.CourseApplications)
               .HasForeignKey(courseApplication => courseApplication.ApplicantId);

        builder.HasOne(courseApplication => courseApplication.ApplicationCycleCourse)
               .WithMany(applicationCycleCoure => applicationCycleCoure.CourseApplications)
               .HasForeignKey(courseApplication => courseApplication.ApplicationCycleCourseId);
    }
}
