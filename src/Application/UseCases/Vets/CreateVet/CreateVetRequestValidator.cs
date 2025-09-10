using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Vets.CreateVet;

public class CreateVetRequestValidator : AbstractValidator<CreateVetRequest>
{
    #region Constructors

    public CreateVetRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new CreateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new CreateContactRequestValidator());
        _ = this.RuleFor(e => e.User).SetValidator(new CreateUserRequestValidator());

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
