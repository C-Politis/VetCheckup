using VetCheckup.Domain.Entities;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserProfile: Profile
{
    
    #region Constructors

    public UpdateUserProfile()
        => CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.UserId, opts => opts.Ignore());

    #endregion
    
}
