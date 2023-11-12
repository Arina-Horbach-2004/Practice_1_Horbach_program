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
            Assert.AreEqual(name, person.Name); // ����������, �� ��'� ��������� ���������
            Assert.AreEqual(gender, person.Gender); // ����������, �� ����� ��������� ���������
        }

        [TestMethod]
        public void Create_person_with_invalid_name_should_set_default_name()
        {
            // Arrange
            string invalid_name = "N2"; // ���������� ��'�
            Gender gender = Gender.Male;

            // Act
            Person person = new Person(invalid_name, gender); // ������ �������� ����� � ����������� ��'��

            // Assert
            Assert.AreEqual("Name", person.Name); // ����������, �� ��'� ����������� �� �������� �� �������������
        }

        [TestMethod]
        public void Calculate_age_should_return_correct_age()
        {
            // Arrange
            DateTime birth_date = new DateTime(1990, 5, 15);
            Person person = new Person("Mike", Gender.Female, birth_date, "48536232901");

            // Act
            int age = person.Age; // ���������� ��

            // Assert
            Assert.AreEqual(33, age); // ����������, �� �� ���������� ��������� (����������, �� ������� ���� - 2023)
        }

        [TestMethod]
        public void Parse_valid_string_should_create_person_object()
        {
            // Arrange
            string personString = "Arina;female;24.11.2004;48538353801"; // ����� ��� �������

            // Act
            Person person = Person.Parse(personString); // ������ �������� �����

            // Assert
            Assert.AreEqual("Arina", person.Name); // ����������, �� ��'� ����������� ���������
            Assert.AreEqual(Gender.female, person.Gender); // ����������, �� ����� ����������� ���������
            Assert.AreEqual(new DateTime(2004, 11, 24), person.BirthDate); // ����������, �� ���� ���������� ����������� ���������
            Assert.AreEqual("48538353801", person.Number); // ����������, �� ����� ������������ ���������
        }

        [TestMethod]
        public void TryParse_valid_string_should_create_person_object()
        {
            // Arrange
            string person_string = "Seyran;Female;22.12.2003;48536353802"; // ����� ��� �������
            Person parsed_person;

            // Act
            bool result = Person.TryParse(person_string, out parsed_person); // ������ �������� �����

            // Assert
            Assert.IsTrue(result, "TryParse should return true"); // ����������, �� TryParse ������� true
            Assert.IsNotNull(parsed_person, "Parsed Person should not be null"); // ����������, �� �������� ����� �� � null
            Assert.AreEqual("Seyran", parsed_person.Name); // ����������, �� ��'� �������� ����� ���������
            Assert.AreEqual(Gender.Female, parsed_person.Gender); // ����������, �� ����� �������� ����� ���������
            Assert.AreEqual(new DateTime(2003, 12, 22), parsed_person.BirthDate); // ����������, �� ���� ���������� ���������
            Assert.AreEqual("48536353802", parsed_person.Number); // ����������, �� ����� �������� ����� ����������
        }

        [TestMethod]
        public void TryParse_invalid_string_should_return_false()
        {
            // Arrange
            string invalid_person_string = "AfraMert"; // ����� � ������������ ������
            Person parsed_person;

            // Act
            bool result = Person.TryParse(invalid_person_string, out parsed_person); // ������ �������� ����������� �����

            // Assert
            Assert.IsFalse(result, "TryParse should return false for invalid input"); // ����������, �� TryParse ������� false ��� ����������� �����
            Assert.IsNull(parsed_person, "Parsed Person should be null for invalid input"); // ����������, �� �������� ����� � null ��� ����������� �����
        }

        [TestMethod]
        public void Parse_invalid_string_should_throw_format_exception()
        {
            // Arrange
            string invalidPersonString = "InvalidData"; // ����� � ������������ ������

            // Act and Assert
            Assert.ThrowsException<FormatException>(() => Person.Parse(invalidPersonString), "Parse should throw FormatException for invalid input"); // ����������, �� ����� Parse ������ ������� FormatException ��� ����������� �����
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
            double average_age = Person.Average_age(people); // ���������� ������� ��

            // Assert
            Assert.AreEqual(30.66, average_age, 2); // ����������, �� ������� �� ���������� ��������� (� �����������)
        }
    }
}
