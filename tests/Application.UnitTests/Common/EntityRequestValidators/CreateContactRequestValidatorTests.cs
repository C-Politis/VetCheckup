using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators;

public class CreateContactRequestValidatorTests
{

    #region Fields

    private readonly IValidator<CreateContactRequest> _createContactRequestValidator = new CreateContactRequestValidator();
    private readonly CreateContactRequest _createContactRequest = new();

    #endregion

    #region Constructor Tests

    [Test]
    public void Email_ValidInput_NoValidationFailures()
    {
        _createContactRequest.Email = "Valid@Email.Com.Au";

        var result = _createContactRequestValidator.Validate(_createContactRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void Email_IsNull_NoValidationFailures()
    {
        _createContactRequest.Email = null;

        var result = _createContactRequestValidator.Validate(_createContactRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void Email_ExceedsMaxLength_ValidationFailure()
    {
        _createContactRequest.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateContactRequest.Email),
            AttemptedValue = _createContactRequest.Email,
            ErrorMessage = "The length of 'Email' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createContactRequestValidator.Validate(_createContactRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Email)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void Mobile_ValidInput_NoValidationFailures()
    {
        _createContactRequest.Mobile = 0420123456;

        var result = _createContactRequestValidator.Validate(_createContactRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void Mobile_IsLessThanZero_NoValidationFailures()
    {
        _createContactRequest.Mobile = -1;

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateContactRequest.Mobile),
            AttemptedValue = _createContactRequest.Mobile,
            ErrorMessage = "'Mobile' must be greater than '0'.",
            ErrorCode = "GreaterThanValidator"
        };

        var result = _createContactRequestValidator.Validate(_createContactRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateContactRequest.Mobile), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
