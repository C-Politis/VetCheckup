using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
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
            Name = string.Empty,
            DateOfBirth = DateTime.MinValue
        };

        #endregion

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void Name_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateOwnerRequest.Name = name;

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.Name),
                AttemptedValue = _updateOwnerRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Name_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateOwnerRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(CreateOwnerRequest.Name),
                AttemptedValue = _updateOwnerRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateOwnerRequestValidator.Validate(_updateOwnerRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }


    }
}
