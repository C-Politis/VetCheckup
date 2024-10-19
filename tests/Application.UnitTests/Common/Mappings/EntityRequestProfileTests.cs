using AutoMapper;
using NUnit.Framework;
using VetCheckup.Application.Common.Mappings;

namespace VetCheckup.Application.UnitTests.Common.Mappings;

public class EntityRequestProfileTests
{

    #region Profile Configuration Tests

    [Test]
    public void EntityRequestProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg => cfg.AddProfile<EntityRequestProfile>()).AssertConfigurationIsValid();

    #endregion

}
