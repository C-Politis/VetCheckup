using FluentAssertions;
using FluentValidation;
using VetCheckup.Domain.Enums;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using Xunit;
using VetCheckup.Application.UseCases.Owners.CreateOwner;

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
        User = new()
        {
            Password = string.Empty,
            UserName = string.Empty,
            UserType = UserType.OrganisationManager
        },
        FirstName = "Test",
        LastName = "Owner",
        MiddleName = "Middle",
        Suffix = Suffix.Esq,
        Title = Title.Dr,
        DateOfBirth = DateTime.MinValue
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

    #endregion
}
