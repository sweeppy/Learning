using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Solution
{
    }
    /// <summary>
    /// Записывает данные переданные пользователем в файл
    /// </summary>
    static void WritingInfo() 
    {
        string [] lines = File.ReadAllLines(@"e:\workersFile.txt");
        int count = 1; /*ID клиента*/
        if (lines.Length == 0)
            count = 1;
        else
            count = lines.Length + 1;
        string[] information = new string[7];
        Console.WriteLine($"Введите своё Ф. И. О.:");
        string fullName = Console.ReadLine();
        Console.WriteLine($"Введите свой возраст:");
        bool successOfAge = int.TryParse(Console.ReadLine(), out var age);
        while (successOfAge == false || age > 80 || age < 16)
        {
            Console.WriteLine($"Введено некоректное число, повторите ввод:");
            successOfAge = int.TryParse(Console.ReadLine(), out age);
        }
        Console.WriteLine($"Введите свой рост");
        bool successOfHeight = int.TryParse(Console.ReadLine(), out var height);
        while (successOfAge == false || height > 230 || height < 130)
        {
            Console.WriteLine($"Введено некоректное число, повторите ввод:");
            successOfHeight = int.TryParse(Console.ReadLine(), out height);
        }
        Console.WriteLine($"Введите свою дату рождения: \n формат записи:##.##.####");
        string dateOfBirthday = Console.ReadLine();
        Console.WriteLine($"Введите место рождения:");
        string placeOfBirthday = Console.ReadLine();
        FileInfo fileInfo = new FileInfo(@"e:\workersFile.txt");

        information[0] = Convert.ToString(count);             /*ID клиента*/
        information[1] = fileInfo.LastWriteTime.ToString();   /*Время добавления информации*/
        information[2] = fullName;                            /*Ф.И.О. работника*/
        information[3] = age.ToString();                      /*Возраст работника*/
        information[4] = height.ToString();                   /*Рост работника*/
        information[5] = dateOfBirthday;                      /*Дата рождения работника*/
        information[6] = $"город {placeOfBirthday}";          /*Место рождения работника*/
        using (StreamWriter sw = new StreamWriter(@"e:\workersFile.txt", true))
        {
            foreach (var item in information)
            {
                sw.Write($"#{item}");
            }
            sw.WriteLine();
        }
        Console.WriteLine();
    }
    static void Main()
    {
        

        Console.Write($"Введите 1: для того чтобы просмортреть файл \n        2: для того чтобы заполнить данные и добавить новую запись в конец файла.");
        Console.WriteLine();
        bool success = int.TryParse(Console.ReadLine(), out var result);
        while (success == false || result > 2)
        {
            Console.WriteLine($"Введено некоректное число, повторите ввод:");
            success = int.TryParse(Console.ReadLine(), out result);
        }
        
        
            if(File.Exists(@"e:\workersFile.txt")) /*Проверка существования нужного файла*/
        {
                switch (result)
                {
                    case 1:

                        string consoleInf;
                        consoleInf = File.ReadAllText(@"e:\workersFile.txt"); /*Вывод в консоль данных с нужного файла*/
                        Console.WriteLine(consoleInf.Replace("#", " "));
                        break;

                    case 2:

                        WritingInfo();
                        
                        break;

                }
            }
            else
            {
            string fileName = @"e:\workersFile.txt";
            using (FileStream fs = File.Create(fileName));
            Console.WriteLine("Так как файла не было на вашем компьютере, он был успешно создан \n Путь к файлу: Е:/workersFile");
            Console.WriteLine();
            Main();
            }
            
        


        Console.ReadKey();
    }
    
}
