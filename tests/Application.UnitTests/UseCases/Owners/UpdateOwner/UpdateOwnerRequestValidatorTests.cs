using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.Common.Enums;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using VetCheckup.Application.UseCases.Owners.UpdateOwner;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerRequestValidatorTests
    {

        #region Fields

        private readonly IValidator<UpdateOwnerRequest> _updateOwnerRequestValidator = new UpdateOwnerRequestValidator();
        private readonly UpdateOwnerRequest _updateOwnerRequest = new()
        {
            OwnerId = Guid.Empty,
            Address = new(),
            ContactDetails = new(),
            FirstName = string.Empty,
            MiddleName = string.Empty,
            LastName = string.Empty,
            Title = Title.Miss,
            Suffix = Suffix.II,
            DateOfBirth = DateTime.MinValue
        };

        #endregion

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void FirstName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateOwnerRequest.FirstName = name;

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOwnerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void FirstName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.FirstName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.FirstName),
                AttemptedValue = _updateOwnerRequest.FirstName,
                ErrorMessage = "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void FirstName_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.FirstName = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.FirstName),
                AttemptedValue = _updateOwnerRequest.FirstName,
                ErrorMessage = "'First Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void MiddleName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateOwnerRequest.MiddleName = name;

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOwnerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void MiddleName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.MiddleName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.MiddleName),
                AttemptedValue = _updateOwnerRequest.MiddleName,
                ErrorMessage = "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void MiddleName_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.MiddleName = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.MiddleName),
                AttemptedValue = _updateOwnerRequest.MiddleName,
                ErrorMessage = "'Middle Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void LastName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateOwnerRequest.LastName = name;

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOwnerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void LastName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.LastName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.LastName),
                AttemptedValue = _updateOwnerRequest.LastName,
                ErrorMessage = "The length of 'Last Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void LastName_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.LastName = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.LastName),
                AttemptedValue = _updateOwnerRequest.MiddleName,
                ErrorMessage = "'Last Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

    }
}
