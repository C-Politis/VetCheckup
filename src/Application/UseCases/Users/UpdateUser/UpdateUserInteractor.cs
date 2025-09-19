using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserInteractor(IDbContext context, IMapper mapper) : IRequestHandler<UpdateUserRequest>
{

    #region Methods

    Task IRequestHandler<UpdateUserRequest>.Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        UserType userType = request.UserType;
        string email = request.Email;
        
        User? user = userType switch
        {
            UserType.OrganisationManager => context.Get<OrganisationManager>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            UserType.Vet => context.Get<Vet>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            UserType.Owner => context.Get<Owner>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            _ => throw new Exception("User type not supported")
        };

        if (user is null)
            throw new Exception("User not Found");
        
        mapper.Map(request, user);
        
        return Task.CompletedTask;
    }
    
    #endregion
    
}
