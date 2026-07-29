using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Users.Login;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Users.Login;

public class LoginRequestValidatorTests
{
    private readonly IValidator<LoginRequest> _validator = new LoginRequestValidator();
    private readonly LoginRequest _request = new()
    {
        UserName = string.Empty,
        Password = string.Empty
    };

    [Fact]
    public void UserName_ValidInput_NoValidationFailures()
    {
        _request.UserName = "ValidUser";

        var result = _validator.Validate(_request);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(LoginRequest.UserName), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Password_ValidInput_NoValidationFailures()
    {
        _request.Password = new string('a', 32);

        var result = _validator.Validate(_request);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(LoginRequest.Password), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void UserName_ExceedsMaxLength_ValidationFailures()
    {
        _request.UserName = new string('a', 21);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(LoginRequest.UserName),
            AttemptedValue = _request.UserName,
            ErrorMessage = "The length of 'User Name' must be 20 characters or fewer. You entered 21 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _validator.Validate(_request);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(LoginRequest.UserName), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void Password_ExceedsMaxLength_ValidationFailures()
    {
        _request.Password = new string('a', 33);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(LoginRequest.Password),
            AttemptedValue = _request.Password,
            ErrorMessage = "The length of 'Password' must be 32 characters or fewer. You entered 33 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _validator.Validate(_request);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(LoginRequest.Password), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }
}
