using VetCheckup.Application.Common.Security.Accounts;
using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserProfile: Profile
{
    
    #region Constructors

    public UpdateUserProfile()
        => CreateMap<UpdateUserRequest, User>()
        .ForMember(destination => destination.UserType, source => source.Ignore())
        .ForMember(destination => destination.Id, source => source.Ignore())
        .ForMember(destination => destination.PasswordHash, source => source.MapFrom<PasswordHashResolver<UpdateUserRequest>>())
        .ForMember(destination => destination.NormalizedUserName, source => source.Ignore())
        .ForMember(destination => destination.Email, source => source.Ignore())
        .ForMember(destination => destination.NormalizedEmail, source => source.Ignore())
        .ForMember(destination => destination.EmailConfirmed, source => source.Ignore())
        .ForMember(destination => destination.SecurityStamp, source => source.Ignore())
        .ForMember(destination => destination.ConcurrencyStamp, source => source.Ignore())
        .ForMember(destination => destination.PhoneNumber, source => source.Ignore())
        .ForMember(destination => destination.PhoneNumberConfirmed, source => source.Ignore())
        .ForMember(destination => destination.TwoFactorEnabled, source => source.Ignore())
        .ForMember(destination => destination.LockoutEnd, source => source.Ignore())
        .ForMember(destination => destination.LockoutEnabled, source => source.Ignore())
        .ForMember(destination => destination.AccessFailedCount, source => source.Ignore());


    #endregion
    
}
