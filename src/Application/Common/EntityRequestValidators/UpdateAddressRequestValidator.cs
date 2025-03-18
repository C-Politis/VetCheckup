using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
{

    #region Constructors

    public UpdateAddressRequestValidator()
    {
        _ = this.RuleFor(e => e.Country)
            .MaximumLength(100)
            .When(e => !string.IsNullOrWhiteSpace(e.Country));

        _ = this.RuleFor(e => e.PostalCode)
            .MaximumLength(20)
            .When(e => !string.IsNullOrWhiteSpace(e.PostalCode));

        _ = this.RuleFor(e => e.State)
            .MaximumLength(100)
            .When(e => !string.IsNullOrWhiteSpace(e.State));

        _ = this.RuleFor(e => e.StreetAddress)
            .MaximumLength(250)
            .When(e => !string.IsNullOrWhiteSpace(e.StreetAddress));

        _ = this.RuleFor(e => e.Suburb)
            .MaximumLength(100)
            .When(e => !string.IsNullOrWhiteSpace(e.Suburb));
    }

    #endregion

}
