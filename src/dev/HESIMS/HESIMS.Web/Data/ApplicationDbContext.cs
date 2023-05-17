namespace HESIMS.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Scholarship> Scholarships { get; set; } = default!;

    public DbSet<Course> Courses { get; set; } = default!;

    public DbSet<Applicant> Applicants { get; set; } = default!;

    public DbSet<ApplicationCycle> ApplicationCycles { get; set; } = default!;

    public DbSet<ApplicationCycleCourse> ApplicationCycleCourses { get; set; } = default!;

    public DbSet<CourseApplication> CourseApplications { get; set; } = default!;

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