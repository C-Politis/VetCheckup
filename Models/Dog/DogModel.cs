using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHealthTracker.Models.Dog
{

    public class DogModel
    {
        #region Properties

        public Guid DogID { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Name { get; set; }

        #endregion

    }

}
