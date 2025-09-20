using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerRequestValidatorTests
{
    #region Fields

    private readonly IValidator<UpdateOrganisationManagerRequest> _updateOrganisationManagerRequestValidator = new UpdateOrganisationManagerRequestValidator();
    private readonly UpdateOrganisationManagerRequest _updateOrganisationManagerRequest = new UpdateOrganisationManagerRequest() {
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
        DateOfBirth = DateTime.MinValue
    };

    #endregion
    
    #region Tests
    
    [Fact]
    public void OrganisationManagerId_ValidInput_NoValidationFailures()
    {
        // Arrange
        _updateOrganisationManagerRequest.OrganisationManagerId = Guid.NewGuid();

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(_updateOrganisationManagerRequest.OrganisationManagerId), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void OrganisationManagerId_Empty_ValidationFailures()
    {
        // Arrange
        _updateOrganisationManagerRequest.OrganisationManagerId = Guid.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(UpdateOrganisationManagerRequest.OrganisationManagerId),
            AttemptedValue = _updateOrganisationManagerRequest.OrganisationManagerId,
            ErrorMessage = "'Organisation Manager Id' must not be equal to '00000000-0000-0000-0000-000000000000'.",
            ErrorCode = "NotEqualValidator"
        };

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.OrganisationManagerId), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }
    
    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'First Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateFirstName(string firstName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _updateOrganisationManagerRequest.FirstName = firstName;

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'Last Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Last Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateLastName(string lastName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _updateOrganisationManagerRequest.LastName = lastName;

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", true, "")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateMiddleName(string middleName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _updateOrganisationManagerRequest.MiddleName = middleName;

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [MemberData(nameof(Title_ValidInput_NoValidationFailures_TestData))]
    public void Title_ValidInput_NoValidationFailures(Title title)
    {
        // Arrange
        _updateOrganisationManagerRequest.Title = title;

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.Title), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(Suffix_ValidInput_NoValidationFailures_TestData))]
    public void Suffix_ValidInput_NoValidationFailures(Suffix suffix)
    {
        // Arrange
        _updateOrganisationManagerRequest.Suffix = suffix;

        // Act
        var result = _updateOrganisationManagerRequestValidator.Validate(_updateOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationManagerRequest.Suffix), StringComparison.OrdinalIgnoreCase))
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
    
    #endregion
}
