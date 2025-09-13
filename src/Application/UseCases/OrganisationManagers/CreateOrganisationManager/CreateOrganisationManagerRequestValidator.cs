using VetCheckup.Application.Common.EntityRequestValidators;
using VetCheckup.Application.Common.Validators;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

namespace VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerRequestValidator : AbstractValidator<CreateOrganisationManagerRequest>
{

    #region Constructors

    public CreateOrganisationManagerRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new CreateContactRequestValidator());

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
