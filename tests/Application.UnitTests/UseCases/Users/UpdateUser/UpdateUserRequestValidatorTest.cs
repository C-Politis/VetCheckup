using FluentAssertions;
using FluentValidation;
using VetCheckup.Application.UseCases.Users.UpdateUser;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Users;

public class UpdateUserRequestValidatorTest
{
    #region Fields

    private readonly IValidator<UpdateUserRequest> _validator = new UpdateUserRequestValidator();
    private readonly UpdateUserRequest _updateUserRequest = new()
        {
            Username = "newusername",
            UserType = UserType.OrganisationManager,
            Email = "bob@bobson.com",
            Password = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855" // SHA-256 hash of an empty string
        };

    #endregion
    
    
    #region Validator Tests

    [Theory]
    [InlineData("validusername")]
    [InlineData(null)]
    public void Username_ValidInput_NoValidationFailures(string? username)
    {
        // Arrange
        _updateUserRequest.Username = username;

        // Act
        var result = _validator.Validate(_updateUserRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Username), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Username_ExceedsMaxLength_ValidationFailures()
    {
        //Arrange
        _updateUserRequest.Username = new string('a', 51);
        var expectedFailure = new FluentValidation.Results.ValidationFailure()
        {
            PropertyName = nameof(UpdateUserRequest.Username),
            AttemptedValue = _updateUserRequest.Username,
            ErrorMessage = "The length of 'Username' must be 20 characters or fewer. You entered 51 characters.",
            ErrorCode = "MaximumLengthValidator"
        };
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Username), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }
    
    [Fact]
    public void Username_IsEmpty_ValidationFailures()
    {
        //Arrange
        _updateUserRequest.Username = string.Empty;
        var expectedFailure = new FluentValidation.Results.ValidationFailure()
        {
            PropertyName = nameof(UpdateUserRequest.Username),
            AttemptedValue = _updateUserRequest.Username,
            ErrorMessage = "'Username' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Username), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }
    
    [Fact]
    public void Password_ValidInput_NoValidationFailures()
    {
        // Act
        var result = _validator.Validate(_updateUserRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Password), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }


    [Fact]
    public void Password_IsNotHashed_ValidationFailures()
    {
        //Act
        _updateUserRequest.Password = "NotAHash";
        var expectedFailure = new FluentValidation.Results.ValidationFailure()
        {
            PropertyName = nameof(UpdateUserRequest.Password),
            AttemptedValue = _updateUserRequest.Password,
            ErrorMessage = "Password must be hashed.",
            ErrorCode = "MustValidator"
        };
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors.Should().ContainSingle(e =>
            e.PropertyName.Equals(nameof(UpdateUserRequest.Password), StringComparison.OrdinalIgnoreCase) &&
            e.ErrorMessage == "Password must be hashed.");
    }

    [Fact]
    public void Password_IsEmpty_ValidationFailures()
    {
        _updateUserRequest.Password = null;
        var expectedFailure = new FluentValidation.Results.ValidationFailure()
        {
            PropertyName = nameof(UpdateUserRequest.Password),
            AttemptedValue = _updateUserRequest.Password,
            ErrorMessage = "'Password' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Password), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    [Fact]
    public void Email_ValidInput_NoValidationFailures()
    {
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }
    
    [Fact]
    public void Email_IsEmpty_ValidationFailures()
    {
        //Arrange
        _updateUserRequest.Email = string.Empty;
        
        var expectedFailure = new FluentValidation.Results.ValidationFailure()
        {
            PropertyName = nameof(UpdateUserRequest.Email),
            AttemptedValue = _updateUserRequest.Email,
            ErrorMessage = "'Email' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Theory]
    [MemberData(nameof(UserType_ValidInput_NoValidationFailures_Data))]
    public void UserType_ValidInput_NoValidationFailures(UserType userType)
    {
        //Arrange
        _updateUserRequest.UserType = userType;
        
        //Act
        var result = _validator.Validate(_updateUserRequest);
        
        //Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateUserRequest.UserType), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }
    
    public static IEnumerable<object[]> UserType_ValidInput_NoValidationFailures_Data()
    {
        foreach (UserType userType in Enum.GetValues(typeof(UserType)))
            yield return new object[] { userType };
    }
    
    #endregion
    
}
