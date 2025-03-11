using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Pets.UpdatePet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.UpdatePet
{
    public class UpdatePetProfileTest
    {
        #region Aragorns

        [Fact]
        public void UpdatePetProfile_ProfileConfigurationValidation()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdatePetProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            }).AssertConfigurationIsValid();

        #endregion

    }
}
