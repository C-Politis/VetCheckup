namespace CanineCheckup.Models.Dog
{

    public class PhysicalActivity
    {

        #region Properties

        public Guid PhysicalActivityID { get; set; }

        public DateOnly? PhysicalActivityDate { get; set; }

        public TimeSpan Duration { get; set; }

        #endregion

    }

}
