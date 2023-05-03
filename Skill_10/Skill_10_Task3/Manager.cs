using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver3
{
    class Manager : Employer
    {
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
        public override void WhatToDoWithList(string whoChanging)
        {
            Consultant consultant = new Consultant();
            List<Client> clients = new List<Client>();
            Manager manager = new Manager();

            clients = consultant.TakeInfoFromJson(clients);
            Console.WriteLine("Введите номер операции:" +
                "\n 1)Удаление пользователя" +
                "\n 2)Добавление пользователя" +
                "\n 3)Изменение данных пользователя");

            bool success = int.TryParse(Console.ReadLine(), out var result);
            while (!success || result < 1 || result > 3)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                success = int.TryParse(Console.ReadLine(), out result);
            }

            switch (result)//У консультанта нет доступа к некоторым пунктам
            {
                case 1: //Удаление записи
                    DeleteClient(clients);
                    consultant.SaveToJson(clients);
                    break;

                case 2: //Добавление записи

                    clients.Add(manager.CreateOfNewClient());
                    consultant.SaveToJson(clients);
                    break;

                case 3: //Редактирование записи
                    ChooseParamToChange(whoChanging);
                    break;
            }
        }

        /// <summary>
        /// Удаляет запись о клиенте по его номеру телефона
        /// </summary>
        /// <param name="clients"></param>
        public void DeleteClient(List<Client> clients)
        {
            Console.WriteLine("Введите номер телефона пользователя, которого хотите удалить");
            bool successOfRemove = false;
            string phoneNumber = Console.ReadLine();
            while (phoneNumber.Length != 11)
            {
                Console.WriteLine("Введён некорректный номер, повторите ввод:");
            }
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].PhoneNumber == phoneNumber)
                {
                    clients.RemoveAt(i);
                    successOfRemove = true;
                }

            }
            if (successOfRemove)
                Console.WriteLine("Клиент с данным номером был успешно удален!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
        /// <summary>
        /// Изменяет выбранный пункт о клиенте по его номеру телефона
        /// </summary>
        /// <param name="whoChanging"></param>
        public override void ChooseParamToChange(string whoChanging)
        {
            string typeOfChange = "Изменение информации";

            Consultant consultant = new Consultant();
            List<Client> clients = new List<Client>();

            clients = consultant.TakeInfoFromJson(clients);

            Console.WriteLine("Какаой параметр хотите изменить" +
                "\n 1)Фамилия" +
                "\n 2)Имя" +
                "\n 3)Отчество" +
                "\n 4)Номер телефона" +
                "\n 5)Серия паспорта" +
                "\n 6)Номер паспорта");
            bool success = int.TryParse(Console.ReadLine(), out var result);
            while (!success || result < 1 || result > 6)
            {
                Console.WriteLine("Введено некорректное число, повторите ввод:");
                success = int.TryParse(Console.ReadLine(), out result);
            }

            switch (result)
            {


                case 1:     //Изменение фамилии
                    ChangeLastName(clients, whoChanging);
                    break;


                case 2:     //Изменение имени
                    ChangeFirstName(clients, whoChanging);
                    break;


                case 3:     //Изменение отчества
                    ChangePatronymic(clients, whoChanging);
                    break;


                case 4:     //Изменение номера телфона
                    ChangePhoneNumber(clients, whoChanging);
                    break;


                case 5:     //Изменение серии паспорта
                    ChangePassportSeries(clients, whoChanging);
                    break;


                case 6:     //Изменение номера паспорта
                    ChangePassportID(clients, whoChanging);
                    break;
            }
            consultant.SaveToJson(clients); //Сохранение изменений в Json
            ChangeInfo(clients, whoChanging);   //Продолжить/Закончить изменения
        }
        /// <summary>
        /// Изменение фамилии клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangeLastName(List<Client> clients, string whoChanging)
        {
            string showForRequest = "фамилию";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;
            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новую фамилию:");
                    string tempLastName = Console.ReadLine();
                    item.LastName = tempLastName;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;

                }
            }
            if (successOfChanging)
                Console.WriteLine("Фамилия была успешно изменена!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");

        }
        /// <summary>
        /// Изменение имени клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangeFirstName(List<Client> clients, string whoChanging)
        {
            string showForRequest = "имя";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;
            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новое имя:");
                    string tempFirstName = Console.ReadLine();
                    item.FirstName = tempFirstName;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Имя было успешно изменено!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
        /// <summary>
        /// Изменение отчества клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangePatronymic(List<Client> clients, string whoChanging)
        {
            string showForRequest = "отчество";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;

            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новое отчество:");
                    string tempPatronymic = Console.ReadLine();
                    item.Patronymic = tempPatronymic;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Отчество было успешно изменено!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
        /// <summary>
        /// Изменение номера клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangePhoneNumber(List<Client> clients, string whoChanging)
        {
            string showForRequest = "номер телефона";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;

            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новый номер телефона:");
                    string tempPhoneNumber = Console.ReadLine();
                    item.PhoneNumber = tempPhoneNumber;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Номер телефона был успешно изменен!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
        /// <summary>
        /// Изменение сории паспорта клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangePassportSeries(List<Client> clients, string whoChanging)
        {
            string showForRequest = "серию паспорта";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;

            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новую серию паспорта:");
                    bool success = int.TryParse(Console.ReadLine(), out var tempPassportSeries);
                    while (!success || tempPassportSeries < 1000 || tempPassportSeries > 9999)
                    {
                        Console.WriteLine("Введено некорректное число, повторите попытку:");
                        success = int.TryParse(Console.ReadLine(), out tempPassportSeries);
                    }
                    item.PassportSeries = tempPassportSeries;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Серия пасспорта была успешно изменена!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
        /// <summary>
        /// Изменение номера паспорта клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangePassportID(List<Client> clients, string whoChanging)
        {
            string showForRequest = "серию паспорта";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;

            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новый номер паспорта:");
                    bool success = int.TryParse(Console.ReadLine(), out var tempPassportID);
                    while (!success || tempPassportID < 100000 || tempPassportID > 999999)
                    {
                        Console.WriteLine("Введено некорректное число, повторите попытку:");
                        success = int.TryParse(Console.ReadLine(), out tempPassportID);
                    }
                    item.PassportID = tempPassportID;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Номер паспорта был успешно изменен!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
    }
}
