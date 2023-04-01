using Newtonsoft.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Skill8_2
{

    public class Task2
    {
        string tempVariable = ""; //Переменная, которая, будет хранить  в себе либо новый телефон, который стоит добавить,
                                  //либо пустую строку, которая свидетельсвует о том, что пользователь закончил вводить номера
        public Dictionary<string, List<string>> baseOfPhoneNumbers = new Dictionary<string, List<string>>();//Сам словарь во которм храняться все данные
        static void Main()
        {
            Task2 task_2 = new Task2();
            task_2.GetFIOandPhoneNumber();  
        }
        /// <summary>
        /// Метод заполнения данных пользователем
        /// </summary>
         void GetFIOandPhoneNumber()
        {
            Console.WriteLine("Введите ваше Ф.И.О."); //Получение ФИО пользователя
            string FIO = Console.ReadLine();
            
            Console.WriteLine("Введите номер телефона:");//Получение номера пользователя
            if (File.Exists("baseOfPhonesNumbers.json"))//Проверка существования данных Json
            {
                string desjson = @"{""key1"":""value1"",""key2"":""value2""}";
                desjson = File.ReadAllText("baseOfPhonesNumbers.json");                 //Дессериализация данных, если они есть
                baseOfPhoneNumbers = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(desjson);
            }
            InputPhoneNumber(FIO, Console.ReadLine());
            
            

        }
        /// <summary>
        /// Синхронизирует данные (ФИО и телефона) и сохраняет их в Json
        /// </summary>
        /// <param name="FIO">Фио пользователя</param>
        /// <param name="phoneNumber">Номер телефона пользователя</param>
        void InputPhoneNumber(string FIO, string phoneNumber)
        {
            
            while (phoneNumber.Length != 11 || phoneNumber[0] == '0')//Проверка на то, что номер состоит из 11 цифр и не начинается с 0
            {
                Console.WriteLine("Введен некорректный номер телефона, повторите попытку:");
                phoneNumber = Console.ReadLine();
            }
            if (!baseOfPhoneNumbers.TryGetValue(FIO, out var templist))
            {
                templist = new List<string>();
                baseOfPhoneNumbers[FIO] = templist;                     //Создание временного листа в который будут помещены номера телефонов 
            }
            templist.Add(phoneNumber);
            
            
            string json;
            json = JsonConvert.SerializeObject(baseOfPhoneNumbers);     //Сохранение данных в Json
            


            Console.WriteLine("Если вы ввели все номера, которые хотели добавить нажмите Enter," +
                "или продолжайте ввод номеров");

            string tempVariable = Console.ReadLine();//Переменная, вводимая пользователем, которая будет свидетельствовать о том,
                                                     //что нужно либо добавить новый ноиер телефона, либо закончить ввод номеров телефона
            switch (tempVariable)
            {
                case "":
                    File.WriteAllText("baseOfPhonesNumbers.json", json);
                    Console.WriteLine("Введите номер телефона, по которому следует выполнить поиск:");//Ввод номеров телефона закончился.
                                                                                                      //Выполняем поиск пользователя по номеру телефона
                    FindHumanByPhoneNumber(Console.ReadLine());
                    break;
                default:
                    InputPhoneNumber(FIO, tempVariable);//Продолжаем ввод номеров телефона
                    break;
            }
        }
        /// <summary>
        /// Поиск пользователя по номеру телефона
        /// </summary>
        /// <param name="findNumber">Номер телефона, по которому будет выполнен поиск</param>
        void FindHumanByPhoneNumber(string findNumber)
        {
            bool successFinding = false;//Переменная, в которой хранится значение существования пользователя
                                        //True - существует
                                        //False - не существует
            string nameOfFindingHuman = ""; //Переменная для хранения ФИО пользователся, номер телефона которого ищем
            foreach (var e in baseOfPhoneNumbers)
            {
                foreach (List<string> t in baseOfPhoneNumbers.Values)
            {
                
                if (t.Contains(findNumber) && e.Value == t)
                {
                        nameOfFindingHuman = e.Key;
                        successFinding = true;
                }
            }
            }
 
            if (!successFinding) //проверяем найден ли пользлватель
            {
                Console.WriteLine("Пользователь с таким номером не найден, введите новый номер телефона для поиска" +
                                  " или нажмите Enter для заверщения программы");
            }
            else
            {
                Console.WriteLine($"Пользователь с таким номером найден: {nameOfFindingHuman}, введите новый номер телефона для поиска" +
                                  $" или нажмите Enter для заверщения программы");
            }
            tempVariable = Console.ReadLine();
            switch (tempVariable)
            {
                case "":

                    break;
                default:
                    FindHumanByPhoneNumber(tempVariable);
                    break;
            }



        }
    }
}