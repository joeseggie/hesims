namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class ApplicationCycleCourseConfiguration : IEntityTypeConfiguration<ApplicationCycleCourse>
{
    public void Configure(EntityTypeBuilder<ApplicationCycleCourse> builder)
    {
        builder.HasKey(applicationCycleCourse => applicationCycleCourse.Id);

        builder.HasOne(applicationCycleCourse => applicationCycleCourse.ApplicationCycle)
               .WithMany(applicationCycle => applicationCycle.ApplicationCycleCourses)
               .HasForeignKey(applicationCycleCourse => applicationCycleCourse.ApplicationCycleId);

        builder.HasOne(applicationCycleCourse => applicationCycleCourse.Course)
               .WithMany(course => course.ApplicationCycleCourses)
               .HasForeignKey(applicationCycleCourse => applicationCycleCourse.CourseId);
    }
}
