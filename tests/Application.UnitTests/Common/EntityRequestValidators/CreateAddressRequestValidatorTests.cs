using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using VetCheckup.Application.Common.EntityRequests;
using VetCheckup.Application.Common.EntityRequestValidators;

namespace VetCheckup.Application.UnitTests.Common.EntityRequestValidators;

public class CreateAddressRequestValidatorTests
{
    #region Fields

    private readonly IValidator<CreateAddressRequest> _createAddressRequestValidator = new CreateAddressRequestValidator();
    private readonly CreateAddressRequest _createAddressRequest = new CreateAddressRequest();

    #endregion

    #region Constructor Tests

    [Test]
    public void Country_ValidInput_NoValidationFailures()
    {
        _createAddressRequest.Country = "This is a Valid Country";

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().BeEmpty();
    }

    [Test]
    public void Country_IsNull_NoValidationFailure()
    {
        _createAddressRequest.Country = null;

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().BeEmpty();
    }

    [Test]
    public void Country_ExceedsMaxLength_ValidationFailure()
    {
        _createAddressRequest.Country = new string('a', 101);

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.Country),
            AttemptedValue = _createAddressRequest.Country,
            ErrorMessage = "The length of 'Country' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Country)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void PostalCode_ValidInput_NoValidationFailures()
    {
        _createAddressRequest.PostalCode = "Valid PostalCode";

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().BeEmpty();
    }

    [Test]
    public void PostalCode_IsNull_NoValidationFailure()
    {
        _createAddressRequest.PostalCode = null;

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().BeEmpty();
    }

    [Test]
    public void PostalCode_ExceedsMaxLength_ValidationFailure()
    {
        _createAddressRequest.PostalCode = new string('a', 21); ;

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.PostalCode),
            AttemptedValue = _createAddressRequest.PostalCode,
            ErrorMessage = "The length of 'Postal Code' must be 20 characters or fewer. You entered 21 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.PostalCode)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void State_ValidInput_NoValidationFailures()
    {
        _createAddressRequest.State = "Valid State";

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().BeEmpty();
    }

    [Test]
    public void State_IsNull_NoValidationFailure()
    {
        _createAddressRequest.State = null;

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().BeEmpty();
    }

    [Test]
    public void State_ExceedsMaxLength_ValidationFailure()
    {
        _createAddressRequest.State = new string('a', 101); ;

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.State),
            AttemptedValue = _createAddressRequest.State,
            ErrorMessage = "The length of 'State' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.State)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void StreetAddress_ValidInput_NoValidationFailures()
    {
        _createAddressRequest.StreetAddress = "Valid StreetAddress";

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().BeEmpty();
    }

    [Test]
    public void StreetAddress_IsNull_NoValidationFailure()
    {
        _createAddressRequest.StreetAddress = null;

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().BeEmpty();
    }

    [Test]
    public void StreetAddress_ExceedsMaxLength_ValidationFailure()
    {
        _createAddressRequest.StreetAddress = new string('a', 251);

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.StreetAddress),
            AttemptedValue = _createAddressRequest.StreetAddress,
            ErrorMessage = "The length of 'Street Address' must be 250 characters or fewer. You entered 251 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.StreetAddress)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    [Test]
    public void Suburb_ValidInput_NoValidationFailures()
    {
        _createAddressRequest.StreetAddress = "Valid Suburb";

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().BeEmpty();
    }

    [Test]
    public void Suburb_IsNull_NoValidationFailure()
    {
        _createAddressRequest.Suburb = null;

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().BeEmpty();
    }

    [Test]
    public void Suburb_ExceedsMaxLength_ValidationFailure()
    {
        _createAddressRequest.Suburb = new string('a', 101);

        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(CreateAddressRequest.Suburb),
            AttemptedValue = _createAddressRequest.Suburb,
            ErrorMessage = "The length of 'Suburb' must be 100 characters or fewer. You entered 101 characters.",
            ErrorCode = "MaximumLengthValidator"
        };

        var result = _createAddressRequestValidator.Validate(_createAddressRequest);

        result.Errors.Where(e => e.PropertyName.Equals(nameof(CreateAddressRequest.Suburb)))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
