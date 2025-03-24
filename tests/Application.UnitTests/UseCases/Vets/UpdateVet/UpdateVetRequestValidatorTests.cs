using System.Xml.Linq;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Vets.UpdateVet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.UpdateVet
{
    public class UpdateVetRequestValidatorTests
    {

        #region Elleventy First Birthday

        private readonly IValidator<UpdateVetRequest> _updateVetRequestValidator = new UpdateVetRequestValidator();
        private readonly UpdateVetRequest _updateVetRequest = new()
        {
            VetId = Guid.Empty,
            Address = new(),
            ContactDetails = new(),
        };

        #endregion

        #region Validator Tests

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void FirstName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateVetRequest.FirstName = name;

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void FirstName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.FirstName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.FirstName),
                AttemptedValue = _updateVetRequest.FirstName,
                ErrorMessage = "The length of 'First Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void FirstName_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.FirstName = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.FirstName),
                AttemptedValue = _updateVetRequest.FirstName,
                ErrorMessage = "'First Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.FirstName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Valid Name")]
        public void MiddleName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateVetRequest.MiddleName = name;

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void MiddleName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.MiddleName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.MiddleName),
                AttemptedValue = _updateVetRequest.MiddleName,
                ErrorMessage = "The length of 'Middle Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.MiddleName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }


        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void LastName_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateVetRequest.LastName = name;

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void LastName_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.LastName = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.LastName),
                AttemptedValue = _updateVetRequest.LastName,
                ErrorMessage = "The length of 'Last Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void LastName_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.LastName = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.LastName),
                AttemptedValue = _updateVetRequest.MiddleName,
                ErrorMessage = "'Last Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.LastName), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        #endregion

    }
}
