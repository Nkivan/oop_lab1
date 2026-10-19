using Lab1.App.Validation;

namespace Lab1.App.Domain
{
    public class TaxiDriver : Person, IStudier, IDriver, IDancer
    {
        public string DriverLicense { get; }
        public int Trips { get; private set; }
        public int StudyHours { get; private set; }
        public int Dances { get; private set; }

        public override string Id => DriverLicense;

        public TaxiDriver(string firstName, string lastName, string driverLicense,
                          int trips = 0, int studyHours = 0, int dances = 0)
            : base(firstName, lastName)
        {
            Validator.Require(Validator.DriverLicensePattern, driverLicense, "driverLicense");
            DriverLicense = driverLicense;
            Trips = trips;
            StudyHours = studyHours;
            Dances = dances;
        }

        public void Drive() => Trips++;

        /// <summary>Самонавчання.</summary>
        public void Study() => StudyHours++;

        public void Dance() => Dances++;
    }
}
