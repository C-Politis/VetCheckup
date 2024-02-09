using CanineCheckup.Models.Dog;

namespace CanineCheckup.ViewModels.Dog
{

    public class DogViewModel
    {

        #region Fields

        private ICollection<DogModel> m_Dogs;

        #endregion

        #region Constructors

        public DogViewModel()
            => this.OnInitialise();

        #endregion

        #region Properties

        public ICollection<DogModel> Dogs
        {
            get => this.m_Dogs;
            set => this.m_Dogs = value;
        }

        #endregion

        #region Methods

        public void OnInitialise()
        {
            this.Dogs = new List<DogModel>()
            {
                new()
                {
                    DogID = Guid.NewGuid(),
                    Breed = "Moodle",
                    DateOfBirth = new DateOnly(2013, 1, 31),
                    Name = "Gigi",
                    Sex = Enums.DogBiologicalSex.Female
                },
                new()
                {
                    DogID = Guid.NewGuid(),
                    Breed = "Moodle",
                    DateOfBirth = new DateOnly(2015, 6, 16),
                    Name = "Daisy",
                    Sex = Enums.DogBiologicalSex.Female
                },
                new()
                {
                    DogID = Guid.NewGuid(),
                    Breed = "Chorkie",
                    DateOfBirth = new DateOnly(2005, 6, 5),
                    Name = "Rubble",
                    Sex = Enums.DogBiologicalSex.Male
                }
            };
        }

        #endregion
    }

}
