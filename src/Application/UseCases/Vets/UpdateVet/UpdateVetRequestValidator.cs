using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UseCases.Vets.UpdateVet;

public class UpdateVetRequestValidator : AbstractValidator<UpdateVetRequest>
{

    #region Constructors

    public UpdateVetRequestValidator()
    {
        _ = this.RuleFor(e => e.Address).SetValidator(new UpdateAddressRequestValidator());
        _ = this.RuleFor(e => e.ContactDetails).SetValidator(new UpdateContactRequestValidator());

        _ = this.RuleFor(e => e.Name)
            .MaximumLength(100)
            .NotEmpty()
            .When(e => e.Name != null);
    }

    #endregion

}
