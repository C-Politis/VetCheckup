using VetCheckup.Application.Common.EntityRequests;

namespace VetCheckup.Application.Common.EntityRequestValidators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{

    #region Constructors

    public CreateUserRequestValidator()
    {
        _ = this.RuleFor(e => e.UserName)
            .MaximumLength(20);

        _ = this.RuleFor(e => e.UserType)
            .IsInEnum();
    }

    #endregion

}
