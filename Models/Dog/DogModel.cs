using CanineCheckup.Enums;

namespace CanineCheckup.Models.Dog
{

    public class DogModel
    {
        #region Properties

        public Guid DogID { get; set; }

        public string Breed { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Name { get; set; }

        public DogBiologicalSex Sex { get; set; }

        #endregion

    }

}
