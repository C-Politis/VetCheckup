using CanineCheckup.Models.Dog;

namespace CanineCheckup.ViewModels.Modal
{

    public class PhysicalActivityModalViewModel
    {

        #region Fields

        private DogModel m_Dog;

        #endregion

        #region Properties

        public DogModel Dog
        {
            get => this.m_Dog;
            set => this.m_Dog = value;
        }

        #endregion

        #region Methods

        public double[] InitialiseData()
            => this.m_Dog.PhysicalActivity
                   .OrderBy(pa => pa.PhysicalActivityDate)
                   .Select(pa => pa.Duration.TotalMinutes)
                   .ToArray();

        public string[] InitialiseLabels()
            => this.m_Dog.PhysicalActivity
                   .OrderBy(pa => pa.PhysicalActivityDate)
                   .Select(pa => pa.PhysicalActivityDate.ToString())
                   .ToArray();

        #endregion

    }

}
