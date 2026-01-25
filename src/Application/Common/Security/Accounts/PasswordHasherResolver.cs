using Microsoft.AspNetCore.Identity;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.Common.Security.Accounts;

public class PasswordHashResolver<TSource> : IValueResolver<TSource, User, string?> where TSource : class
{
    #region Fields

    private readonly IPasswordHasher<User> _passwordHasher;

    #endregion

    #region Constructors

    public PasswordHashResolver(IPasswordHasher<User> passwordHasher)
    {
        this._passwordHasher = passwordHasher;
    }

    #endregion

    #region Methods

    public string? Resolve(TSource source, User destination, string? destMember, ResolutionContext context)
    {
        var password = typeof(TSource).GetProperty("Password")?.GetValue(source) as string;

        return string.IsNullOrWhiteSpace(password) 
            ? null 
            : _passwordHasher.HashPassword(destination, password);
    }

    #endregion
}
