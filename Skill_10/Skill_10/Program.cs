using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Newtonsoft.Json;

    namespace BankProgram
{
    class Program
    {
        string path = "clients.json";
        List<Client> clients = new List<Client>();
        static void Main()
        {
            
            Program program = new Program();

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
                case 1: TakeNumberOfClients(program.clients);
                    GetInfo(program.clients);
                    break;
                case 2:
                    GetInfo(program.clients);
                    break;
            }
            
            
        }
        static int SuccessOfRandomClients()
        {
            
            bool succes = int.TryParse(Console.ReadLine(), out var numOfRandClients);
            while(!succes || numOfRandClients <= 0)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                succes = int.TryParse(Console.ReadLine(), out numOfRandClients);
            }
            return numOfRandClients;
        }
        static void TakeNumberOfClients(List<Client> clients)
        {
            
            Program program = new Program();
            Console.WriteLine("Введите количество рандомных клиентов для заполненрия списка:");
            int temp = SuccessOfRandomClients();
            if (File.Exists(program.path))
            {
                string jsonInf = File.ReadAllText(program.path);
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonInf);
            }
            for (int i = 0; i < temp; i++)
            {
                Client client = new Client();
                clients.Add(client);
            }
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(clients);
            File.WriteAllText(program.path, jsonSer);
            
        }
        static void GetInfo(List<Client> tempList)
        {
            
            Program program = new Program();
            if (File.Exists(program.path))
            {
                string jsonInf = File.ReadAllText(program.path);
                tempList = JsonConvert.DeserializeObject<List<Client>>(jsonInf);
                foreach (var item in tempList)
                {
                    Console.WriteLine(item.Print());
                }
            }
            else
                Console.WriteLine("В списке нет ни одной записи");
        }

    }
    
}

