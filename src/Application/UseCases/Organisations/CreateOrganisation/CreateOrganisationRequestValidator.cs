using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationRequestValidator : AbstractValidator<CreateOrganisationRequest>
{

    #region Constructors

    public CreateOrganisationRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new CreateContactRequestValidator());

        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty();

        _ = this.RuleFor(e => e.OrganisationType)
            .IsInEnum();
    }

    #endregion

}
