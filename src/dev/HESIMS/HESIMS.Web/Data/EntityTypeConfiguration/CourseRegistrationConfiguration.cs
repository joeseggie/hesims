namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class CourseRegistrationConfiguration : IEntityTypeConfiguration<CourseRegistration>
{
    public void Configure(EntityTypeBuilder<CourseRegistration> builder)
    {
        builder.HasKey(courseRegistration => courseRegistration.Id);

        builder.Property(courseRegistration => courseRegistration.AcademicYear).IsRequired();

        builder.HasOne(courseRegistration => courseRegistration.StudentCourse)
            .WithMany(studentCourse => studentCourse.CourseRegistrations)
            .HasForeignKey(courseRegistration => courseRegistration.StudentCourseId)
            .IsRequired();
    }
}