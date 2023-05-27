using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Skill_11
{
    public class Client
    {
        public static string pathPhoneNumbers = "phoneNumbers.json";
        public static string pathPassports = "passports.json";

        public static List<string> names;
        public static List<string> phoneNumbers = new List<string>();
        public static Dictionary<int, int> passports = new Dictionary<int, int>();

        static Client()
        {
            names = new List<string>();
        }
        Random random = new Random();

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Ноер телефона
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Серия паспорта
        /// </summary>
        public int PassportSeries { get; set; }
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public int PassportID { get; set; }
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime DateOfChange { get; set; }
        /// <summary>
        /// Информация которую изменили
        /// </summary>
        public string InformationThatChanged { get; set; }
        /// <summary>
        /// Кто изменил информацию
        /// </summary>
        public string WhoChanged { get; set; }
        /// <summary>
        /// Тип изменения
        /// </summary>
        public string TypeOfChanging { get; set; }
        /// <summary>
        /// Привязка к департаменту
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// содание информации о клиенте
        /// </summary>
        /// <param name="LastName"></param>
        /// <param name="FirstName"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportID"></param>
        public Client(string LastName, string FirstName, string patronymic, string phoneNumber, int passportSeries, int passportID, int DepartmentID)
        {
            string jsonSer;

            if (File.Exists(pathPhoneNumbers))
            {
                jsonSer = File.ReadAllText(pathPhoneNumbers);
                phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer); //Если существует файл, дессериализация списка                                                                       //с номерами телефонов из Json
            }
            if (File.Exists(pathPassports))
            {
                jsonSer = File.ReadAllText(pathPassports);
                passports = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonSer); //Если существует файл, дессериализация библиотеки  
            }

            
            this.LastName = LastName;

            
            this.FirstName = FirstName;

            
            this.Patronymic = patronymic;


            this.PhoneNumber = phoneNumber;

            this.PassportSeries = passportSeries;
            this.PassportID = passportID;

            this.DateOfChange = DateTime.Now;
            this.InformationThatChanged = "Запись не изменялась";
            this.WhoChanged = "Менеджер";
            this.TypeOfChanging = "Создание записи";
            this.DepartmentID = DepartmentID;
        }


        /// <summary>
        /// Cоздание пользователя c рандомными параметрами
        /// </summary>
        /// <param name="LastName"></param>
        /// <param name="FirstName"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportID"></param>
        public Client(string whoChanging, int DepartmentIDб)
        {
            
            string jsonSer;
            
            if (File.Exists(pathPhoneNumbers))
            {
                jsonSer = File.ReadAllText(pathPhoneNumbers);
                phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer); //Если существует файл, дессериализация списка                                                                       //с номерами телефонов из Json
            }
            if (File.Exists(pathPassports))
            {
                jsonSer = File.ReadAllText(pathPassports);
                passports = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonSer); //Если существует файл, дессериализация библиотеки  
            }
            
            string tempPhoneNumber = GeneratePhoneNumber(phoneNumbers);
            
            passports  = GeneratePassportSeries(passports);

            jsonSer = JsonConvert.SerializeObject(phoneNumbers); //сохранение списка номеров телефона в Json
            File.WriteAllText(pathPhoneNumbers, jsonSer);

            this.LastName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.FirstName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.Patronymic = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.PhoneNumber = tempPhoneNumber;
            this.PassportSeries = passports.Keys.Last();
            this.PassportID = passports.Values.Last();
            this.DateOfChange = DateTime.Now;
            this.WhoChanged = whoChanging;
            this.InformationThatChanged = "Запись не изменялась";
            this.TypeOfChanging = "Создание записи";
            this.DepartmentID = DepartmentID;
        }
        /// <summary>
        /// Для Дессерализации
        /// </summary>
        public Client()
        {
            this.LastName = "";
            this.FirstName = "";
            this.Patronymic = "";
            this.PhoneNumber = "";
            this.PassportSeries = 0;
            this.PassportID = 0;
            this.DateOfChange = DateTime.Now;
            this.WhoChanged = "";
            this.InformationThatChanged = "";
            this.TypeOfChanging = "";
        }

        /// <summary>
        /// Создание рандомного номера телефона
        /// </summary>
        /// <returns></returns>
        private string GeneratePhoneNumber(List<string> phoneNumbers)
        {
            int tempInt;
            char[] nums = "123456789".ToCharArray();
            string tempPhoneNumber = "";
            for (int i = 0; i < 11; i++)
            {
                tempInt = random.Next(0, nums.Length - 1);
                tempPhoneNumber += nums[tempInt];
            }
            if (phoneNumbers.Count > 0)
            {
                if (!phoneNumbers.Contains(tempPhoneNumber))    //Добавление рандомного номера телефона в список, если его не существует                                               //генерация нового
                {
                    phoneNumbers.Add(tempPhoneNumber);
                }
                else tempPhoneNumber = GeneratePhoneNumber(phoneNumbers);
            }
            else
                phoneNumbers.Add(tempPhoneNumber);

            return tempPhoneNumber;
        }
        
        private Dictionary<int, int> GeneratePassportSeries(Dictionary<int, int> passports)
        {
            int randomSerie = random.Next(1000, 9999);
            int randompassportID = random.Next(100000, 999999);
            if (passports.Count > 0)
            {
                    if (!passports.Keys.Contains(randomSerie) && !passports.Values.Contains(randompassportID))
                    {
                        passports.Add(randomSerie, randompassportID);
                    }
                    else
                    {
                        GeneratePassportSeries(passports);
                    }
                
            }
            else passports.Add(randomSerie, randompassportID);

            string jsonSerPassports = JsonConvert.SerializeObject(passports);
            File.WriteAllText(pathPassports, jsonSerPassports);

            return passports;
        }
    }
}
