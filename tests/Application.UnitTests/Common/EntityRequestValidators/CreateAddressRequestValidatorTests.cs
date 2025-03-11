using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators;

public class CreateAddressRequestValidatorTests
{
    #region Fields

    private readonly IValidator<CreateAddressRequest> _createAddressRequestValidator = new CreateAddressRequestValidator();
    private readonly CreateAddressRequest _createAddressRequest = new CreateAddressRequest();

    #endregion

    #region Constructor Tests

    [Fact]
    public void Country_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createAddressRequest.Country = "This is a Valid Country";

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().BeEmpty();
    }

    [Fact]
    public void Country_IsNull_NoValidationFailure()
    {
        // Arrange
        _createAddressRequest.Country = null;

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().BeEmpty();
    }

    [Fact]
    public void Country_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createAddressRequest.Country = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.Country),
            AttemptedValue = _createAddressRequest.Country,
            ErrorMessage = "The length of 'Country' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void PostalCode_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createAddressRequest.PostalCode = "Valid PostalCode";

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().BeEmpty();
    }

    [Fact]
    public void PostalCode_IsNull_NoValidationFailure()
    {
        // Arrange
        _createAddressRequest.PostalCode = null;

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().BeEmpty();
    }

    [Fact]
    public void PostalCode_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createAddressRequest.PostalCode = new string('a', 21);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.PostalCode),
            AttemptedValue = _createAddressRequest.PostalCode,
            ErrorMessage = "The length of 'Postal Code' must be 20 characters or fewer. You entered 21 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void State_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createAddressRequest.State = "Valid State";

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().BeEmpty();
    }

    [Fact]
    public void State_IsNull_NoValidationFailure()
    {
        // Arrange
        _createAddressRequest.State = null;

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().BeEmpty();
    }

    [Fact]
    public void State_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createAddressRequest.State = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.State),
            AttemptedValue = _createAddressRequest.State,
            ErrorMessage = "The length of 'State' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void StreetAddress_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createAddressRequest.StreetAddress = "Valid StreetAddress";

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().BeEmpty();
    }

    [Fact]
    public void StreetAddress_IsNull_NoValidationFailure()
    {
        // Arrange
        _createAddressRequest.StreetAddress = null;

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().BeEmpty();
    }

    [Fact]
    public void StreetAddress_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createAddressRequest.StreetAddress = new string('a', 251);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.StreetAddress),
            AttemptedValue = _createAddressRequest.StreetAddress,
            ErrorMessage = "The length of 'Street Address' must be 250 characters or fewer. You entered 251 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void Suburb_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createAddressRequest.StreetAddress = "Valid Suburb";

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().BeEmpty();
    }

    [Fact]
    public void Suburb_IsNull_NoValidationFailure()
    {
        // Arrange
        _createAddressRequest.Suburb = null;

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().BeEmpty();
    }

    [Fact]
    public void Suburb_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createAddressRequest.Suburb = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.Suburb),
            AttemptedValue = _createAddressRequest.Suburb,
            ErrorMessage = "The length of 'Suburb' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
