using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.Common.Enums;
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
        FirstName = "Test",
        LastName = "Owner",
        MiddleName = "Middle",
        Suffix = Suffix.Dr,
        Title = Title.II,
        DateOfBirth = DateTime.MinValue
    };

    #endregion

    #region The Shire

            [Theory]
            [InlineData("ValidName", true, "")]
            [InlineData("", false, "'First Name' must not be empty.")]
            [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.")]
            public void ValidateFirstName(string firstName, bool isValid, string expectedErrorMessage)
            {
                // Arrange
                _createOwnerRequest.FirstName = firstName;
            
                // Act
                var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);
            
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
                _createOwnerRequest.LastName = lastName;
            
                // Act
                var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);
            
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
            [InlineData("", false, "'Middle Name' must not be empty.")]
            [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.")]
            public void ValidateMiddleName(string middleName, bool isValid, string expectedErrorMessage)
            {
                // Arrange
                _createOwnerRequest.MiddleName = middleName;
            
                // Act
                var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);
            
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
                _createOwnerRequest.Title = title;
            
                // Act
                var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);
            
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
                _createOwnerRequest.Suffix = suffix;
            
                // Act
                var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);
            
                // Assert
                result.Errors
                    .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Suffix), StringComparison.OrdinalIgnoreCase))
                    .Should().BeEmpty();
            }
            
            public static IEnumerable<object[]> Suffix_ValidInput_NoValidationFailures_TestData()
                =>
                [
                    [Suffix.Dr], 
                    [Suffix.Mr],
                    [Suffix.Mrs],
                    [Suffix.Ms],
                    [Suffix.Miss],
                    [Suffix.Prof],
                    [Suffix.Rev],
                    [Suffix.Hon],
                    [Suffix.Other]
                ];

            public static IEnumerable<object[]> Title_ValidInput_NoValidationFailures_TestData()
                =>
                [
                    [Title.II], 
                    [Title.III],
                    [Title.IV],
                    [Title.Jr],
                    [Title.Sr],
                    [Title.Esq],
                    [Title.Other]
                ];

            #endregion

}
