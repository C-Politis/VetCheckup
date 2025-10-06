using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Enums;
using VetCheckup.Infrastructure.Data;

namespace VetCheckup.Infrastructure;

public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // The fallback of ConnectionStrings.DefaultConnection matches the JSON section in the consumers appsettings.json. If the file does not have it, this will be null.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        using var _ServiceProvider = services.BuildServiceProvider();
        {
            var _DbContext = _ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _DbContext.Database.Migrate();
        }

        services
            .AddIdentity<User, IdentityRole>(options => { 
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var rolemanager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
        if (!await rolemanager.RoleExistsAsync(UserType.Owner.ToString()))
            await rolemanager.CreateAsync(new IdentityRole(UserType.Owner.ToString()));
        if (!await rolemanager.RoleExistsAsync(UserType.OrganisationManager.ToString()))
            await rolemanager.CreateAsync(new IdentityRole(UserType.OrganisationManager.ToString()));
        if (!await rolemanager.RoleExistsAsync(UserType.Vet.ToString()))
            await rolemanager.CreateAsync(new IdentityRole(UserType.Vet.ToString()));

        //services.AddSingleton(TimeProvider.System);
        //services.AddTransient<IIdentityService, IdentityService>();

        //services.AddAuthorization(options =>
        //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
