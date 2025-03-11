using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.Vets.CreateVet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.CreateVet;

public class CreateVetRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateVetRequest> _createVetRequestValidator = new CreateVetRequestValidator();
    private readonly CreateVetRequest _createVetRequest = new()
    {
        Address = new(),
        ContactDetails = new(),
        Name = string.Empty,
        DateOfBirth = DateTime.MinValue
    };

    #endregion

    #region Constructor Tests

    [Fact]
    public void Name_ValidInput_NoValidationFailures()
    {
        // Arrange
        _createVetRequest.Name = "Valid Name";

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void Name_ExceedsMaxLength_ValidationFailures()
    {
        // Arrange
        _createVetRequest.Name = new string('a', 101);
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateVetRequest.Name),
            AttemptedValue = _createVetRequest.Name,
            ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    [Fact]
    public void Name_IsEmpty_ValidationFailures()
    {
        // Arrange
        _createVetRequest.Name = string.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateVetRequest.Name),
            AttemptedValue = _createVetRequest.Name,
            ErrorMessage = "'Name' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };

        // Act
        var result = _createVetRequestValidator.Validate(_createVetRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(CreateVetRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }


    #endregion

}
