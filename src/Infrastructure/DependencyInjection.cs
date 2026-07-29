using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetCheckup.Application.Common.Authorization;
using VetCheckup.Application.Common.Interfaces;
using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Enums;
using VetCheckup.Infrastructure.Data;
using VetCheckup.Infrastructure.Identity;

namespace VetCheckup.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration.GetSection("ConnectionStrings")["DefaultConnection"];

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddSingleton(TimeProvider.System);
        services.AddDataProtection();
        services.AddHttpContextAccessor();

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUser, CurrentUserService>();

        services.AddAuthorizationCore(ConfigurePolicies);

        using var _ServiceProvider = services.BuildServiceProvider();
        {
            var _DbContext = _ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _DbContext.Database.Migrate();
        }

        return services;
    }

    private static void ConfigurePolicies(AuthorizationOptions options)
    {
        options.AddPolicy(Policies.CanManageVets, policy =>
            policy.RequireRole(Roles.Administrator.ToString(), Roles.OrganisationManager.ToString()));

        options.AddPolicy(Policies.CanManageOwners, policy =>
            policy.RequireRole(Roles.Administrator.ToString(), Roles.OrganisationManager.ToString()));

        options.AddPolicy(Policies.CanManagePets, policy =>
            policy.RequireRole(Roles.Administrator.ToString(), Roles.OrganisationManager.ToString(), Roles.Owner.ToString()));

        options.AddPolicy(Policies.CanManageOrganisations, policy =>
            policy.RequireRole(Roles.Administrator.ToString()));

        options.AddPolicy(Policies.CanViewMedicalRecords, policy =>
            policy.RequireRole(Roles.Administrator.ToString(), Roles.Vet.ToString(), Roles.OrganisationManager.ToString()));

    }
}
