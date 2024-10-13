using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;
public class CreateOwnerRequestValidation : AbstractValidator<CreateOwnerRequest>
{

    #region Constructors

    public CreateOwnerRequestValidation()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.Contact).SetValidator(new CreateContactRequestValidator());

        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty();
    }

    #endregion

}
