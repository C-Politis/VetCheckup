using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Organisations.UpdateOrganisation;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Organisations.UpdateOrganisation
{
    public class UpdateOrganisationProfileTests
    {

        #region but what about

        [Fact]
        public void ConfigurationValidation_NoValidationFailures()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdateOrganisationProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            });

        #endregion

    }

}
