namespace HESIMS.Web.Configuration;

public static class Configuration
{

    public static WebApplicationBuilder? RegisterAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<IScholarshipService, ScholarshipService>();
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<IInstitutionService, InstitutionService>();
        builder.Services.AddScoped<ICourseLevelService, CourseLevelService>();
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<IBankAccountService, BankAccountService>();
        builder.Services.AddScoped<IStudentContactService, StudentContactService>();
        builder.Services.AddScoped<IStudentCourseService, StudentCourseService>();
        builder.Services.AddScoped<ICourseRegistrationService, CourseRegistrationService>();

        return builder;
    }

    public static WebApplication? InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        context.Database.Migrate();
        SeedDatabase(app, userManager, roleManager);

        return app;
    }

    private static void SeedDatabase(WebApplication app, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        ;
    }
}
