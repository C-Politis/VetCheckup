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

    #endregion

}
