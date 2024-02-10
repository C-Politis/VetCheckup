using CanineCheckup.Models.Dog;
using CanineCheckup.ViewModels.Dog;

namespace CanineCheckup.ViewModels.Modal
{

    public class AddDogModalViewModel(DogOverviewViewModel dogOverviewViewModel)
    {

        #region Fields

        public DogOverviewViewModel m_DogOverviewViewModel = dogOverviewViewModel;

        #endregion

        #region Properties

        public DogOverviewViewModel DogOverviewViewModel
        {
            get => this.m_DogOverviewViewModel;
            set => this.m_DogOverviewViewModel = value;
        }

        #endregion

        #region Methods

        public void AddDog(DogModel newDog)
            => this.m_DogOverviewViewModel.DogViewModel.Dogs.Add(newDog);

        #endregion

    }

}
