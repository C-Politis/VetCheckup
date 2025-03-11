using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.Mappings;

public class EntityRequestProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void EntityRequestProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg => cfg.AddProfile<EntityRequestProfile>()).AssertConfigurationIsValid();

    #endregion

}
