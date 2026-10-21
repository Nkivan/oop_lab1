using System;
using System.Text.RegularExpressions;

namespace Lab1.App.Validation
{
    public static class Validator
    {
        public const string NamePattern = @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'’\-]{2,30}$";
        public const string CityPattern = @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'’\- ]{2,40}$";
        public const string CoursePattern = @"^[1-6]$";
        public const string StudentIdPattern = @"^[A-Z]{2}\d{6}$";
        public const string PassportPattern = @"^[A-ZА-ЯІЇЄҐ]{2}\d{6}$";
        public const string DriverLicensePattern = @"^[A-Z]{3}\d{6}$";
        public const string CertificatePattern = @"^AC\d{5}$";
        public const string ExperiencePattern = @"^\d{1,2}$";
        public const string CounterPattern = @"^\d{1,9}$";

        public static bool IsValid(string pattern, string value)
            => value != null && Regex.IsMatch(value, pattern);

        public static void Require(string pattern, string value, string fieldName)
        {
            if (!IsValid(pattern, value))
                throw new ArgumentException($"Недопустиме значення '{value}' для поля '{fieldName}'.");
        }
    }
}
