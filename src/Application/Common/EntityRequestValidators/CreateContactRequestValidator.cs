using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{

    #region Constructors

    public CreateContactRequestValidator()
    {
        _ = this.RuleFor(e => e.Email)
            .MaximumLength(100)
            .When(e => !string.IsNullOrWhiteSpace(e.Email));

        _ = this.RuleFor(e => e.Mobile)
            .Matches("\\d{1,15}")
            .When(e => !string.IsNullOrWhiteSpace(e.Mobile));
    }

    #endregion

}
