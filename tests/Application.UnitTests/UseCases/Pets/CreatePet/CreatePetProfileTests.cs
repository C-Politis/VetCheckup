using AutoMapper;
using NUnit.Framework;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using VetCheckup.Application.UseCases.Pets.CreatePet;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.CreatePet
{
    public class CreatePetProfileTests
    {
        #region MyPrecious

        [Test]
        public void CreatePetProfile_ProfileConfigurationValidation()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreatePetProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            }).AssertConfigurationIsValid();
        
        #endregion
    }
}
