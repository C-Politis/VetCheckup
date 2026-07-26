using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VetCheckup.Application.Common.Interfaces;
using VetCheckup.Application.Common.Models;

namespace VetCheckup.Infrastructure.Identity;

public class IdentityService(
    IAuthorizationService authorizationService,
    UserManager<User> userManager,
    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory) : IIdentityService
{
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory = userClaimsPrincipalFactory;

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

}
