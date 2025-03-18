using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
{

    #region Constructors

    public UpdateAddressRequestValidator()
    {
        _ = this.RuleFor(e => e.Country)
            .MaximumLength(100)
            .When(e => e.Country != null);

        _ = this.RuleFor(e => e.PostalCode)
            .MaximumLength(20)
            .When(e => e.PostalCode != null);

        _ = this.RuleFor(e => e.State)
            .MaximumLength(100)
            .When(e => e.State != null);

        _ = this.RuleFor(e => e.StreetAddress)
            .MaximumLength(250)
            .When(e => e.StreetAddress != null);

        _ = this.RuleFor(e => e.Suburb)
            .MaximumLength(100)
            .When(e => e.Suburb != null);
    }

    #endregion

}
