using Ardalis.SmartEnum;

namespace CanineCheckup.Enums
{

    public class DogBiologicalSex(string name, int value) : SmartEnum<DogBiologicalSex>(name, value)
    {

        #region Enums

        public static readonly DogBiologicalSex Female = new(nameof(Female), 0);
        public static readonly DogBiologicalSex Male = new(nameof(Male), 1);

        #endregion

    }

}
