using Lab1.App.Validation;

namespace Lab1.App.Domain
{
    public class Student : Person, IStudier, IDancer
    {
        public int Course { get; }
        public string StudentId { get; }
        public string ArrivalCity { get; }
        public string Passport { get; }
        public int StudyHours { get; private set; }
        public int Dances { get; private set; }

        public override string Id => StudentId;

        public Student(string firstName, string lastName, int course, string studentId,
                       string arrivalCity, string passport, int studyHours = 0, int dances = 0)
            : base(firstName, lastName)
        {
            Validator.Require(Validator.CoursePattern, course.ToString(), "course");
            Validator.Require(Validator.StudentIdPattern, studentId, "studentId");
            Validator.Require(Validator.CityPattern, arrivalCity, "arrivalCity");
            Validator.Require(Validator.PassportPattern, passport, "passport");
            Course = course;
            StudentId = studentId;
            ArrivalCity = arrivalCity;
            Passport = passport;
            StudyHours = studyHours;
            Dances = dances;
        }

        /// <summary>Навчання у ВНЗ.</summary>
        public void Study() => StudyHours++;

        public void Dance() => Dances++;
    }
}
