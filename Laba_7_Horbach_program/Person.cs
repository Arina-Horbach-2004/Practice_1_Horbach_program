using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Laba_7_Horbach_program
{
    public class Person
    {
        private string? _name;
        private Gender _gender;
        private DateTime _birthDate;
        private string? _number;
        private static int Counter = 0;

        public Person()
        {
            Name = "Name";
            Gender = Gender.Male;
            BirthDate = GenerateRandomBirthDate();
            Number = "48538353801";
            Counter++;
        }

        public Person(string name, Gender gender) : this()
        {
            Name = name;
            Gender = gender;
        }

        public Person(string name, Gender gender, DateTime birthDate, string number)
        {
            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Number = number;
            Counter++;
        }

 
        public string? Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 3 && value.All(char.IsLetter))
                {
                    _name = value;
                }
                else
                {
                    _name = "Name";
                }
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value >= new DateTime(1958, 1, 1) && value <= new DateTime(2006, 12, 31))
                {
                    _birthDate = value;
                }
                else
                {
                    throw new ArgumentException("Некорректная дата рождения.");
                }
            }
        }

        private DateTime GenerateRandomBirthDate()
        {
            Random random = new Random();
            int year = random.Next(1958, 2007);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            return new DateTime(year, month, day);
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }

        
        public string? Number
        {
            get { return _number; }
            set
            {
                bool isValid = false;
                do
                {
                    if (!string.IsNullOrEmpty(value) && value.Length == 11 && value.All(char.IsDigit))
                    {
                        _number = value;
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Некоректний номер телефону. Повторіть спробу.");
                        value = Console.ReadLine();
                    }
                } while (!isValid);
            }
        }

        public string Country { get; set; } = "Ukraine";

        public void DisplayInfo()
        {
            Console.WriteLine($"Ім'я: {_name}");
            Console.WriteLine($"Стать: {Gender}");
            Console.WriteLine($"Дата народження: {_birthDate:dd.MM.yyyy}");
            Console.WriteLine($"Номер телефону: {_number}");
            Console.WriteLine($"Країна: {Country}");
            Console.WriteLine($"Вік: {Age} years old");
        }

        public void DisplayInfo(string additionalInfo)
        {
            Console.WriteLine($"Ім'я: {_name}");
            Console.WriteLine($"Стать: {Gender}");
            Console.WriteLine($"Дата народження: {_birthDate:dd.MM.yyyy}");
            Console.WriteLine($"Номер телефону: {_number}");
            Console.WriteLine($"Країна: {Country}");
            Console.WriteLine($"Вік: {Age} years old");
            Console.WriteLine($"Додаткова информация: {additionalInfo}");
        }
        public static double Average_age(List<Person> people)
        {
            if (people.Count == 0)
            {
                return 0;
            }
            int Average_Age = 0;
            foreach (var person in people)
            {
                Average_Age += person.Age;
            }
            return (double)Average_Age / people.Count;
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // Зберігання кількості створених об’єктів предметної області
        public static int Count
        {
            get { return Counter; }
        }


        public static Person Parse(string s)
        {
            string[] parts = s.Split(';');
            if (parts.Length != 4)
            {
                throw new FormatException("Invalid format for parsing.");
            }
            string name = parts[0];
            Gender gender = (Gender)Enum.Parse(typeof(Gender), parts[1]);
            DateTime birthDate = DateTime.ParseExact(parts[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            string number = parts[3];
            return new Person(name, gender, birthDate, number);
        }

        public static bool TryParse(string s, out Person? obj)
        {
            obj = null;
            try
            {
                obj = Parse(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            string genderString = Gender.ToString();
            return $"{_name},{genderString},{_birthDate:dd.MM.yyyy},{_number}";
        }


    }

}
