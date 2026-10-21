using System;
using Lab1.App.Domain;

namespace Lab1.App.Services
{
    public class StudentStatistics
    {
        private readonly string _universityCity;

        public StudentStatistics(string universityCity)
        {
            _universityCity = universityCity ?? throw new ArgumentNullException(nameof(universityCity));
        }

        public string UniversityCity => _universityCity;

        public int CountFirstYear(Person[] people)
        {
            int count = 0;
            foreach (Person p in people)
                if (p is Student s && s.Course == 1) count++;
            return count;
        }

        public Student[] GetFirstYearFromOtherCities(Person[] people)
        {
            Student[] result = new Student[0];
            foreach (Person p in people)
                if (p is Student s && s.Course == 1 &&
                    !string.Equals(s.ArrivalCity, _universityCity, StringComparison.OrdinalIgnoreCase))
                    result = Arrays.Add(result, s);
            return result;
        }

        public double PercentFromOtherCities(Person[] people)
        {
            int total = CountFirstYear(people);
            if (total == 0) return 0;
            return GetFirstYearFromOtherCities(people).Length * 100.0 / total;
        }
    }
}
