using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerRequestValidator : AbstractValidator<UpdateOrganisationManagerRequest>
{
    #region Constructors

    public UpdateOrganisationManagerRequestValidator()
    {
        _ = this.RuleFor(e => e.OrganisationManagerId)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        _ = this.RuleFor(e => e.Address).SetValidator(new UpdateAddressRequestValidator()!);
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new UpdateContactRequestValidator()!);

        _ = this.RuleFor(e => e.FirstName)
            .MaximumLength(100)
            .NotEmpty();

        _ = this.RuleFor(e => e.MiddleName)
            .MaximumLength(100);

        _ = this.RuleFor(e => e.LastName)
            .MaximumLength(100)
            .NotEmpty();

        _ = this.RuleFor(e => e.Title)
            .IsInEnum();

        _ = this.RuleFor(e => e.Suffix)
            .IsInEnum();
    }

    #endregion
}
