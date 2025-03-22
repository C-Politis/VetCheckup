using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Application.UseCases.Pets.UpdatePet;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.UpdatePet
{

    public class UpdatePetRequestValidatorTest
    {
        #region Fields

        private readonly IValidator<UpdatePetRequest> _updatePetRequestValidator = new UpdatePetRequestValidator();
        private readonly UpdatePetRequest _updatePetRequest = new()
        {
            Name = string.Empty,
            PetId = Guid.Empty,
            Species = string.Empty,
            Sex = Sex.Male,
            OwnerId = Guid.Empty,
            DateOfBirth = DateTime.MinValue
        };

        #endregion

        #region Validator Tests

        [Theory]
        [InlineData("Valid Name")]
        [InlineData(null)]
        public void Name_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updatePetRequest.Name = name;

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updatePetRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Name),
                AttemptedValue = _updatePetRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Name_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updatePetRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Name),
                AttemptedValue = _updatePetRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData("Valid Species")]
        [InlineData(null)]
        public void Species_ValidInput_NoValidationFailures(string? species)
        {
            // Arrange
            _updatePetRequest.Species = species;

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Species_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updatePetRequest.Species = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Species),
                AttemptedValue = _updatePetRequest.Species,
                ErrorMessage = "The length of 'Species' must be 50 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Species_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updatePetRequest.Species = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Species),
                AttemptedValue = _updatePetRequest.Species,
                ErrorMessage = "'Species' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void OwnerId_ValidInput_NoValidationFailures()
        {
            // Arrange
            _updatePetRequest.OwnerId = Guid.NewGuid();

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.OwnerId), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void DateOfBirth_ValidInput_NoValidationFailures()
        {
            // Arrange
            _updatePetRequest.DateOfBirth = new DateTime(2010, 01, 01);

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.DateOfBirth), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void PetID_ValidInput_NoValidationFailures()
        {
            // Arrange
            _updatePetRequest.PetId = Guid.NewGuid();

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.PetId), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Theory]
        [MemberData(nameof(Sex_ValidInput_NoValidationFailures_TestData))]
        public void Sex_ValidInput_NoValidationFailures(Sex sex)
        {
            // Arrange
            _updatePetRequest.Sex = sex;

            // Act
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Sex), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        public static IEnumerable<object[]> Sex_ValidInput_NoValidationFailures_TestData()
            => new[]
            {
                new object[] { Sex.Male },
                new object[] { Sex.Female },
                new object[] { Sex.Other }
            };

        #endregion
    }
}
