using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.CreateOrganisationManager;

public class CreateOrganisationManagerProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void CreateOrganisationManagerProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateOrganisationManagerProfile>();
            cfg.AddProfile<EntityRequestProfile>();
        }).AssertConfigurationIsValid();

    #endregion

}
