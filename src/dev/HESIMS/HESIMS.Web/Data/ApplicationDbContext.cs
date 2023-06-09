namespace HESIMS.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Scholarship> Scholarships { get; set; } = default!;

    public DbSet<Course> Courses { get; set; } = default!;

    public DbSet<Student> Students { get; set; } = default!;

    public DbSet<Country> Countries { get; set; } = default!;

    public DbSet<Institution> Institutions { get; set; } = default!;

    public DbSet<CourseLevel> CourseLevels { get; set; } = default!;

    public DbSet<StudentCourse> StudentCourses { get; set; } = default!;

    public DbSet<StudentContact> StudentContacts { get; set; } = default!;

    public DbSet<BankAccount> BankAccounts { get; set; } = default!;

    public DbSet<CourseRegistration> CourseRegistrations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // This is required for IdentityDbContext to work.

        new ScholarshipConfiguration().Configure(builder.Entity<Scholarship>());
        new CourseConfiguration().Configure(builder.Entity<Course>());
        new CountryConfiguration().Configure(builder.Entity<Country>());
        new InstitutionConfiguration().Configure(builder.Entity<Institution>());
        new CourseLevelConfiguration().Configure(builder.Entity<CourseLevel>());
        new StudentConfiguration().Configure(builder.Entity<Student>());
        new StudentCourseConfiguration().Configure(builder.Entity<StudentCourse>());
        new StudentContactConfiguration().Configure(builder.Entity<StudentContact>());
        new BankAccountConfiguration().Configure(builder.Entity<BankAccount>());
    }
}