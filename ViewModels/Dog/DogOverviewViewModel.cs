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

        public DogViewModel DogViewModel
        {
            get => this.m_DogViewModel;
            set => this.m_DogViewModel = value;
        }

        public DogModel Dog
        {
            get => this.m_DogModel;
            set => this.m_DogModel = value;
        }

        #endregion

        #region Methods

        public Task OnInitialised(Guid dogID)
        {
            this.Dog = this.m_DogViewModel.Dogs.FirstOrDefault(d => d.DogID == dogID);
            return Task.CompletedTask;
        }

        #endregion

    }

}
