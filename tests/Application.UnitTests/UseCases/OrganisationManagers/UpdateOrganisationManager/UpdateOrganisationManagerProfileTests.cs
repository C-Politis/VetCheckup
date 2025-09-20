using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.OrganisationManagers.UpdateOrganisationManager;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.OrganisationManagers.UpdateOrganisationManager;

public class UpdateOrganisationManagerProfileTests
{
    
    #region Deeds will not be less valiant because they are unpraised

    [Fact]
    public void UpdateOrganisationManagerProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UpdateOrganisationManagerProfile>();
            cfg.AddProfile<EntityRequestProfile>();
        }).AssertConfigurationIsValid();

    #endregion
    
}
