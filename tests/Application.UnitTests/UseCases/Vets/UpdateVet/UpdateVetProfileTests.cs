using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Vets.UpdateVet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.UpdateVet
{
    public class UpdateVetProfileTests
    {

        #region Profile Tests

        [Fact]
        public void UpdateVetProfile_ProfileConfigurationValidation()
        {
            var profile = new UpdateVetProfile();
            Assert.IsType<Profile>(profile);
        }

        #endregion

    }
}
