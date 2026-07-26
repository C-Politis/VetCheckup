using VetCheckup.Application.Services.Persistence;
using VetCheckup.Domain.Entities;
using VetCheckup.Domain.Enums;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserInteractor(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdateUserRequest>
{

    #region Methods

    Task IRequestHandler<UpdateUserRequest>.Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        Roles role = request.Role;
        string email = request.Email;
        
        User? user = role switch
        {
            Roles.OrganisationManager => context.Get<OrganisationManager>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            Roles.Vet => context.Get<Vet>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            Roles.Owner => context.Get<Owner>().AsEnumerable().FirstOrDefault(e => e.ContactDetails.Email == email)?.User,
            _ => throw new Exception("User type not supported")
        };

        if (user is null)
            throw new Exception("User not Found");
        
        mapper.Map(request, user);
        
        return Task.CompletedTask;
    }
    
    #endregion
    
}
