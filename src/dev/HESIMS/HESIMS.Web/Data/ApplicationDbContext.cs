namespace HESIMS.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // This is required for IdentityDbContext to work.

        new ScholarshipConfiguration().Configure(builder.Entity<Scholarship>());
        new CourseConfiguration().Configure(builder.Entity<Course>());
        new ApplicantConfiguration().Configure(builder.Entity<Applicant>());
        new ApplicationCycleConfiguration().Configure(builder.Entity<ApplicationCycle>());
        new ApplicationCycleCourseConfiguration().Configure(builder.Entity<ApplicationCycleCourse>());
        new CourseApplicationConfiguration().Configure(builder.Entity<CourseApplication>());
    }
}