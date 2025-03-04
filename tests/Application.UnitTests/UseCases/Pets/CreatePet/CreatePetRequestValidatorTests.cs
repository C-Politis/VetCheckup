using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using VetCheckup.Domain.Enums;

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

        [Test]
        public void Name_ValidInput_NoValidationFailures()
        {
            _createPetRequest.Name = "Valid Name";
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            _createPetRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Name),
                AttemptedValue = _createPetRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Name_IsEmpty_ValidationFailures()
        {
            _createPetRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Name),
                AttemptedValue = _createPetRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Species_ValidInput_NoValidationFailures()
        {
            _createPetRequest.Species = "Valid Species";
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void Species_ExceedsMaxLength_ValidationFailures()
        {
            _createPetRequest.Species = new string('a', 51);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Species),
                AttemptedValue = _createPetRequest.Species,
                ErrorMessage = "The length of 'Species' must be 50 characters or fewer. You entered 51 characters.",
                ErrorCode = "MaximumLengthValidator"
            };
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Species_IsEmpty_ValidationFailures()
        {
            _createPetRequest.Species = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreatePetRequest.Species),
                AttemptedValue = _createPetRequest.Species,
                ErrorMessage = "'Species' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _createPetRequestValidator.Validate(_createPetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(CreatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        #endregion
    }

}
