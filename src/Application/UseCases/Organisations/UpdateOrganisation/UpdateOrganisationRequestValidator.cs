using VetCheckup.Application.Common.EntityRequestValidators;
using VetCheckup.Application.Common.Validators;

namespace VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;

public class UpdateOrganisationRequestValidator : AbstractValidator<UpdateOrganisationRequest>
{

    #region Constructors

    public UpdateOrganisationRequestValidator()
    {
        _ = this.RuleFor(e => e.Abn).SetValidator(new AbnValidator());
        _ = this.RuleFor(e => e.Address).SetValidator(new UpdateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new UpdateContactRequestValidator());

        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty()
            .When(e => e.Name != null);

        _ = this.RuleFor(e => e.OrganisationType)
            .IsInEnum()
            .When(e => e.OrganisationType != null);
    }

    #endregion

}
