using Lab1.App.Domain;
using Lab1.App.Validation;

namespace Lab1.App.Serialization
{
    public class AcrobatSerializer : PersonSerializerBase<Acrobat>
    {
        public override string TypeName => "Acrobat";

        public override FieldDefinition[] Fields { get; } =
        {
            FieldDefinition.FirstName(),
            FieldDefinition.LastName(),
            new FieldDefinition("certificateid", "Сертифікат акробата", Validator.CertificatePattern, "AC + 5 цифр, напр. AC12345"),
            new FieldDefinition("experienceyears", "Стаж (років)", Validator.ExperiencePattern, "число від 0 до 99"),
            FieldDefinition.Counter("studyhours", "Годин самонавчання"),
            FieldDefinition.Counter("dances", "Кількість танців")
        };

        protected override string[] GetValues(Acrobat a) => new[]
        {
            a.FirstName, a.LastName, a.CertificateId,
            a.ExperienceYears.ToString(), a.StudyHours.ToString(), a.Dances.ToString()
        };

        public override Person Create(RecordData d) => new Acrobat(
            d.Get("firstname"), d.Get("lastname"), d.Get("certificateid"),
            d.GetInt("experienceyears"), d.GetIntOrDefault("studyhours", 0), d.GetIntOrDefault("dances", 0));
    }
}
