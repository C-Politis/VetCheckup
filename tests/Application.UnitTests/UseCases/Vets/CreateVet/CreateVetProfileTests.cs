using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Vets.CreateVet;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Vets.CreateVet;

public class CreateVetProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void CreateVetProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateVetProfile>();
            cfg.AddProfile<EntityRequestProfile>();
        }).AssertConfigurationIsValid(); 

    #endregion

}
