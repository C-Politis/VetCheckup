using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Pets.CreatePet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Pets.CreatePet
{
    public class CreatePetProfileTests
    {
        #region MyPrecious

        [Fact]
        public void CreatePetProfile_ProfileConfigurationValidation()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreatePetProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            }).AssertConfigurationIsValid();
        
        #endregion
    }
}
