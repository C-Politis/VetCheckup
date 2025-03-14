using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidation;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;
using Xunit;
using FluentAssertions;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators
{
    public class UpdateContactRequestValidatorTests
    {

        #region Fields

        private readonly IValidator<UpdateContactRequest> _updateContactRequestValidator = new UpdateContactRequestValidator();
        private readonly UpdateContactRequest _updateContactRequest = new();

        #endregion

        #region Constructor Tests

        [Fact]
        public void Email_ValidInput_NoValidationFailures()
        {
            // Arrange
            _updateContactRequest.Email = "Valid@Email.Com.Au";

            // Act
            var result = _updateContactRequestValidator.Validate(_updateContactRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Email_IsNull_NoValidationFailures()
        {
            // Arrange
            _updateContactRequest.Email = null;

            // Act
            var result = _updateContactRequestValidator.Validate(_updateContactRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Email_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateContactRequest.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateContactRequest.Email),
                AttemptedValue = _updateContactRequest.Email,
                ErrorMessage = "The length of 'Email' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateContactRequestValidator.Validate(_updateContactRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateContactRequest.Email)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Mobile_ValidInput_NoValidationFailures()
        {
            // Arrange
            _updateContactRequest.Mobile = 0420123456;

            // Act
            var result = _updateContactRequestValidator.Validate(_updateContactRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Mobile_IsLessThanZero_NoValidationFailures()
        {
            // Arrange
            _updateContactRequest.Mobile = -1;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateContactRequest.Mobile),
                AttemptedValue = _updateContactRequest.Mobile,
                ErrorMessage = "'Mobile' must be greater than '0'.",
                ErrorCode = "GreaterThanValidator"
            };

            // Act
            var result = _updateContactRequestValidator.Validate(_updateContactRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }


        #endregion

    }
}
