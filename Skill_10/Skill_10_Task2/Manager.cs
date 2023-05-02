using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver2
{
    class Manager : Consultant
    {
        public void MainLogic()
        {
            Main();
            Console.WriteLine("Хотите добавть клиента?" +
                "\n 1)Да" +
                "\n 2)Нет");
            bool success = int.TryParse(Console.ReadLine(), out var number);
            while (!success || number < 1 || number > 2)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                success = int.TryParse(Console.ReadLine(), out number);
            }
            List<Client> clients = new List<Client>();
            switch (number)
            {
                case 1:
                    clients = TakeInfoFromJson(clients);
                    clients.Add(CreateOfNewClient());
                    SaveToJson(clients);
                    GetInfo(clients);
                    break;
                case 2:
                    
                    GetInfo(clients);
                    break;
            }
        }
        public Client CreateOfNewClient()
        {
            Console.WriteLine("Введите фамилию клиента:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Введите имя клиента:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите отчество клиента:");
            string patronymic = Console.ReadLine();

            Console.WriteLine("Введите номер телефона клиента:");
            string phoneNumber = Console.ReadLine();
            while (phoneNumber.Length != 11)
            {
                Console.WriteLine("Введен некорректный номер, повтрите ввод:");
                phoneNumber = Console.ReadLine();
            }

            Console.WriteLine("Введите номер серии паспорта:");
            bool successOfPassportSeries = int.TryParse(Console.ReadLine(), out var passportSeries);
            while (!successOfPassportSeries || passportSeries < 1000 || passportSeries > 9999)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                successOfPassportSeries=int.TryParse(Console.ReadLine(),out passportSeries);
            }

            Console.WriteLine("Введите номер паспорта:");
            bool successOfPassportId = int.TryParse(Console.ReadLine(), out int passportId);
            while (!successOfPassportId || passportId < 100000 || passportId > 999999)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                successOfPassportId = int.TryParse(Console.ReadLine(), out passportId);
            }



            Client client = new Client(lastName, firstName, patronymic, phoneNumber, passportSeries, passportId);
            return client;


        }
    }
}
