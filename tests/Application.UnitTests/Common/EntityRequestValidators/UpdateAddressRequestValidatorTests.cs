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
    public class UpdateAddressRequestValidatorTests
    {
        #region Fields

        private readonly IValidator<UpdateAddressRequest> _updateAddressRequestValidator = new UpdateAddressRequestValidator();
        private readonly UpdateAddressRequest _updateAddressRequest = new UpdateAddressRequest();

        #endregion

        #region Constructor Tests

        [Theory]
        [InlineData("This is a Valid Country")]
        [InlineData("")]
        [InlineData(null)]
        public void Country_ValidInput_NoValidationFailures(string? country)
        {
            // Arrange
            _updateAddressRequest.Country = country;

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.Country)))
                .Should().BeEmpty();
        }

        [Fact]
        public void Country_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateAddressRequest.Country = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateAddressRequest.Country),
                AttemptedValue = _updateAddressRequest.Country,
                ErrorMessage = "The length of 'Country' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.Country)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData("Valid PostalCode")]
        [InlineData("")]
        [InlineData(null)]
        public void PostalCode_ValidInput_NoValidationFailures(string? postalCode)
        {
            // Arrange
            _updateAddressRequest.PostalCode = postalCode;

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.PostalCode)))
                .Should().BeEmpty();
        }

        [Fact]
        public void PostalCode_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateAddressRequest.PostalCode = new string('a', 21);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateAddressRequest.PostalCode),
                AttemptedValue = _updateAddressRequest.PostalCode,
                ErrorMessage = "The length of 'Postal Code' must be 20 characters or fewer. You entered 21 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.PostalCode)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData("Valid State")]
        [InlineData("")]
        [InlineData(null)]
        public void State_ValidInput_NoValidationFailures(string? state)
        {
            // Arrange
            _updateAddressRequest.State = state;

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.State)))
                .Should().BeEmpty();
        }

        [Fact]
        public void State_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateAddressRequest.State = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateAddressRequest.State),
                AttemptedValue = _updateAddressRequest.State,
                ErrorMessage = "The length of 'State' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.State)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData("Valid StreetAddress")]
        [InlineData("")]
        [InlineData(null)]
        public void StreetAddress_ValidInput_NoValidationFailures(string? streetAddress)
        {
            // Arrange
            _updateAddressRequest.StreetAddress = streetAddress;

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.StreetAddress)))
                .Should().BeEmpty();
        }

        [Fact]
        public void StreetAddress_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateAddressRequest.StreetAddress = new string('a', 251);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateAddressRequest.StreetAddress),
                AttemptedValue = _updateAddressRequest.StreetAddress,
                ErrorMessage = "The length of 'Street Address' must be 250 characters or fewer. You entered 251 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.StreetAddress)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [InlineData("Valid Suburb")]
        [InlineData("")]
        [InlineData(null)]
        public void Suburb_ValidInput_NoValidationFailures(string? suburb)
        {
            // Arrange
            _updateAddressRequest.StreetAddress = suburb;

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.Suburb)))
                .Should().BeEmpty();
        }

        [Fact]
        public void Suburb_ExceedsMaxLength_ValidationFailure()
        {
            // Arrange
            _updateAddressRequest.Suburb = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateAddressRequest.Suburb),
                AttemptedValue = _updateAddressRequest.Suburb,
                ErrorMessage = "The length of 'Suburb' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateAddressRequestValidator.Validate(_updateAddressRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateAddressRequest.Suburb)))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        #endregion

    }
}
