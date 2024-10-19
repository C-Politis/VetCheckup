using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;

public class CreateOwnerRequestValidator : AbstractValidator<CreateOwnerRequest>
{

    #region Constructors

    public CreateOwnerRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new CreateContactRequestValidator());

        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty();
    }

    #endregion

}
