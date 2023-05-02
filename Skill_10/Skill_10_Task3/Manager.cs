using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver3
{
    class Manager : Consultant
    {
        public void MainLogic(string whoChanging)
        {
            Main(whoChanging);
        }
        /// <summary>
        /// Метод создания нового клиента
        /// </summary>
        /// <returns>данные нового клиента</returns>
        public Client CreateOfNewClient()
        {
            Console.WriteLine("Введите фамилию клиента:"); //Ввод пользователем фамилии клиента
            string lastName = Console.ReadLine();

            Console.WriteLine("Введите имя клиента:");     //Ввод пользователем имени клиента
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите отчество клиента:"); //Ввод пользователем отчество клиента
            string patronymic = Console.ReadLine();

            Console.WriteLine("Введите номер телефона клиента:"); //Ввод пользователем фамилии клиента
            string phoneNumber = Console.ReadLine();
            while (phoneNumber.Length != 11)
            {
                Console.WriteLine("Введен некорректный номер, повтрите ввод:"); //Проверка на то, что номер из 11 цифр
                phoneNumber = Console.ReadLine();
            }

            Console.WriteLine("Введите номер серии паспорта:");
            bool successOfPassportSeries = int.TryParse(Console.ReadLine(), out var passportSeries); //Ввод пользователем серии паспорта клиента
            while (!successOfPassportSeries || passportSeries < 1000 || passportSeries > 9999)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                successOfPassportSeries = int.TryParse(Console.ReadLine(), out passportSeries); //проверка но то, что он состоит из 4 цифр
            }

            Console.WriteLine("Введите номер паспорта:");                                   //Ввод пользователем номера паспорта клиента
            bool successOfPassportId = int.TryParse(Console.ReadLine(), out int passportId); 
            while (!successOfPassportId || passportId < 100000 || passportId > 999999)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");        //Проверка на то, что он состоит из 6 цифр
                successOfPassportId = int.TryParse(Console.ReadLine(), out passportId);
            }



            Client client = new Client(lastName, firstName, patronymic, phoneNumber, passportSeries, passportId);
            return client;

            Console.WriteLine("Клиент был успешно добавлен!");
        }
    }
}
