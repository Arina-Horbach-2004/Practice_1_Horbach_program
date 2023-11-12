using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Laba_7_Horbach_program;

namespace TestProject
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void Create_person_with_valid_name_and_gender_should_succeed()
        {
            // Arrange
            string name = "Lukas";
            Gender gender = Gender.Male;

            // Act
            Person person = new Person(name, gender);

            // Assert
            Assert.AreEqual(name, person.Name); // Перевіряємо, чи ім'я збережено правильно
            Assert.AreEqual(gender, person.Gender); // Перевіряємо, чи стать збережена правильно
        }

        [TestMethod]
        public void Create_person_with_invalid_name_should_set_default_name()
        {
            // Arrange
            string invalid_name = "N2"; // Некоректне ім'я
            Gender gender = Gender.Male;

            // Act
            Person person = new Person(invalid_name, gender); // Спроба створити особу з некоректним ім'ям

            // Assert
            Assert.AreEqual("Name", person.Name); // Перевіряємо, чи ім'я встановлено на значення за замовчуванням
        }

        [TestMethod]
        public void Calculate_age_should_return_correct_age()
        {
            // Arrange
            DateTime birth_date = new DateTime(1990, 5, 15);
            Person person = new Person("Mike", Gender.Female, birth_date, "48536232901");

            // Act
            int age = person.Age; // Обчислюємо вік

            // Assert
            Assert.AreEqual(33, age); // Перевіряємо, чи вік обчислений правильно (припускаємо, що поточна дата - 2023)
        }

        [TestMethod]
        public void Parse_valid_string_should_create_person_object()
        {
            // Arrange
            string personString = "Arina;female;24.11.2004;48538353801"; // Рядок для розбору

            // Act
            Person person = Person.Parse(personString); // Спроба розібрати рядок

            // Assert
            Assert.AreEqual("Arina", person.Name); // Перевіряємо, чи ім'я встановлено правильно
            Assert.AreEqual(Gender.female, person.Gender); // Перевіряємо, чи стать встановлена правильно
            Assert.AreEqual(new DateTime(2004, 11, 24), person.BirthDate); // Перевіряємо, чи дата народження встановлена правильно
            Assert.AreEqual("48538353801", person.Number); // Перевіряємо, чи номер встановлений правильно
        }

        [TestMethod]
        public void TryParse_valid_string_should_create_person_object()
        {
            // Arrange
            string person_string = "Seyran;Female;22.12.2003;48536353802"; // Рядок для розбору
            Person parsed_person;

            // Act
            bool result = Person.TryParse(person_string, out parsed_person); // Спроба розібрати рядок

            // Assert
            Assert.IsTrue(result, "TryParse should return true"); // Перевіряємо, чи TryParse повертає true
            Assert.IsNotNull(parsed_person, "Parsed Person should not be null"); // Перевіряємо, чи розібрана особа не є null
            Assert.AreEqual("Seyran", parsed_person.Name); // Перевіряємо, чи ім'я розібраної особи правильне
            Assert.AreEqual(Gender.Female, parsed_person.Gender); // Перевіряємо, чи стать розібраної особи правильна
            Assert.AreEqual(new DateTime(2003, 12, 22), parsed_person.BirthDate); // Перевіряємо, чи дата народження правильна
            Assert.AreEqual("48536353802", parsed_person.Number); // Перевіряємо, чи номер розібраної особи правильний
        }

        [TestMethod]
        public void TryParse_invalid_string_should_return_false()
        {
            // Arrange
            string invalid_person_string = "AfraMert"; // Рядок з некоректними даними
            Person parsed_person;

            // Act
            bool result = Person.TryParse(invalid_person_string, out parsed_person); // Спроба розібрати некоректний рядок

            // Assert
            Assert.IsFalse(result, "TryParse should return false for invalid input"); // Перевіряємо, чи TryParse повертає false для некоректних даних
            Assert.IsNull(parsed_person, "Parsed Person should be null for invalid input"); // Перевіряємо, чи розібрана особа є null для некоректних даних
        }

        [TestMethod]
        public void Parse_invalid_string_should_throw_format_exception()
        {
            // Arrange
            string invalidPersonString = "InvalidData"; // Рядок з некоректними даними

            // Act and Assert
            Assert.ThrowsException<FormatException>(() => Person.Parse(invalidPersonString), "Parse should throw FormatException for invalid input"); // Перевіряємо, чи метод Parse генерує виняток FormatException для некоректних даних
        }

        [TestMethod]
        public void Average_age_should_calculate_average_age()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person("Person1", Gender.Male, new DateTime(1997, 9, 1), "48536725401"),
                new Person("Person2", Gender.Female, new DateTime(1985, 3, 15), "48536999801"),
                new Person("Person3", Gender.Male, new DateTime(1995, 5, 20), "48532636802"),
            };

            // Act
            double average_age = Person.Average_age(people); // Обчислюємо середній вік

            // Assert
            Assert.AreEqual(30.66, average_age, 2); // Перевіряємо, чи середній вік обчислений правильно (з округленням)
        }
    }
}
