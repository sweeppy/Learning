using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver2
{
    public class Client
    {

        static List<string> names;
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
        }


        /// <summary>
        /// Рандомное создание пользователя
        /// </summary>
        /// <param name="LastName"></param>
        /// <param name="FirstName"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passportSeries"></param>
        /// <param name="passportID"></param>
        public Client()
        {
            this.LastName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.FirstName = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.Patronymic = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            this.PhoneNumber = "00000000000";
            this.PassportSeries = random.Next(1000, 9999);
            this.PassportID = random.Next(100000, 999999);
        }
        public string Print(List<Client> list, int index)
        {
            return $"{index})Фамилия: {LastName}" +
                $" Имя: {FirstName}" +
                $" Отчество: {Patronymic}" +
                $" Номер телефона: {PhoneNumber}" +
                $" Серия паспорта: ****" +
                $" Номер паспорта: ******";
        }

        public string CheckOfEmpty(string str)
        {
            if (str == string.Empty)
            {
                str = $"{Guid.NewGuid().ToString().Substring(0, 5)}";
            }
            return str;
        }

    }
}
