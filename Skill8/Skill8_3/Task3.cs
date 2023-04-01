using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Skill8_3
{
    public class Task3
    {
        static void Main()
        {
            HashSet<int> numbers = new HashSet<int>();
            int tempnum;
            string strOfClearHash;

            if (File.Exists("numbers.json"))//Проверяет существование файла json
            {
                string inputjson = File.ReadAllText("numbers.json");//Если существует, то считывает информацию  с него и помещает 
                numbers = JsonConvert.DeserializeObject<HashSet<int>>(inputjson);
                Console.WriteLine("Желаете удалить все прошлые введенные числа?" +
                "\n да:" +
                "\n нет: -");
                strOfClearHash = Console.ReadLine();
                if (strOfClearHash != "+")
                {
                    if (strOfClearHash != "-")
                    {
                        Console.WriteLine("Введен некорректный символ, повторите ввод:");
                        SuccessEndOrContinue(Console.ReadLine());
                    }
                }                                                                               //отчищает колекцию если этого хочет пользователь
                else
                {
                    switch (strOfClearHash)
                    {
                        case "+":
                            numbers.Clear();
                            break;
                        case "-":
                            break;
                    }
                }
            }
           



            Console.WriteLine("Введите число:");
            tempnum = SuccessInput(Console.ReadLine());
            if (!numbers.Contains(tempnum))
            {
                numbers.Add(tempnum);
                Console.WriteLine("Число было успешно добавленно");
            }
            else
                Console.WriteLine("Число уже вводилось ранее, поэтому оно не было добавлено");

            string outputjson = JsonConvert.SerializeObject(numbers);
            File.WriteAllText("numbers.json", outputjson);
            Console.WriteLine("Хотите продолжить вводить числа" +
                "\n да: +" +
                "\n нет: -");
            SuccessEndOrContinue(Console.ReadLine());
        }

        /// <summary>
        /// Проверяет явлеются ли входные данные числами
        /// </summary>
        /// <param name="str">вводимое число</param>
        /// <returns></returns>
        static int SuccessInput(string str)
        {
            bool succ = int.TryParse(str, out var truenum);
            while (!succ )
            {
                Console.WriteLine("Введено некорректное число повторите ввод");
                succ = int.TryParse(Console.ReadLine(), out truenum);
            }
            return truenum;
        }
        /// <summary>
        /// Продолжает или заканчивает программу
        /// </summary>
        /// <param name="str">параметр для продолжения или конца программы, вводимый пользователем</param>
        static void SuccessEndOrContinue(string str)
        {

            if (str != "+")
            {
                if (str != "-")
                {
                    Console.WriteLine("Введен некорректный символ, повторите ввод:");
                    SuccessEndOrContinue(Console.ReadLine());
                }
            }
            else
            {
                  switch (str)
            {
                case "+":
                        Main();
                    break;
                case "-":
                    break;
            }  
            }
            
        }

    }
}