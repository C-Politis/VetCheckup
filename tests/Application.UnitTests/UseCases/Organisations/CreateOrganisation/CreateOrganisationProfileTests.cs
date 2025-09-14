using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.OrganisationManagers.CreateOrganisationManager;
using VetCheckup.Application.UseCases.Organisations.CreateOrganisation;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.CreateOrganisation;

public class CreateOrganisationProfileTests
{

    #region Profile Configuration Tests

    [Fact]
     public void CreateOrganisationProfile_ProfileConfigurationValidation()
         => new MapperConfiguration(cfg =>
         {
             cfg.AddProfile<CreateOrganisationProfile>();
             cfg.AddProfile<EntityRequestProfile>();
         }).AssertConfigurationIsValid();

    #endregion

}
