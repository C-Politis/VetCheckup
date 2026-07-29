using Microsoft.AspNetCore.Identity;
using VetCheckup.Application.Common.Interfaces;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Infrastructure.Identity;

public class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User?> AuthenticateAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return null;
        }

        var passwordMatches = await _userManager.CheckPasswordAsync(user, password);

        return passwordMatches ? user : null;
    }
}
