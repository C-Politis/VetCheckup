using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Domain.Enums;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using Xunit;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerRequestValidatorTests
{
    
    #region Fields

    private readonly IValidator<CreateOrganisationManagerRequest> _createOrganisationManagerRequestValidator= new CreateOrganisationManagerRequestValidator();
    private readonly CreateOrganisationManagerRequest _createOrganisationManagerRequest = new()
    {
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
        FirstName = "Test",
        LastName = "Owner",
        MiddleName = "Middle",
        Suffix = Suffix.Esq,
        Title = Title.Dr,
        DateOfBirth = DateTime.MinValue,
        Organisation = new()
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
        }
    };

    #endregion

    #region Happy Eleventy First!

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'First Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateFirstName(string firstName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createOrganisationManagerRequest.FirstName = firstName;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'Last Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Last Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateLastName(string lastName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createOrganisationManagerRequest.LastName = lastName;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", true, "")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateMiddleName(string middleName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createOrganisationManagerRequest.MiddleName = middleName;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [MemberData(nameof(Title_ValidInput_NoValidationFailures_TestData))]
    public void Title_ValidInput_NoValidationFailures(Title title)
    {
        // Arrange
        _createOrganisationManagerRequest.Title = title;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Title), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(Suffix_ValidInput_NoValidationFailures_TestData))]
    public void Suffix_ValidInput_NoValidationFailures(Suffix suffix)
    {
        // Arrange
        _createOrganisationManagerRequest.Suffix = suffix;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Suffix), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }
    public static IEnumerable<object[]> Suffix_ValidInput_NoValidationFailures_TestData()
        =>
        [
            [Suffix.None],
                    [Suffix.Esq],
                    [Suffix.Jr],
                    [Suffix.Sr],
                    [Suffix.II],
                    [Suffix.III],
                    [Suffix.IV],
                    [Suffix.Other]
        ];

    public static IEnumerable<object[]> Title_ValidInput_NoValidationFailures_TestData()
        =>
        [
            [Title.None],
                    [Title.Mr],
                    [Title.Mrs],
                    [Title.Ms],
                    [Title.Miss],
                    [Title.Dr],
                    [Title.Prof],
                    [Title.Rev],
                    [Title.Hon],
                    [Title.Other]
        ];

    [Theory]
    [InlineData("51824753556")]
    [InlineData("")]
    public void Abn_ValidInput_NoValidationFailures(string abn)
    {
        // Arrange
        _createOrganisationManagerRequest.Organisation!.Abn = abn;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationManagerRequest.Organisation.Abn), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Abn_NotANumber_ValidationFailures()
    {
        // Arrange
        _createOrganisationManagerRequest.Organisation!.Abn = new string('a', 11);
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationManagerRequest.Organisation.Abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be a valid 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

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
        _createOrganisationManagerRequest.Organisation!.Abn = "12345678";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationManagerRequest.Organisation.Abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be a valid 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

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
        _createOrganisationManagerRequest.Organisation!.Abn = "02110000000";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationManagerRequest.Organisation!.Abn,
            ErrorCode = "RegularExpressionValidator",
            ErrorMessage = "ABN must be a valid 11 digit number and cannot begin with 0."
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

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
        _createOrganisationManagerRequest.Organisation!.Abn = "12345678910";
        var expectedFailure = new ValidationFailure()
        {
            AttemptedValue = _createOrganisationManagerRequest.Organisation!.Abn,
            ErrorCode = "PredicateValidator",
            ErrorMessage = "ABN is invalid."
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

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
        _createOrganisationManagerRequest.Organisation!.Name = "Giant Eagles Rescue";

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateOrganisationManagerRequest.Organisation.Name), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Name_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createOrganisationManagerRequest.Organisation!.Name = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = "Organisation.Name",
            AttemptedValue = _createOrganisationManagerRequest.Organisation!.Name,
            ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals("Organisation.Name", StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    [Fact]
    public void Name_IsEmpty_ValidationFailures()
    {
        // Arrange
        _createOrganisationManagerRequest.Organisation!.Name = string.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = "Organisation.Name",
            AttemptedValue = _createOrganisationManagerRequest.Organisation!.Name,
            ErrorMessage = "'Name' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals("Organisation.Name", StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Theory]
    [MemberData(nameof(OrganisationType_ValidInput_NoValidationFailures_TestData))]
    public void OrganisationType_ValidInput_NoValidationFailures(OrganisationType organisationType)
    {
        // Arrange
        _createOrganisationManagerRequest.Organisation!.OrganisationType = organisationType;

        // Act
        var result = _createOrganisationManagerRequestValidator.Validate(_createOrganisationManagerRequest);

        // Assert
        result.Errors.Where(e => e.PropertyName.Equals("Organisation.OrganisationType", StringComparison.OrdinalIgnoreCase))
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
