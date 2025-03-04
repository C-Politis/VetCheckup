using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using VetCheckup.Application.Common.Mappings;

namespace VetCheckup.Application.UnitTests.Common.Mappings
{
    public class DtoMappingProfileTests
    {

        #region Profile Configuration Tests

        [Test]
        public void DtoMappingProfile_ProfileConfigurationValidation()
            => new MapperConfiguration(cfg => cfg.AddProfile<DtoMappingProfile>()).AssertConfigurationIsValid();

        #endregion

    }
}
