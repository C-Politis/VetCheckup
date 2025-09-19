using System.ComponentModel;

namespace VetCheckup.Application.UseCases.Users.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    #region Constructors

    public UpdateUserRequestValidator()
    {
        _ = this.RuleFor(e => e.Username)
            .MaximumLength(20)
            .NotEmpty()
            .When(e => e.Username != null);
        
        _ = RuleFor(e => e.Password)
            .NotEmpty()
            .Must(p => p != null && IsHashed(p))
            .WithMessage("Password must be hashed.");
        
        _ = this.RuleFor(e => e.UserType)
            .IsInEnum();

        _ = this.RuleFor(e => e.Email)
            .MaximumLength(100)
            .NotEmpty();
    }
    
    #endregion

    #region Methods
    
    private bool IsHashed(string password)
        => password.Length == 64 && System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-fA-F0-9]{64}$");
    
    #endregion
    
}
