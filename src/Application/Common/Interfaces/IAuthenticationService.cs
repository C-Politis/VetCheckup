using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<User?> AuthenticateAsync(string userName, string password);
}
