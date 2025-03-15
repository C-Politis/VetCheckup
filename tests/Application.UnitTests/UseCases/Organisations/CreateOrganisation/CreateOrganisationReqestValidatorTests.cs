using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationReqestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateOrganisationRequest> _createOrganisationRequestValidator = new CreateOrganisationRequestValidator();
    private readonly CreateOrganisationRequest _createOrganisationRequest = new()
    {
        Abn = string.Empty,
        Address = new(),
        ContactDetails = new(),
        Name = string.Empty,
        OrganisationType = Domain.Enums.OrganisationType.Clinic
    };

    #endregion

    #region Constructor Tests

    [Fact]
    public void Abn_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createOrganisationRequest.Abn = "51824753556";

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.Abn), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Abn_IsEmpty_NoValidationFailures()
    {
        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.Abn), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Abn_NotANumber_ValidationFailures()
    {
        // Arrange
        _createOrganisationRequest.Abn = new string('a', 11);
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationRequest.Abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be an 11 digit number"
        };

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

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
        _createOrganisationRequest.Abn = "12345678";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationRequest.Abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be an 11 digit number"
        };

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

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
        _createOrganisationRequest.Abn = "12345678910";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationRequest.Abn,
            ErrorCode = "PredicateValidator",
            ErrorMessage = "ABN is invalid"
        };

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
            .Excluding(e => e.FormattedMessagePlaceholderValues)
            .Excluding(e => e.PropertyName));
    }

    [Fact]
    public void Name_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createOrganisationRequest.Name = "Valid Name";

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Name_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createOrganisationRequest.Name = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOrganisationRequest.Name),
            AttemptedValue = _createOrganisationRequest.Name,
            ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    [Fact]
    public void Name_IsEmpty_ValidationFailures()
    {
        // Arrange
        _createOrganisationRequest.Name = string.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOrganisationRequest.Name),
            AttemptedValue = _createOrganisationRequest.Name,
            ErrorMessage = "'Name' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Theory]
    [MemberData(nameof(OrganisationType_ValidInput_NoValidationFailures_TestData))]
    public void OrganisationType_ValidInput_NoValidationFailures(OrganisationType organisationType)
    {
        // Arrange
        _createOrganisationRequest.OrganisationType = organisationType;

        // Act
        var result = _createOrganisationRequestValidator.Validate(_createOrganisationRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateOrganisationRequest.OrganisationType), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    public static IEnumerable<object[]> OrganisationType_ValidInput_NoValidationFailures_TestData()
        => new[]
        {
                new object[] { OrganisationType.Clinic },
                new object[] { OrganisationType.Daycare },
                new object[] { OrganisationType.Shelter },
                new object[] { OrganisationType.Rescues },
                new object[] { OrganisationType.Other },
        };

    #endregion

}
