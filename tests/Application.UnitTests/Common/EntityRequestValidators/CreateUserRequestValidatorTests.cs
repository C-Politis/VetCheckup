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
        UserType = UserType.Organisation,
        Password = "Password"
    };

    #endregion

    #region Is it safe?

    [Fact]
    public void Name_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createUserRequest.UserName = "Valid Name";

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.UserName), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(UserType_ValidInput_NoValidationFailures_TestData))]
    public void UserType_ValidInput_NoValidationFailures(UserType UserType)
    {
        // Arrange
        _createUserRequest.UserType = UserType;

        // Act
        var result = _createUserRequestValidator.Validate(_createUserRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateUserRequest.UserType), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    public static IEnumerable<object[]> UserType_ValidInput_NoValidationFailures_TestData()
        => new[]
        {
                new object[] { UserType.Organisation },
                new object[] { UserType.Vet },
                new object[] { UserType.Owner }
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
    #endregion

}
