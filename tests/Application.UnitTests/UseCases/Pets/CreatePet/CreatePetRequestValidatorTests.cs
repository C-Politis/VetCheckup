using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.CreatePet
{

    public class CreatePetRequestValidatorTests
    {
        #region Fields

        private readonly IValidator<CreatePetRequest> _createPetRequestValidator = new CreatePetRequestValidator();
        private readonly CreatePetRequest _createPetRequest = new()
        {
            Name = string.Empty,
            Species = string.Empty,
            Sex = Sex.Male,
            OwnerId = Guid.Empty,
            DateOfBirth = DateTime.MinValue
        };

        #endregion


        #region Validator Tests

        [Fact]
        public void Name_ValidInput_NoValidationFailures()
        {
            // Arrange
            _createPetRequest.Name = "Valid Name";

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _createPetRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Name),
                AttemptedValue = _createPetRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Name_IsEmpty_ValidationFailures()
        {
            // Arrange
            _createPetRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Name),
                AttemptedValue = _createPetRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Species_ValidInput_NoValidationFailures()
        {
            // Arrange
            _createPetRequest.Species = "Valid Species";

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Species_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _createPetRequest.Species = new string('a', 51);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Species),
                AttemptedValue = _createPetRequest.Species,
                ErrorMessage = "The length of 'Species' must be 50 characters or fewer. You entered 51 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Species_IsEmpty_ValidationFailures()
        {
            // Arrange
            _createPetRequest.Species = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Species),
                AttemptedValue = _createPetRequest.Species,
                ErrorMessage = "'Species' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _createPetRequestValidator.Validate(_createPetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        #endregion
    }

}
