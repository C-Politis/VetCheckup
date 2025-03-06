using AutoMapper;
using NUnit.Framework;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Pets.UpdatePet;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.UpdatePet
{
    public class UpdatePetProfileTest
    {
        #region Aragorns

        [Test]
        public void UpdatePetProfile_ProfileConfigurationValidation()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdatePetProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            }).AssertConfigurationIsValid();

        #endregion

    }
}
