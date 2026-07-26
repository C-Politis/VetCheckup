using Microsoft.AspNetCore.Identity;
using VetCheckup.Infrastructure.Extensions;

namespace VetCheckup.Infrastructure.Identity;

public class IdentitySeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (var roleName in RolesExtensions.GetAllRoleNames())
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }
}
