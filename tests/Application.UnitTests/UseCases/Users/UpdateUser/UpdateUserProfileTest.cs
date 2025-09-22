using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Users.UpdateUser;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Users;

public class UpdateUserProfileTest
{
    
    #region One ring to rule them all

    [Fact]
    public void UpdateUserProfile_ProfileConfigurationValidation()
        => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UpdateUserProfile>();
        }).AssertConfigurationIsValid();

    #endregion
    
}
