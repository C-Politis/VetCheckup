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
        [InlineData("Valid Name")]
        [InlineData(null)]
        public void Name_ValidInput_NoValidationErrors(string? name)
        {
            // Arrange
            _updateVetRequest.Name = name;

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.Name),
                AttemptedValue = _updateVetRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Name_IsEmpty_ValidationFailures()
        {
            // Arrange
            _updateVetRequest.Name = string.Empty;
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateVetRequest.Name),
                AttemptedValue = _updateVetRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateVetRequestValidator.Validate(_updateVetRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        #endregion

    }
}
