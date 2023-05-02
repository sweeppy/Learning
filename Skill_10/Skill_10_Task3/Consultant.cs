using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Newtonsoft.Json;

namespace BankProgram_ver3
{
    
    class Consultant : ChangeInfoOfClient
    {
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
                    GenerateRandomClients(consultant.clients, whoChanging);       //Заполнение списка рандомными клиентами
                    GetInfo(consultant.clients);    //Вывод всех клиентов в консоль
                    break;
                case 2:
                    GetInfo(consultant.clients);    //Вывод всех клиентов в консоль
                    break;
            }
            ChangeInfo(clients , whoChanging); //Изменеие информации отдельного клиента
        }
        /// <summary>
        /// Возвращает количество рандомных клиентов, которые в последствии будут созданы
        /// </summary>
        /// <returns></returns>
        public int SuccessOfRandomClients()
        {

            bool succes = int.TryParse(Console.ReadLine(), out var numOfRandClients);
            while (!succes || numOfRandClients <= 0)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                succes = int.TryParse(Console.ReadLine(), out numOfRandClients);
            }
            return numOfRandClients;
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

            SaveToJson(clients);//сохранение клиентов в Json
        }
        /// <summary>
        /// Вывод клиентов в консоль
        /// </summary>
        /// <param name="tempList"></param>
        public void GetInfo(List<Client> tempList)
        {
            Consultant consultant = new Consultant();
            if (File.Exists(consultant.path))
            {
                string jsonInf = File.ReadAllText(consultant.path);
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
            File.WriteAllText(consultant.path, jsonSer);
        }
        /// <summary>
        /// Возвращает лист с клиентами из файла Json, если он есть
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public List<Client> TakeInfoFromJson(List<Client> clients)
        {
            Consultant consultant = new Consultant();
            if (File.Exists(consultant.path))
            {
                string jsonInf = File.ReadAllText(consultant.path);
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonInf);
            }
            return clients;
        }
        
    }

}