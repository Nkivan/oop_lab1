using Lab1.App.Domain;
using Lab1.App.Validation;

namespace Lab1.App.Serialization
{
    public class TaxiDriverSerializer : PersonSerializerBase<TaxiDriver>
    {
        public override string TypeName => "TaxiDriver";

        public override FieldDefinition[] Fields { get; } =
        {
            FieldDefinition.FirstName(),
            FieldDefinition.LastName(),
            new FieldDefinition("driverlicense", "Посвідчення водія", Validator.DriverLicensePattern, "3 великі латинські літери + 6 цифр, напр. ABC123456"),
            FieldDefinition.Counter("trips", "Кількість поїздок"),
            FieldDefinition.Counter("studyhours", "Годин самонавчання"),
            FieldDefinition.Counter("dances", "Кількість танців")
        };

        protected override string[] GetValues(TaxiDriver t) => new[]
        {
            t.FirstName, t.LastName, t.DriverLicense,
            t.Trips.ToString(), t.StudyHours.ToString(), t.Dances.ToString()
        };

        public override Person Create(RecordData d) => new TaxiDriver(
            d.Get("firstname"), d.Get("lastname"), d.Get("driverlicense"),
            d.GetIntOrDefault("trips", 0), d.GetIntOrDefault("studyhours", 0), d.GetIntOrDefault("dances", 0));
    }
}
