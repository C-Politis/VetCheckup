using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.Mappings;

public class EntityRequestProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void EntityRequestProfile_ProfileConfigurationValidation()
    {
        var profile = new EntityRequestProfile();
        Assert.IsType<Profile>(profile);
    }

    #endregion

}
