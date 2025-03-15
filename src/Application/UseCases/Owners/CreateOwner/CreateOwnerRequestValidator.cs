using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Owners.CreateOwner;

public class CreateOwnerRequestValidator : AbstractValidator<CreateOwnerRequest>
{

    #region Constructors

    public CreateOwnerRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new CreateContactRequestValidator());

        _ = this.RuleFor(e => e.FirstName)
            .MaximumLength(100)
            .NotEmpty();

        _ = this.RuleFor(e => e.MiddleName)
            .MaximumLength(100)
            .NotEmpty();
        
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
