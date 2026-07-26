namespace VetCheckup.Infrastructure.Extensions;

using System.Security.Claims;
using VetCheckup.Domain.Enums;

public static class RolesExtensions
{
    public static string ToRoleName(this Roles role)
    {
        return role.ToString();
    }

    public static IEnumerable<string> GetAllRoleNames()
    {
        return Enum.GetValues<Roles>().Select(r => r.ToString());
    }

    public static bool IsInRole(this ClaimsPrincipal user, Roles role)
    {
        return user.IsInRole(role.ToString());
    }
}
