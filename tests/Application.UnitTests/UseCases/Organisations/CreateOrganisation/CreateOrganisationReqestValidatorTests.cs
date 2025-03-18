using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationReqestValidatorTests
{

    #region Alright then keep your secrets

    private readonly IValidator<CreateOrganisationRequest> _createOrganisationRequestValidator = new CreateOrganisationRequestValidator();
    private readonly CreateOrganisationRequest _createOrganisationRequest = new()
    {
        Abn = string.Empty,
        Address = new()
        {
            StreetAddress = string.Empty,
            Country = string.Empty,
            PostalCode = string.Empty,
            State = string.Empty,
            Suburb = string.Empty,
        },
        ContactDetails = new()
        {
            Email = string.Empty,
            Mobile = string.Empty
        },
        Name = string.Empty,
        OrganisationType = Domain.Enums.OrganisationType.Clinic
    };

    #endregion

    #region Constructor Tests

    [Theory]
    [InlineData("51824753556")]
    [InlineData("")]
    public void Abn_ValidInput_NoValidationFailures(string abn)
    {
        // Arrange
        _createOrganisationRequest.Abn = abn;

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
        _createOrganisationRequest.Name = "Giant Eagles Rescue";

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
