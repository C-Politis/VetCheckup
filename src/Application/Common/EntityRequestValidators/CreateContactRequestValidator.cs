using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{

    #region Constructors

    public CreateContactRequestValidator()
    {
        _ = this.RuleFor(e => e.Email)
            .MaximumLength(100);

        _ = this.RuleFor(e => e.Mobile)
            .Matches("^\\d{0,15}$");
    }

    #endregion

}
