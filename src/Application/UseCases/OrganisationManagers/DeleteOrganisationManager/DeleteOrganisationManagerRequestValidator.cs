namespace VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager;

public class DeleteOrganisationManagerRequestValidator : AbstractValidator<DeleteOrganisationManagerRequest>
{

    #region Constructors

    public DeleteOrganisationManagerRequestValidator()
    {
        _ = this.RuleFor(e => e.OrganisationManagerId)
            .NotNull()
            .NotEqual(Guid.Empty);
    }

    #endregion

}
