using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MainLogic
{
    public class Client
    {
        public static string pathPhoneNumbers = "phoneNumbers.json";
        public static string pathPassports = "passports.json";

        public static List<string> names;
        public static List<string> phoneNumbers = new List<string>();
        public static Dictionary<string, string> passports = new Dictionary<string, string>();
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
        public string PassportSeries { get; set; }
        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string PassportID { get; set; }
        /// <summary>
        /// Не депозитный счет
        /// </summary>
        public AccountA NonDepositMoney { get; set; }
        /// <summary>
        /// Депозитный счет
        /// </summary>
        public AccountB DepositMoney { get; set; }
        /// <summary>
        /// Тип счета(депозитный/не депозитный)
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime DateOfCreate { get; set; }
        /// <summary>
        /// Привязка к департаменту
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Создание клиента с заданными параметрами
        /// </summary>
        /// <param name="LastName">Фамилия клиента</param>
        /// <param name="FirstName">Имя клиента</param>
        /// <param name="patronymic">Отчество клиента</param>
        /// <param name="phoneNumber">Номер телефона клиента</param>
        /// <param name="passportSeries">Серия паспорта клиента</param>
        /// <param name="passportID">Номер паспорта клиента</param>
        /// <param name="money">Количество денег на счету клиента</param>
        /// <param name="account">Тип счета(депозитный/не депозитный)</param>
        public Client(string LastName,
                      string FirstName,
                      string patronymic,
                      string phoneNumber,
                      string passportSeries,
                      string passportID,
                      string account,
                      int DepartmentID)
        {

            Program.DeserializeBaseOfPassports();
            Program.DeserializeBaseOfPassports();

            this.LastName = LastName;

            this.FirstName = FirstName;

            this.Patronymic = patronymic;

            this.PhoneNumber = phoneNumber;

            this.PassportSeries = passportSeries;
            this.PassportID = passportID;

            this.Account = account;

            if (account == "Депозитный")
                this.DepositMoney = new AccountB();
            else
                this.NonDepositMoney = new AccountA();

            this.DateOfCreate = DateTime.Now;
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
            this.PassportSeries = "";
            this.PassportID = "";
            this.DateOfCreate = DateTime.Now;
        }

    }
}
