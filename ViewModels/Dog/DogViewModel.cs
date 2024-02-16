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
                    MicrochipNumber = "A123456789",
                    Name = "Gigi",
                    PhysicalActivity = new List<PhysicalActivity>()
                    {
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 2),
                            Duration = new TimeSpan(3, 2, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = DateOnly.FromDateTime(DateTime.Now),
                            Duration = new TimeSpan(5, 6, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 1),
                            Duration = new TimeSpan(1, 2, 32)
                        }
                    },
                    Sex = Enums.DogBiologicalSex.Female
                },
                new()
                {
                    DogID = Guid.NewGuid(),
                    Breed = "Moodle",
                    DateOfBirth = new DateOnly(2015, 6, 16),
                    MicrochipNumber = "A124356789",
                    Name = "Daisy",
                    PhysicalActivity = new List<PhysicalActivity>()
                    {
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 2),
                            Duration = new TimeSpan(3, 2, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = DateOnly.FromDateTime(DateTime.Now),
                            Duration = new TimeSpan(5, 6, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 1),
                            Duration = new TimeSpan(1, 2, 32)
                        }
                    },
                    Sex = Enums.DogBiologicalSex.Female
                },
                new()
                {
                    DogID = Guid.NewGuid(),
                    Breed = "Chorkie",
                    DateOfBirth = new DateOnly(2005, 6, 5),
                    MicrochipNumber = "A123456879",
                    Name = "Rubble",
                    PhysicalActivity = new List<PhysicalActivity>()
                    {
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 2),
                            Duration = new TimeSpan(3, 2, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = DateOnly.FromDateTime(DateTime.Now),
                            Duration = new TimeSpan(5, 6, 22)
                        },
                        new()
                        {
                            PhysicalActivityID = Guid.NewGuid(),
                            PhysicalActivityDate = new DateOnly(2022, 1, 1),
                            Duration = new TimeSpan(1, 2, 32)
                        }
                    },
                    Sex = Enums.DogBiologicalSex.Male
                }
            };
        }

        #endregion
    }

}
