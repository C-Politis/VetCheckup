using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{

    #region Constructors

    public CreateContactRequestValidator()
    {
        _ = this.RuleFor(e => e.Email)
            .MaximumLength(100)
            .When(e => e.Email != null);

        _ = this.RuleFor(e => e.Mobile)
            .MaximumLength(15)
            .When(e => e.Mobile != null);
    }

    #endregion

}
