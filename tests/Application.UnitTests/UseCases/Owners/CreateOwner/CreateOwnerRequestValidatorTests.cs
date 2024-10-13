using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using VetCheckup.Application.UseCases.Owners.CreateOwner;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.CreateOwner;

public class CreateOwnerRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateOwnerRequest> _createOwnerRequestValidator = new CreateOwnerRequestValidator();
    private readonly CreateOwnerRequest _createOwnerRequest = new()
    {
        Address = new(),
        ContactDetails = new(),
        Name = string.Empty,
        DateOfBirth = DateTime.MinValue
    };

    #endregion

    #region Constructor Tests

    [Test]
    public void Name_ValidInput_NoValidationFailures()
    {
        _createOwnerRequest.Name = "Valid Name";

        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void Name_ExceedsMaxLength_ValidationFailures()
    {
        _createOwnerRequest.Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOwnerRequest.Name),
            AttemptedValue = _createOwnerRequest.Name,
            ErrorMessage = "The length of 'Name' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void Name_IsEmpty_ValidationFailures()
    {
        _createOwnerRequest.Name = string.Empty;

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateOwnerRequest.Name),
            AttemptedValue = _createOwnerRequest.Name,
            ErrorMessage = "'Name' must not be empty.",
            ErrorCode = "NotEmptyValidator"
        };

        var result = _createOwnerRequestValidator.Validate(_createOwnerRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateOwnerRequest.Name), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
