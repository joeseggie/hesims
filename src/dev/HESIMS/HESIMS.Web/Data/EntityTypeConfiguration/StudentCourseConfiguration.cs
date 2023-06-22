namespace HESIMS.Web.Data.EntityTypeConfiguration;

/// <summary>
/// Represents configuration of the StudentCourse entity.
/// </summary>
public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    /// <summary>
    /// Configure studentCourse entity.
    /// </summary>
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.HasKey(studentCourse => studentCourse.Id);

        builder.Property(studentCourse => studentCourse.EntryYear).IsRequired();

        builder.HasOne(studentCourse => studentCourse.Student)
            .WithMany(student => student.StudentCourses)
            .HasForeignKey(studentCourse => studentCourse.StudentId)
            .IsRequired();
        
        builder.HasOne(studentCourse => studentCourse.Course)
            .WithMany(course => course.StudentCourses)
            .HasForeignKey(studentCourse => studentCourse.CourseId)
            .IsRequired();
        
        builder.HasOne(studentCourse => studentCourse.Scholarship)
            .WithMany(scholarship => scholarship.StudentCourses)
            .HasForeignKey(studentCourse => studentCourse.ScholarshipId)
            .IsRequired();
    }
}