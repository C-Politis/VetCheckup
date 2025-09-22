using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Infrastructure.Data;

namespace VetCheckup.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // The fallback of ConnectionStrings.DefaultConnection matches the JSON section in the consumers appsettings.json. If the file does not have it, this will be null.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<IApplicationDbContext, ApplicationApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        using var _ServiceProvider = services.BuildServiceProvider();
        {
            var _DbContext = _ServiceProvider.GetRequiredService<ApplicationApplicationDbContext>();
            _DbContext.Database.Migrate();
        }

        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddSingleton(TimeProvider.System);
        //services.AddTransient<IIdentityService, IdentityService>();

        //services.AddAuthorization(options =>
        //    options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        return services;
    }
}
