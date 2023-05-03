using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankProgram_ver3
{
    interface ChangeInfo
    {
        /// <summary>
        /// Предлагает Удалить/Изменить/Редактировать запись об одном из клиенте
        /// </summary>
        /// <param name="whoChanging"></param>
        void WhatToDoWithList(string whoChanging);
        /// <summary>
        /// Изменение списка клиентов: Редактирование/Удаление/Добавление
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        void ChangeInfo(List<Client> clients, string whoChanging);
        /// <summary>
        /// Предлагает изменить некоторые параметры клиента из списка по номеру телефона клиента
        /// </summary>
        /// <param name="whoChanging"></param>
        void ChooseParamToChange(string whoChanging);
    }
    abstract class Employer : ChangeInfo
    {
        //public abstract void Main(string whoChanging);
        //public abstract int SuccessOfRandomClients();

        //public abstract void GenerateRandomClients();

        string path = "clients_Task3.json"; //Название файла для сохранения в Json
        List<Client> clients = new List<Client>();
        public void Main(string whoChanging)
        {

            Consultant consultant = new Consultant();

            Console.WriteLine("Хотите заполнить список рандомными клиентами?" +
                "\n 1)Да" +
                "\n 2)Нет");

            bool success = int.TryParse(Console.ReadLine(), out var number);
            while (!success || number < 1 || number > 2)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                success = int.TryParse(Console.ReadLine(), out number);
            }
            switch (number)
            {
                case 1:
                    GenerateRandomClients(clients, whoChanging);       //Заполнение списка рандомными клиентами
                    GetInfo(clients);    //Вывод всех клиентов в консоль
                    break;
                case 2:
                    GetInfo(clients);    //Вывод всех клиентов в консоль
                    break;
            }
            ChangeInfo(clients, whoChanging);
            //Изменеие информации отдельного клиента
        }


        /// <summary>
        /// Создание рандомных клиентов
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void GenerateRandomClients(List<Client> clients, string whoChanging)
        {

            Consultant consultant = new Consultant();

            Console.WriteLine("Введите количество рандомных клиентов для заполнения списка:");
            int temp = SuccessOfRandomClients();
            clients = TakeInfoFromJson(clients);
            string s = "";
            for (int i = 0; i < temp; i++)
            {
                Client client = new Client(whoChanging);
                clients.Add(client);
            }
            /// <summary>
            /// Возвращает количество рандомных клиентов, которые в последствии будут созданы
            /// </summary>
            /// <returns></returns>
            int SuccessOfRandomClients()
            {

                bool succes = int.TryParse(Console.ReadLine(), out var numOfRandClients);
                while (!succes || numOfRandClients <= 0)
                {
                    Console.WriteLine("Введено некорректное число, повторите попытку:");
                    succes = int.TryParse(Console.ReadLine(), out numOfRandClients);
                }
                return numOfRandClients;
            }


            SaveToJson(clients);//сохранение клиентов в Json
        }
        /// <summary>
        /// Вывод клиентов в консоль
        /// </summary>
        /// <param name="tempList"></param>
        public void GetInfo(List<Client> tempList)
        {
            Consultant consultant = new Consultant();
            if (File.Exists(path))
            {
                string jsonInf = File.ReadAllText(path);
                tempList = JsonConvert.DeserializeObject<List<Client>>(jsonInf);
                int index = 1;
                foreach (var item in tempList)
                {
                    Console.WriteLine(item.Print(tempList, index));
                    index++;
                }
            }
            else
                Console.WriteLine("В списке нет ни одной записи");
        }

        /// <summary>
        /// Метод сохранения списка клиентов в Json
        /// </summary>
        /// <param name="clients">Список клиентов, который нужно сохранить</param>
        public void SaveToJson(List<Client> clients)
        {
            Consultant consultant = new Consultant();
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(clients);
            File.WriteAllText(path, jsonSer);
        }
        /// <summary>
        /// Возвращает лист с клиентами из файла Json, если он есть
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public List<Client> TakeInfoFromJson(List<Client> clients)
        {
            Consultant consultant = new Consultant();
            if (File.Exists(path))
            {
                string jsonInf = File.ReadAllText(path);
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonInf);
            }
            return clients;
        }


        public void ChangeInfo(List<Client> clients, string whoChanging)
        {

            Console.WriteLine("Хотите внести изменения в списке?" +
                "\n 1)Да" +
                "\n 2)Нет");


            bool success = int.TryParse(Console.ReadLine(), out var number);
            while (!success || number < 1 || number > 2)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                success = int.TryParse(Console.ReadLine(), out number);
            }


            switch (number)
            {
                case 1:
                    WhatToDoWithList(whoChanging); //Удаление/Редактирование/Изменение записи о клиенте
                    break;
                case 2:     //заканчивает программу
                    break;
            }
        }
        public abstract void ChooseParamToChange(string whoChanging);

        public abstract void WhatToDoWithList(string whoChanging);

        /// <summary>
        /// Проверяет павильность введенного телефона по которому будет происходить поиск клиента
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string TakeInfOfClientToChange(string str)
        {
            Console.WriteLine($"Введите номер телефона клиента, {str} которого хотите изменить:");
            string strToReturn = Console.ReadLine();
            while (strToReturn.Length != 11)
            {
                Console.WriteLine("Введён некорректный номер телефона, повторите ввод:");
                strToReturn = Console.ReadLine();
            }
            return strToReturn;
        }
    }
}

