using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Newtonsoft.Json;

namespace BankProgram_ver2
{
    class Consultant
    {
        string path = "clients_Task2.json";
        List<Client> clients = new List<Client>();
         public void Main()
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
                    TakeNumberOfClients(consultant.clients);
                    GetInfo(consultant.clients);
                    break;
                case 2:
                    GetInfo(consultant.clients);
                    break;
            }


        }
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
         public void TakeNumberOfClients(List<Client> clients)
        {

            Consultant consultant = new Consultant();

            Console.WriteLine("Введите количество рандомных клиентов для заполнения списка:");
            int temp = SuccessOfRandomClients();
            clients = TakeInfoFromJson(clients);
            for (int i = 0; i < temp; i++)
            {
                Client client = new Client();
                clients.Add(client);
            }

            SaveToJson(clients);
        }
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


        public void SaveToJson(List<Client> clients)
        {
            Consultant consultant = new Consultant();
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(clients);
            File.WriteAllText(consultant.path, jsonSer);
        }

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