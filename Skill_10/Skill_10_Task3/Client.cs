using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankProgram_ver3
{
    public class Client
    {
        string path = "phoneNumbers.json";
        static List<string> names;
        List<string> phoneNumbers = new List<string>();
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
        /// содание информации о клиенте
        /// </summary>
        /// <param name="LastName"></param>
        /// <param name="FirstName"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportID"></param>
        public Client(string LastName, string FirstName, string patronymic, string phoneNumber, int passportSeries, int passportID)
        {
            LastName = CheckOfEmpty(LastName);
            this.LastName = LastName;

            FirstName = CheckOfEmpty(FirstName);
            this.FirstName = FirstName;

            patronymic = CheckOfEmpty(patronymic);
            this.Patronymic = patronymic;

            this.PhoneNumber = phoneNumber;
            this.PassportSeries = passportSeries;
            this.PassportID = passportID;
            this.DateOfChange = DateTime.Now;
            this.TypeOfChanging = "Создание записи";
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
        public Client(string whoChanging)
        {
            bool successOfCreateNewNumber = false;
            string jsonSer;
            if (File.Exists(path))
            {
                jsonSer = File.ReadAllText(path);
                phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer); //Если существует файл, дессериализация списка
                                                                                     //с номерами телефонов из Json
            }
            string tempPhoneNumber = GeneratePhoneNumber();
            if (phoneNumbers.Count > 0)
            {
                while (!successOfCreateNewNumber)
                {
                    for (int i = 0; i < phoneNumbers.Count; i++)
                    {
                        if (!phoneNumbers.Contains(tempPhoneNumber))                    //Добавление рандомного номера телефона в список, если он существует,                                               //генерация нового
                        {
                            phoneNumbers.Add(tempPhoneNumber);
                            successOfCreateNewNumber = true;
                        }
                    }
                }
            }
            else
                phoneNumbers.Add(tempPhoneNumber);
            
            
            jsonSer = JsonConvert.SerializeObject(phoneNumbers); //сохранение списка номеров телефона в Json
            File.WriteAllText(path, jsonSer);

            this.LastName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.FirstName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.Patronymic = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.PhoneNumber = tempPhoneNumber;
            this.PassportSeries = random.Next(1000, 9999);
            this.PassportID = random.Next(100000, 999999);
            this.DateOfChange = DateTime.Now;
            this.WhoChanged = whoChanging;
            this.InformationThatChanged = "xd";
            this.TypeOfChanging = "Создание записи";
        }
        /// <summary>
        /// Для Дессерализации
        /// </summary>
        public Client()
        {
            this.LastName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.FirstName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.Patronymic = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.PhoneNumber = "00000000000";
            this.PassportSeries = random.Next(1000, 9999);
            this.PassportID = random.Next(100000, 999999);
            this.DateOfChange = DateTime.Now;
            this.WhoChanged = "Менеджер";
            this.InformationThatChanged = "xd";
            this.TypeOfChanging = "Создание записи";
        }
        /// <summary>
        /// Вывод информации о клиентах
        /// </summary>
        /// <param name="list">Лист клиентов, который нужно вывести</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string Print(List<Client> list, int index)
        {
            return $"{index})Фамилия: {LastName}" +
                $" Имя: {FirstName}" +
                $" Отчество: {Patronymic}" +
                $" Номер телефона: {PhoneNumber}" +
                $" Серия паспорта: ****" +
                $" Номер паспорта: ******" +
                $" Изменено: {WhoChanged}" +
                $" Тип изменения: {TypeOfChanging}" +
                $" Дата изменения:: {DateOfChange}";
        }
        /// <summary>
        /// Проверка на пустую строку, если она пустая, присваивание ей рандомное значение
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string CheckOfEmpty(string str)
        {
            if (str == string.Empty)
            {
                str = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            }
            return str;
        }
/// <summary>
/// Создание рандомного номера телефона
/// </summary>
/// <returns></returns>
        private string GeneratePhoneNumber()
        {
            int tempInt;
            char[] nums = "123456789".ToCharArray();
            string tempPhoneNumber = "";
            for (int i = 0; i < 11; i++)
            {
                tempInt = random.Next(0, nums.Length - 1);
                tempPhoneNumber += nums[tempInt];
            }
            return tempPhoneNumber;
        }

    }
}