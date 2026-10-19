using Lab1.App.Validation;

namespace Lab1.App.Domain
{
    public class Acrobat : Person, IStudier, IDancer
    {
        public string CertificateId { get; }
        public int ExperienceYears { get; }
        public int StudyHours { get; private set; }
        public int Dances { get; private set; }

        public override string Id => CertificateId;

        public Acrobat(string firstName, string lastName, string certificateId,
                       int experienceYears, int studyHours = 0, int dances = 0)
            : base(firstName, lastName)
        {
            Validator.Require(Validator.CertificatePattern, certificateId, "certificateId");
            Validator.Require(Validator.ExperiencePattern, experienceYears.ToString(), "experienceYears");
            CertificateId = certificateId;
            ExperienceYears = experienceYears;
            StudyHours = studyHours;
            Dances = dances;
        }

        /// <summary>Самонавчання.</summary>
        public void Study() => StudyHours++;

        public void Dance() => Dances++;
    }
}
