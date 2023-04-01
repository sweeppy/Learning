
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Skill8
{
    public class Task1
    {
        static void Main()
        {
            List<int> listOfInteger = new List<int>();
            listOfInteger = FillingList(listOfInteger); //запонение листа рандомными числами от 1 до 100
            PrintListToConsole(listOfInteger); //вывод листа в консоль

            Console.ReadKey();
            Console.Clear();
            listOfInteger = DeleteNumberInList(listOfInteger); //удаление чисел из лмиста, которые больше 25, но меньше 50
            PrintListToConsole(listOfInteger); //вывод обработанного листа в консоль

            Console.ReadKey();
        }
        /// <summary>
        /// Заполнение листа целыми числами от 1 до 100
        /// </summary>
        /// <param name="list">лист, который нужно заполнить</param>
        static List<int> FillingList(List<int> list)
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                list.Add(random.Next(1, 101));
            }
            return list;
        }
        /// <summary>
        /// выводит в консоль лист
        /// </summary>
        /// <param name="list">лист, который нужно вывести</param>
        static void PrintListToConsole(List<int> list)
        {
            Console.WriteLine("Полученный лист:");
            int count = 1;
            foreach (var item in list)
            {
                Console.Write($"{count}){item} ");
                count++;
            }
        }
        /// <summary>
        /// Удаляет  из лста числа, которые болше 25, но меньше 50
        /// </summary>
        /// <param name="list">лист для операции</param>
        static List<int> DeleteNumberInList(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > 25 && list[i] < 50)
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }

    }
}

