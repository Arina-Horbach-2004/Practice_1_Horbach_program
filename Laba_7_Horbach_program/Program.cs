using Laba_7_Horbach_program;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введіть максимальну кількість користувачів (N): ");
        if (int.TryParse(Console.ReadLine(), out int max_objects) && max_objects > 0)
        {
            Console.WriteLine($"Максимальна кількість користувачів: {max_objects}");

            List<Person> people = new();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1 - Додати об’єкт");
                Console.WriteLine("2 - Вивести на екран об’єкти");
                Console.WriteLine("3 - Знайти об’єкт");
                Console.WriteLine("4 - Видалити об’єкт");
                Console.WriteLine("5 - Демонстрація поведінки об’єктів");
                Console.WriteLine("6 - Демонстрація роботи static методів");
                Console.WriteLine("7 - зберегти колекцію об’єктів у файлі");
                Console.WriteLine("8 - зчитати колекцію об’єктів з файлу");
                Console.WriteLine("9 - очистити колекцію об’єктів");
                Console.WriteLine("0 - Вийти з програми");
                Console.Write("Виберіть опцію: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                Add_Person(people, max_objects);
                                break;

                            case 2:
                                Display_People(people);
                                break;

                            case 3:
                                Find_Person(people);
                                break;

                            case 4:
                                Remove_Person(people);
                                break;

                            case 5:
                                Demo_behavior(people);
                                break;

                            case 6:
                                DemoStaticMethods(people);
                                break;
                            case 7:
                                Console.WriteLine("1 – зберегти у файл *.csv (*.txt)");
                                Console.WriteLine("2 – зберегти у файл *.json");
                                int save = int.Parse(Console.ReadLine());
                                switch (save)
                                {
                                    case 1:

                                        string csvFilePath = @"C:\Users\Arina Gorbach\Desktop\Lab_7.csv"; 
                                        SaveToFIleCVS(people, csvFilePath);
                                        Console.WriteLine("Data loaded into a CSV file.");
                                        break;
                                    case 2:
                                        string jsonFilePath = @"C:\Users\Arina Gorbach\Desktop\Lab_7.json";
                                        SaveToFileJson(people, jsonFilePath);
                                        Console.WriteLine("Data loaded into JSON file.");
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний вибір способу зберігання.");
                                        break;
                                }
                                break;
                            case 8:
                                Console.WriteLine("1 – зчитати з файлу *.csv (*.txt)");
                                Console.WriteLine("2 – зчитати з файлу *.json");
                                int read = int.Parse(Console.ReadLine());
                                switch (read)
                                {
                                    case 1:
                                        string readCVSFile = @"C:\Users\Arina Gorbach\Desktop\Lab_7.csv";
                                        people = ReadFromFileCSV(readCVSFile); 
                                        Console.WriteLine("Data loaded from CSV file.");
                                        break;
                                    case 2:
                                        string readJsonFile = @"C:\Users\Arina Gorbach\Desktop\Lab_7.json";
                                        people = ReadFromFileJson(readJsonFile); 
                                        Console.WriteLine("Data loaded from JSON file.");
                                        break;
                                    default:
                                        Console.WriteLine("Некоректний вибір способу завантаження.");
                                        break;
                                }
                                break;
                            case 9:
                                people.Clear();
                                Console.WriteLine("Колекція очищена");
                                break;

                            case 0:
                                Console.WriteLine("Завершення роботи програми.");
                                return;

                            default:
                                Console.WriteLine("Некоректний вибір опції.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Некоректний вибір опції.");
                }
            }
        }
        else
        {
            Console.WriteLine("Некоректне значення для максимальної кількості користувачів.");
        }
    }

    public static void Add_Person(List<Person> people, int max_objects)
    {
        if (people.Count < max_objects)
        {
            Console.Write("1 - використання конструкторів, 2 - В форматі: Name,Gender,BirthDate,Number\n");
            int answer = int.Parse(Console.ReadLine());
            switch (answer)
            {
                case 1:
                    {
                        Console.WriteLine("Оберіть конструктор: 1 - без параметрів, 2 - заповнити ім'я та стать, 3 - всі параметри");
                        int constructor = int.Parse(Console.ReadLine());
                        switch (constructor)
                        {
                            case 1:
                                Person person = new Person();
                                people.Add(person);
                                Console.Write("Об'єкт додано");
                                break;
                            case 2:
                                Console.Write("Введіть ім'я: ");
                                string name = Console.ReadLine();
                                Console.Write("Введіть стать (Male/Female): ");
                                if (Enum.TryParse(typeof(Gender), Console.ReadLine(), true, out object genderobj))
                                {
                                    Gender gender = (Gender)genderobj;
                                    Person personWithGender = new Person(name, gender);
                                    people.Add(personWithGender);
                                    Console.Write("Об'єкт додано");
                                }
                                else
                                {
                                    Console.WriteLine("Некоректна стать.");
                                }
                                break;
                            case 3:
                                Console.Write("Введіть ім'я: ");
                                string _name = Console.ReadLine();
                                Gender _gender = Gender.Male;
                                Console.Write("Введіть стать (Male/Female): ");
                                if (Enum.TryParse(typeof(Gender), Console.ReadLine(), true, out object _parsedGenderObj))
                                {
                                    _gender = (Gender)_parsedGenderObj;
                                    Console.Write("Введіть дату народження (dd.MM.yyyy): ");
                                    if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
                                    {
                                        Console.Write("Введіть номер телефону: ");
                                        string number = Console.ReadLine();
                                        Person personWithManualData = new Person(_name, _gender, birthDate, number);
                                        people.Add(personWithManualData);
                                        Console.Write("Об'єкт додано");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Некоректна дата народження.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Некоректна стать.");
                                }
                                break;
                            default:
                                Console.WriteLine("Некоректний вибір конструктора.");
                                break;

                        }
                        break;
                    }
                case 2:
                    {
                        Console.Write("Введіть характеристики користувача в форматі: Name,Gender,BirthDate,Number:\n");
                        string inputString = Console.ReadLine();
                        if (Person.TryParse(inputString, out Person new_Person))
                        {
                            people.Add(new_Person);
                            Console.WriteLine("Об'єкт додано.");
                        }
                        else
                        {
                            Console.WriteLine("Некоректний формат вводу.");
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Некоректний вибір");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Досягнуто максимальну кількість об'єктів.");
        }
    }


    public static void Remove_Person(List<Person> people)
    {
        Console.WriteLine("Видалення об'єкта: ");

        Console.Write("Виберіть спосіб видалення (1 - за номером, 2 - за характеристикою): ");
        if (int.TryParse(Console.ReadLine(), out int deleteChoice))
        {
            switch (deleteChoice)
            {
                case 1:
                    Console.Write("Введіть номер об'єкта для видалення (1 - перший, 2 - другий і т.д.): ");
                    if (int.TryParse(Console.ReadLine(), out int deleteIndex) && deleteIndex > 0 && deleteIndex <= people.Count)
                    {
                        people.RemoveAt(deleteIndex - 1);
                        Console.WriteLine("Об'єкт видалено.");
                    }
                    else
                    {
                        Console.WriteLine("Некоректний номер об'єкта.");
                    }
                    break;
                case 2:
                    Console.Write("Введіть характеристику для видалення (Name/Gender/BirthDate/Number): ");
                    string deleteProperty = Console.ReadLine();

                    Console.Write($"Введіть значення для  {deleteProperty}: ");
                    string deleteValue = Console.ReadLine();

                    switch (deleteProperty)
                    {
                        case "Name":
                            people.RemoveAll(p => p.Name.Equals(deleteValue, StringComparison.OrdinalIgnoreCase));
                            break;
                        case "Gender":
                            if (Enum.TryParse(typeof(Gender), deleteValue, true, out object deleteGenderObj))
                            {
                                Gender deleteGender = (Gender)deleteGenderObj;
                                people.RemoveAll(p => p.Gender == deleteGender);
                            }
                            else
                            {
                                Console.WriteLine("Некоректна стать. ");
                            }
                            break;
                        case "BirthDate":
                            if (DateTime.TryParseExact(deleteValue, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime deleteBirthDate))
                            {
                                people.RemoveAll(p => p.BirthDate == deleteBirthDate);
                            }
                            else
                            {
                                Console.WriteLine("Некоректна дата народження.");
                            }
                            break;
                        case "Number":
                            people.RemoveAll(p => p.Number.Equals(deleteValue));
                            break;
                        default:
                            Console.WriteLine("Некоректна характеристика для видалення.");
                            break;
                    }

                    Console.WriteLine("Об'єкти видалено. ");
                    break;
                default:
                    Console.WriteLine("Некоректний вибір способу видалення. ");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Некоректний вибір способу видалення.");
        }
    }

    public static void Find_Person(List<Person> people)
    {
        Console.WriteLine("Пошук об'єкта:");
        Console.Write("Введіть характеристику для пошуку (Name/Gender/BirthDate/Number): ");
        string searchCharacteristic = Console.ReadLine();
        Console.Write($"Введіть значення для {searchCharacteristic}: ");
        string searchValue = Console.ReadLine();

        // Визначення умови пошуку
        Predicate<Person> condition = person =>
        {
            switch (searchCharacteristic)
            {
                case "Name":
                    return person.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase);
                case "Gender":
                    return person.Gender.ToString().Equals(searchValue, StringComparison.OrdinalIgnoreCase);
                case "BirthDate":
                    if (DateTime.TryParseExact(searchValue, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
                    {
                        return person.BirthDate == birthDate;
                    }
                    return false;
                case "Number":
                    return person.Number.Equals(searchValue, StringComparison.OrdinalIgnoreCase);
                default:
                    return false;
            }
        };

        Person foundPerson = people.Find(condition);

        if (foundPerson != null)
        {
            Console.WriteLine("Результат пошуку:");
            foundPerson.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Об'єкт, який відповідає умові пошуку, не знайдено.");
        }
    }

    public static void Display_People(List<Person> people)
    {
        if (people.Count == 0)
        {
            Console.WriteLine("Об'єктів ще не було додано.");
        }
        else
        {
            Console.WriteLine("Об'єкти:");
            for (int i = 0; i < people.Count; i++)
            {
                Console.WriteLine($"Об'єкт {i + 1}:");
                people[i].DisplayInfo();
                Console.WriteLine();
            }
        }
    }
    public static void Demo_behavior(List<Person> people)
    {
        Console.WriteLine("Демонстрація поведінки:");
        if (people.Count > 0)
        {
            Console.Write("Виберіть номер об'єкта для демонстрації: ");
            if (int.TryParse(Console.ReadLine(), out int demo_index) && demo_index > 0 && demo_index <= people.Count)
            {
                Console.WriteLine("Без параметрів:");
                people[demo_index - 1].DisplayInfo();
                Console.WriteLine("З параметром:");
                people[demo_index - 1].DisplayInfo($"Час реєстрації: {DateTime.Now:HH:mm:ss dd.MM.yyyy}");
            }
            else
            {
                Console.WriteLine("Некоректний номер об'єкта.");
            }
        }
        else
        {
            Console.WriteLine("Немає об'єктів для демонстрації.");
        }
    }

    public static void DemoStaticMethods(List<Person> people)
    {
        Console.WriteLine("Демонстрація роботи static методів:");
        if (people.Count > 0)
        {
            double average_age = Person.Average_age(people);
            Console.WriteLine($"Середній вік об'єктів: {average_age} років");
        }
        else
        {
            Console.WriteLine("Немає об'єктів для підрахунку середнього віку.");
        }
    }

    public  static void SaveToFIleCVS(List<Person> accounts, string path)
    {
        List<string> lines = new List<string>();
        foreach(var item in accounts)
        {
            lines.Add(item.ToString());
        }
        try
        {
            File.WriteAllLines(path, lines);
            Console.WriteLine($"Check out the CVS file.at: {Path.GetFullPath(path)}");
        }
        catch (IOException ex)
        {
            // Викидаємо IOException при помилці запису в файл.
            throw ex;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void SaveToFileJson(List<Person> accounts, string path)
    {
        try
        {
            string jsonstring = "";
            foreach (var item in accounts)
            {
                jsonstring += JsonSerializer.Serialize<Person>(item);
                jsonstring += "\n";
            }
            File.WriteAllText(path, jsonstring);
            Console.WriteLine($"Cheak out the JSON file at: {Path.GetFullPath(path)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static List<Person> ReadFromFileJson(string path)
    {
        List<Person> accounts = new List<Person>();
        try
        {
            List<string> lines = new List<String>();
            lines = File.ReadAllLines(path).ToList();
            Console.WriteLine("\nContents of JSON file:\n");
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
            foreach (var item in lines)
            {
                Person? account = JsonSerializer.Deserialize<Person>(item);
                if (account != null) accounts.Add(account);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Reading JSON file error:{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return accounts;
    }

    public static List<Person> ReadFromFileCSV(string path)
    {
        List<Person> accounts = new List<Person>();

        try
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(path).ToList();
            Console.WriteLine("\nContents of CSV file: \n");
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
            foreach (var item in lines)
            {
                Person? account;
                bool result = Person.TryParse(item, out account);
                if (result) accounts.Add(account);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Reading CSV file error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return accounts;
    }


}