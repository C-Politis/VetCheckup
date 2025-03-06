using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using VetCheckup.Application.UseCases.Pets.UpdatePet;
using VetCheckup.Domain.Enums;

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

        [Test]
        public void Name_ValidInput_NoValidationFailures()
        {
            _updatePetRequest.Name = "Valid Name";
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            _updatePetRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Name),
                AttemptedValue = _updatePetRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Name_IsEmpty_ValidationFailures()
        {
            _updatePetRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Name),
                AttemptedValue = _updatePetRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Species_ValidInput_NoValidationFailures()
        {
            _updatePetRequest.Species = "Valid Species";
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void Species_ExceedsMaxLength_ValidationFailures()
        {
            _updatePetRequest.Species = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Species),
                AttemptedValue = _updatePetRequest.Species,
                ErrorMessage = "The length of 'Species' must be 50 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void Species_IsEmpty_ValidationFailures()
        {
            _updatePetRequest.Species = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.Species),
                AttemptedValue = _updatePetRequest.Species,
                ErrorMessage = "'Species' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Species), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Test]
        public void OwnerId_ValidInput_NoValidationFailures()
        {
            _updatePetRequest.OwnerId = Guid.NewGuid();
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.OwnerId), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void OwnerId_IsEmpty_ValidationFailures()
        {
            _updatePetRequest.OwnerId = Guid.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.OwnerId),
                AttemptedValue = _updatePetRequest.OwnerId,
                ErrorMessage = "'Owner Id' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.OwnerId), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }


        [Test]
        public void DateOfBirth_ValidInput_NoValidationFailures()
        {
            _updatePetRequest.DateOfBirth = new DateTime(2010, 01, 01);
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.DateOfBirth), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void PetID_ValidInput_NoValidationFailures()
        {
            _updatePetRequest.PetId = Guid.NewGuid();
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.PetId), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Test]
        public void PetID_IsEmpty_ValidationFailures()
        {
            _updatePetRequest.PetId = Guid.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdatePetRequest.PetId),
                AttemptedValue = _updatePetRequest.PetId,
                ErrorMessage = "'Pet Id' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.PetId), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }


        [Test]
        public void Sex_ValidInput_NoValidationFailures()
        {
            var result = _updatePetRequestValidator.Validate(_updatePetRequest);
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdatePetRequest.Sex), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        #endregion
    }
}
