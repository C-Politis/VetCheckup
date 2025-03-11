using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.CreateOwner;

public class CreateOwnerProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void CreateOwnerProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateOwnerProfile>();
            cfg.AddProfile<EntityRequestProfile>();
        }).AssertConfigurationIsValid();

    #endregion

}
