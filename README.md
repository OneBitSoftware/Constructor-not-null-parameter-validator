**Usage example:**

```c#

    using ConstructorValidator;

    public class Person
    {
        private string email;

        private int age;

        private string firstName;

        private readonly string lastName;

        public Person(string email, int age, string firstName, string lastName)
        {
            this.email = email;
            this.age = age;
            this.firstName = firstName;
            this.lastName = lastName;

            Validator.ValidateNotNullConstructorFields(this);
            // or
            Validator.ValidateNotNullFields("www", "age", "firstName", "lastName");
        }
    }
```
