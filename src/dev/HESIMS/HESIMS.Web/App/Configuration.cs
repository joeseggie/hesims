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
        SeedDatabaseAsync(context, userManager, roleManager).ConfigureAwait(false).GetAwaiter().GetResult();

        return app;
    }

    private static async Task SeedDatabaseAsync(ApplicationDbContext? dbContext, UserManager<ApplicationUser>? userManager, RoleManager<IdentityRole>? roleManager)
    {
        if (dbContext is null || userManager is null || roleManager is null)
        {
            // TODO: Log invalid arguments.
            return;
        }

        var adminCredentialsQueryResult = TryGetAdminCredentialsFromEnv();
        if (!adminCredentialsQueryResult.IsSuccess)
        {
            // TODO: Log failure to get admin credentials from environment.
            return;
        }

        var adminCredentials = adminCredentialsQueryResult.Value;
        if (adminCredentials is null)
        {
            // TODO: Log query for admin credentials from environment returned null.
            return;
        }
        var adminEmail = adminCredentials[0];
        var adminPassword = adminCredentials[1];

        var createAdminUserResult = await CreateAdminUserAsync(dbContext, adminEmail, adminPassword);
        if (!createAdminUserResult.IsSuccess)
        {
            // TODO: Log failure message.
            return;
        }
        var adminUser = createAdminUserResult.Value;

        var assignAdminRoleResult = await AssignAdminRoleToUserAsync(dbContext, adminUser);
        if (!assignAdminRoleResult.IsSuccess)
        {
            // TODO: Log failure message.
            return;
        }
    }

    private static async Task<Result<bool>> AssignAdminRoleToUserAsync(ApplicationDbContext? dbContext, ApplicationUser? adminUser)
    {
        if (adminUser is null)
        {
            return Result<bool>.Failure("Admin user object is null.");
        }

        if (dbContext is null)
        {
            return Result<bool>.Failure("Database context is null");
        }

        var adminRole = await dbContext.Roles.FirstOrDefaultAsync(role => role.Name == "Admin");
        if (adminRole is null)
        {
            var addRoleResult = await dbContext.Roles.AddAsync(new IdentityRole
            { 
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN" }
            );
            await dbContext.SaveChangesAsync();

            adminRole = addRoleResult.Entity;
        }

        var adminUserHasAdminRole = await dbContext.UserRoles.AnyAsync(userRole => userRole.UserId == adminUser.Id && userRole.RoleId == adminRole.Id);
        if (adminUserHasAdminRole)
        {
            return Result<bool>.Failure("Admin user is assigned to the Admin role already.");
        }

        var addUserRoleResult = await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
        { 
            UserId = adminUser.Id, 
            RoleId = adminRole.Id 
        });
        await dbContext.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    private static async Task<Result<ApplicationUser>> CreateAdminUserAsync(ApplicationDbContext? dbContext, string adminEmail, string adminPassword)
    {
        if (dbContext is null)
        {
            return Result<ApplicationUser>.Failure("Database context is null.");
        }

        var adminUserExists = await dbContext.Users.AnyAsync(user => user.Email == adminEmail || user.UserName == adminEmail);
        if (adminUserExists)
        {
            return Result<ApplicationUser>.Failure("Admin user already exists");
        }

        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = adminEmail,
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpper(),
            NormalizedUserName = adminEmail.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin",
        };
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, adminPassword);
        var result = await dbContext.Users.AddAsync(adminUser);
        await dbContext.SaveChangesAsync();

        return Result<ApplicationUser>.Success(result.Entity);
    }

    private static Result<string[]> TryGetAdminCredentialsFromEnv()
    {
        var adminCredentialsFromEnv = Environment.GetEnvironmentVariable("HESIMS_ADMIN_CREDENTIALS");
        if (string.IsNullOrEmpty(adminCredentialsFromEnv))
        {
            return Result<string[]>.Failure("Admin credentials do not exist in the environment");
        }

        var adminCredentials = adminCredentialsFromEnv.Split(':');
        if (adminCredentials.Length != 2)
        {
            return Result<string[]>.Failure("Admin credentials are not setup in expected format");
        }

        return Result<string[]>.Success(adminCredentials);
    }
}