using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Vets.CreateVet;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.CreateVet;

public class CreateVetRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateVetRequest> _createVetRequestValidator = new CreateVetRequestValidator();
    private readonly CreateVetRequest _createVetRequest = new()
    {
        Address = new()
        {
            Country = String.Empty,
            PostalCode = String.Empty,
            State = String.Empty,
            StreetAddress = String.Empty,
            Suburb = String.Empty
        },
        ContactDetails = new()
        {
            Email = String.Empty,
            Mobile = String.Empty
        },
        Title = Title.None,
        FirstName = "Test",
        MiddleName = "Middle",
        LastName = "Vet",
        Suffix = Suffix.None,
        DateOfBirth = DateTime.MinValue,
        OrganisationIds = new List<Guid>()
        {
            Guid.NewGuid(),
        },        
        PrimaryOrganisationId = Guid.Empty
    };

    #endregion

    #region Constructor Tests

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'First Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateFirstName(string firstName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createVetRequest.FirstName = firstName;

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", false, "'Last Name' must not be empty.")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Last Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateLastName(string lastName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createVetRequest.LastName = lastName;

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [InlineData("ValidName", true, "")]
    [InlineData("", true, "")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.")]
    public void ValidateMiddleName(string middleName, bool isValid, string expectedErrorMessage)
    {
        // Arrange
        _createVetRequest.MiddleName = middleName;

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        if (isValid)
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        else
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainSingle(e => e.ErrorMessage == expectedErrorMessage);
    }

    [Theory]
    [MemberData(nameof(Title_ValidInput_NoValidationFailures_TestData))]
    public void Title_ValidInput_NoValidationFailures(Title title)
    {
        // Arrange
        _createVetRequest.Title = title;

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.Title), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(Suffix_ValidInput_NoValidationFailures_TestData))]
    public void Suffix_ValidInput_NoValidationFailures(Suffix suffix)
    {
        // Arrange
        _createVetRequest.Suffix = suffix;

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.Suffix), StringComparison.OrdinalIgnoreCase))
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
