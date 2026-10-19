using Lab1.App.Validation;

namespace Lab1.App.Domain
{
    public abstract class Person : IIdentifiable
    {
        public string FirstName { get; }
        public string LastName { get; }

        /// <summary>Унікальний ідентифікатор конкретного типу особи.</summary>
        public abstract string Id { get; }

        protected Person(string firstName, string lastName)
        {
            Validator.Require(Validator.NamePattern, firstName, "firstname");
            Validator.Require(Validator.NamePattern, lastName, "lastname");
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
