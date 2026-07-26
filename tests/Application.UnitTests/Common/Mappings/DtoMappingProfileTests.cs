using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using VetCheckup.Application.Common.Mappings;
using Xunit;

namespace VetCheckup.Application.UnitTests.Common.Mappings
{
    public class DtoMappingProfileTests
    {

        #region Profile Configuration Tests

        [Fact]
        public void DtoMappingProfile_ProfileConfigurationValidation()
        {
            var profile = new DtoMappingProfile();
            Assert.IsType<Profile>(profile);
        }

        #endregion

    }
}
