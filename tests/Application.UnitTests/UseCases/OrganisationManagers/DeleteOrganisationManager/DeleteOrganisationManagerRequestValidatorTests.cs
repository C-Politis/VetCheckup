using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using VetCheckup.Application.UseCases.OrganisationManagers.DeleteOrganisationManager;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.DeleteOrganisationManager;

public class DeleteOrganisationManagerRequestValidatorTests
{

    #region Did you know he broke his foot when he kicked that helmet?

    private readonly IValidator<DeleteOrganisationManagerRequest> _deleteOrganisationRequestValidator = new DeleteOrganisationManagerRequestValidator();
    private readonly DeleteOrganisationManagerRequest _deleteOrganisationManagerRequest = new()
    {
        OrganisationManagerId = Guid.NewGuid()
    };

    #endregion

    #region Handle Tests

    [Fact]
    public void OrganisationManagerId_ValidInput_NoValidationFailures()
    {
        // Arrange
        _deleteOrganisationManagerRequest.OrganisationManagerId = Guid.NewGuid();

        // Act
        var result = _deleteOrganisationRequestValidator.Validate(_deleteOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(DeleteOrganisationManagerRequest.OrganisationManagerId), StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Fact]
    public void OrganisationManagerId_Empty_ValidationFailures()
    {
        // Arrange
        _deleteOrganisationManagerRequest.OrganisationManagerId = Guid.Empty;
        var expectedFailure = new ValidationFailure()
        {
            PropertyName = nameof(DeleteOrganisationManagerRequest.OrganisationManagerId),
            AttemptedValue = _deleteOrganisationManagerRequest.OrganisationManagerId,
            ErrorMessage = "'Organisation Manager Id' must not be equal to '00000000-0000-0000-0000-000000000000'.",
            ErrorCode = "NotEqualValidator"
        };

        // Act
        var result = _deleteOrganisationRequestValidator.Validate(_deleteOrganisationManagerRequest);

        // Assert
        result.Errors
            .Where(e => e.PropertyName.Equals(nameof(DeleteOrganisationManagerRequest.OrganisationManagerId), StringComparison.OrdinalIgnoreCase))
            .Should().ContainEquivalentOf(expectedFailure, cfg => cfg.Excluding(e => e.FormattedMessagePlaceholderValues));
    }

    #endregion

}
