using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
{

    #region Constructors

    public CreateAddressRequestValidator()
    {
        _ = this.RuleFor(e => e.Country)
            .MaximumLength(100);

        _ = this.RuleFor(e => e.PostalCode)
            .MaximumLength(20);

        _ = this.RuleFor(e => e.State)
            .MaximumLength(100);

        _ = this.RuleFor(e => e.StreetAddress)
            .MaximumLength(250);

        _ = this.RuleFor(e => e.Suburb)
            .MaximumLength(100);
    }

    #endregion

}
