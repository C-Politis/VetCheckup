using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.CreateOwner;

public class CreateOwnerRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateOwnerRequest> _createOwnerRequestValidator = new CreateOwnerRequestValidator();
    private readonly CreateOwnerRequest _createOwnerRequest = new()
    {
        Address = new(),
        ContactDetails = new(),
        Name = string.Empty,
        DateOfBirth = DateTime.MinValue
    };

    #endregion

    #region Constructor Tests

    [Fact]
    public void Name_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createOwnerRequest.Name = "Valid Name";

        // Act
        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Name_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createOwnerRequest.Name = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOwnerRequest.Name),
            AttemptedValue = _createOwnerRequest.Name,
            ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    [Fact]
    public void Name_IsEmpty_ValidationFailures()
    {
        // Arrange
        _createOwnerRequest.Name = string.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOwnerRequest.Name),
            AttemptedValue = _createOwnerRequest.Name,
            ErrorMessage = "'Name' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };

        // Act
        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    #endregion

}
