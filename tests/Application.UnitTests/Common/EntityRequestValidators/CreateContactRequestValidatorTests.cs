﻿using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators;

public class CreateContactRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateContactRequest> _createContactRequestValidator = new CreateContactRequestValidator();
    private readonly CreateContactRequest _createContactRequest = new()
    {
        Email = string.Empty,
        Mobile = string.Empty
    };

    #endregion

    #region Constructor Tests

    [Fact]
    public void Email_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createContactRequest.Email = "Valid@Email.Com.Au";

        // Act
        var result = _createContactRequestValidator.Validate(_createContactRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Email_IsEmpty_NoValidationFailures()
    {
        // Act
        var result = _createContactRequestValidator.Validate(_createContactRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Email_ExceedsMaxLength_ValidationFailure()
    {
        // Arrange
        _createContactRequest.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateContactRequest.Email),
            AttemptedValue = _createContactRequest.Email,
            ErrorMessage = "The length of 'Email' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createContactRequestValidator.Validate(_createContactRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Fact]
    public void Mobile_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createContactRequest.Mobile = "0420123456";

        // Act
        var result = _createContactRequestValidator.Validate(_createContactRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Mobile_IsNotANumber_NoValidationFailures()
    {
        // Arrange
        _createContactRequest.Mobile = "WrongNumber";
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateContactRequest.Mobile),
            AttemptedValue = _createContactRequest.Mobile,
            ErrorMessage = "'Mobile' is not in the correct format.",
            ErrorCode = "RegularExpressionValidator"
        };

        // Act
        var result = _createContactRequestValidator.Validate(_createContactRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    #endregion

}
