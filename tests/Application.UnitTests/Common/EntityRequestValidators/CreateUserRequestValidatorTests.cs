using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators;

public class CreateUserRequestValidatorTests
{
    #region Is it secret?

    private readonly IValidator<CreateUserRequest> _createUserRequestValidator = new CreateUserRequestValidator();
    private readonly CreateUserRequest _createUserRequest = new CreateUserRequest()
    {
        UserName = string.Empty,
        Role = Roles.OrganisationManager,
        Password = "Password"
    };

    #endregion

    #region Is it safe?

    [Fact]
    public void UserName_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createUserRequest.UserName = "Valid Name";

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.UserName), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Password_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createUserRequest.Password = new string('a', 32);

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.Password), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(Role_ValidInput_NoValidationFailures_TestData))]
    public void Role_ValidInput_NoValidationFailures(Roles Role)
    {
        // Arrange
        _createUserRequest.Role = Role;

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.Role), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    public static IEnumerable<object[]> Role_ValidInput_NoValidationFailures_TestData()
        => new[]
        {
                new object[] { Roles.OrganisationManager },
                new object[] { Roles.Vet },
                new object[] { Roles.Owner }
        };


    [Fact]
    public void UserName_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createUserRequest.UserName = new string('a', 21);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateUserRequest.UserName),
            AttemptedValue = _createUserRequest.UserName,
            ErrorMessage = "The length of 'User Name' must be 20 characters or fewer. You entered 21 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.UserName), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void Password_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createUserRequest.Password = new string('a', 33);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateUserRequest.Password),
            AttemptedValue = _createUserRequest.Password,
            ErrorMessage = "The length of 'Password' must be 32 characters or fewer. You entered 33 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.Password), StringComparison.OrdinalIgnoreCase))
             .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
