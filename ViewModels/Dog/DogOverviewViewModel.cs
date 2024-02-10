using CanineCheckup.Models.Dog;

namespace CanineCheckup.ViewModels.Dog
{

    public class DogOverviewViewModel(DogViewModel dogViewModel)
    {

        #region Fields

        private DogModel m_DogModel;
        private DogViewModel m_DogViewModel = dogViewModel;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        public DogModel Dog
        {
            get => this.m_DogModel;
            set => this.m_DogModel = value;
        }

        public DogViewModel DogViewModel
        {
            get => this.m_DogViewModel;
            set => this.m_DogViewModel = value;
        }

        #endregion

        #region Methods

        public Task OnInitialised(Guid dogID)
        {
            this.Dog = this.m_DogViewModel.Dogs.FirstOrDefault(d => d.DogID == dogID);
            return Task.CompletedTask;
        }

        public void RemoveDog(Guid dogID)
            => this.DogViewModel.Dogs.Remove(this.DogViewModel.Dogs.FirstOrDefault(dog => dog.DogID == dogID));

        #endregion

    }

}
