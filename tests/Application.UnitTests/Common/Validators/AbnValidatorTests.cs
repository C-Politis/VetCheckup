using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using VetCheckup.Application.Common.Validators;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.Validators;

public class AbnValidatorTests
{

    #region Fields

    private string _abn = string.Empty;

    private readonly IValidator<string> _abnValidator = new AbnValidator();

    #endregion

    #region Validator Tests

    [Fact]
    public void ValidAbn_NoValidationFailures()
    {
        // Arrange
        _abn = "48123123124";

        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void EmptyAbn_NoValidationFailures()
    {
        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Abn_NotANumber_ValidationFailures()
    {
        // Arrange
        _abn = new string('a', 11);
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be an 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
            .Excluding(e => e.FormattedMessagePlaceholderValues)
            .Excluding(e => e.PropertyName));
    }

    [Fact]
    public void Abn_NotElevenDigits_ValidationFailures()
    {
        // Arrange
        _abn = "12345678";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be an 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
            .Excluding(e => e.FormattedMessagePlaceholderValues)
            .Excluding(e => e.PropertyName));
    }

    [Fact]
    public void Abn_ZeroLeadingDigit_ValidationFailures()
    {
        // Arrange
        _abn = "02110000000";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be an 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
            .Excluding(e => e.FormattedMessagePlaceholderValues)
            .Excluding(e => e.PropertyName));
    }

    [Fact]
    public void Abn_InvalidAbn_ValidationFailures()
    {
        // Arrange
        _abn = "12345678910";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _abn,
            ErrorCode = "PredicateValidator",
            ErrorMessage = "ABN is invalid"
        };

        // Act
        var result = _abnValidator.Validate(_abn);

        // Assert
        result.Errors
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
            .Excluding(e => e.FormattedMessagePlaceholderValues)
            .Excluding(e => e.PropertyName));
    }

    #endregion

}
