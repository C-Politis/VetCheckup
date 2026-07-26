using AutoMapper;
using VetCheckup.Application.UseCases.Owners.CreateOwner;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.CreateOwner;

public class CreateOwnerProfileTests
{

    #region Profile Configuration Tests

    [Fact]
    public void CreateOwnerProfile_ProfileConfigurationValidation()
    {
        var profile = new CreateOwnerProfile();
        Assert.IsType<Profile>(profile);
    }

    #endregion

}
