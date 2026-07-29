using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using VetCheckup.Application.Common.Interfaces;

namespace VetCheckup.Infrastructure.Identity;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
