namespace VetCheckup.Application.UseCases.Users.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        _ = RuleFor(e => e.UserName)
            .NotEmpty()
            .MaximumLength(20);

        _ = RuleFor(e => e.Password)
            .NotEmpty()
            .MaximumLength(32);
    }
}
