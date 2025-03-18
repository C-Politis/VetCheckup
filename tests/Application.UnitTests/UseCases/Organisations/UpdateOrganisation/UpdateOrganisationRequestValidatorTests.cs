using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;
using VetCheckup.Domain.Enums;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.UpdateOrganisation
{
    public class UpdateOrganisationRequestValidatorTests
    {

        #region second breakfast?

        private readonly IValidator<UpdateOrganisationRequest> _updateOrganisationRequestValidator = new UpdateOrganisationRequestValidator();
        private readonly UpdateOrganisationRequest _updateOrganisationRequest = new()
        {
            OrganisationId = Guid.NewGuid(),
            Abn = string.Empty,
            Address = new(),
            ContactDetails = new(),
            Name = string.Empty,
            OrganisationType = Domain.Enums.OrganisationType.Other
        };

        #endregion

        #region Handle Tests

        [Theory]
        [InlineData("51824753556")]
        [InlineData("")]
        [InlineData(null)]
        public void Abn_ValidInput_NoValidationFailures(string? abn)
        {
            // Arrange
            _updateOrganisationRequest.Abn = abn;

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationRequest.Abn), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Abn_NotANumber_ValidationFailures()
        {
            // Arrange
            _updateOrganisationRequest.Abn = new string('a', 11);
            var expectedFailure = new ValidationFailure()
            {
                AttemptedValue = _updateOrganisationRequest.Abn,
                ErrorCode = "RegularExpressionValidator",
                ErrorMessage = "ABN must be an 11 digit number"
            };

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
                .Excluding(e => e.FormattedMessagePlaceholderValues)
                .Excluding(e => e.PropertyName));
        }

        [Fact]
        public void Abn_NotElevenDigits_ValidationFailures()
        {
            // Arrange
            _updateOrganisationRequest.Abn = "12345678";
            var expectedFailure = new ValidationFailure()
            {
                AttemptedValue = _updateOrganisationRequest.Abn,
                ErrorCode = "RegularExpressionValidator",
                ErrorMessage = "ABN must be an 11 digit number"
            };

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
                .Excluding(e => e.FormattedMessagePlaceholderValues)
                .Excluding(e => e.PropertyName));
        }

        [Fact]
        public void Abn_InvalidAbn_ValidationFailures()
        {
            // Arrange
            _updateOrganisationRequest.Abn = "12345678910";
            var expectedFailure = new ValidationFailure()
            {
                AttemptedValue = _updateOrganisationRequest.Abn,
                ErrorCode = "PredicateValidator",
                ErrorMessage = "ABN is invalid"
            };

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg
                .Excluding(e => e.FormattedMessagePlaceholderValues)
                .Excluding(e => e.PropertyName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Valid Name")]
        public void Name_ValidInput_NoValidationFailures(string? name)
        {
            // Arrange
            _updateOrganisationRequest.Name = name;

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        [Fact]
        public void Name_ExceedsMaxLength_ValidationFailures()
        {
            // Arrange
            _updateOrganisationRequest.Name = new string('a', 101);
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateOrganisationRequest.Name),
                AttemptedValue = _updateOrganisationRequest.Name,
                ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
                ErrorCode = "MaximumLengthValidator"
            };

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Fact]
        public void Name_IsEmpty_ValidationFailures()
        {
            // Arrange
            var expectedFailure = new ValidationFailure()
            {
                PropertyName = nameof(UpdateOrganisationRequest.Name),
                AttemptedValue = _updateOrganisationRequest.Name,
                ErrorMessage = "'Name' must not be empty.",
                ErrorCode = "NotEmptyValidator"
            };

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors
                .Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationRequest.Name), StringComparison.OrdinalIgnoreCase))
                .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
        }

        [Theory]
        [MemberData(nameof(OrganisationType_ValidInput_NoValidationFailures_TestData))]
        public void OrganisationType_ValidInput_NoValidationFailures(OrganisationType organisationType)
        {
            // Arrange
            _updateOrganisationRequest.OrganisationType = organisationType;

            // Act
            var result = _updateOrganisationRequestValidator.Validate(_updateOrganisationRequest);

            // Assert
            result.Errors.Where(e => e.PropertyName.Equals(nameof(UpdateOrganisationRequest.OrganisationType), StringComparison.OrdinalIgnoreCase))
                .Should().BeEmpty();
        }

        public static IEnumerable<object[]> OrganisationType_ValidInput_NoValidationFailures_TestData()
        => new[]
        {
                new object[] { OrganisationType.Clinic },
                new object[] { OrganisationType.Daycare },
                new object[] { OrganisationType.Shelter },
                new object[] { OrganisationType.Rescues },
                new object[] { OrganisationType.Other },
        };

        #endregion

    }
}
