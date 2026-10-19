using Lab1.App.Domain;
using Lab1.App.Validation;

namespace Lab1.App.Serialization
{
    public class StudentSerializer : PersonSerializerBase<Student>
    {
        public override string TypeName => "Student";

        public override FieldDefinition[] Fields { get; } =
        {
            FieldDefinition.FirstName(),
            FieldDefinition.LastName(),
            new FieldDefinition("course", "Курс", Validator.CoursePattern, "число від 1 до 6"),
            new FieldDefinition("studentid", "Студентський квиток", Validator.StudentIdPattern, "2 великі латинські літери + 6 цифр, напр. KB123456"),
            new FieldDefinition("arrivalcity", "Місто прибуття", Validator.CityPattern, "літери, 2-40 символів"),
            new FieldDefinition("passport", "Паспорт (серія і номер)", Validator.PassportPattern, "2 великі літери + 6 цифр, напр. AB123456"),
            FieldDefinition.Counter("studyhours", "Годин навчання"),
            FieldDefinition.Counter("dances", "Кількість танців")
        };

        protected override string[] GetValues(Student s) => new[]
        {
            s.FirstName, s.LastName, s.Course.ToString(), s.StudentId, s.ArrivalCity, s.Passport,
            s.StudyHours.ToString(), s.Dances.ToString()
        };

        public override Person Create(RecordData d) => new Student(
            d.Get("firstname"), d.Get("lastname"), d.GetInt("course"), d.Get("studentid"),
            d.Get("arrivalcity"), d.Get("passport"),
            d.GetIntOrDefault("studyhours", 0), d.GetIntOrDefault("dances", 0));
    }
}
