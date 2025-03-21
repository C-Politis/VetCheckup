using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VetCheckup.Application.Common.Mappings;
using VetCheckup.Application.UseCases.Owners.UpdateOwner;
using Xunit;

namespace VetCheckup.Application.UnitTests.UseCases.Owners.UpdateOwner
{
    public class UpdateOwnerProfileTests
    {

        #region Configuration Validation

        [Fact]
        public void ConfigurationValidation_NoValidationFailures()
            => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdateOwnerProfile>();
                cfg.AddProfile<EntityRequestProfile>();
            })
            .AssertConfigurationIsValid();

        #endregion

    }
}
