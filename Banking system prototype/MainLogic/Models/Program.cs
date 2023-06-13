using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MainLogic
{
    public static class Program
    {

        static string pathClients = "clients.json";
        static string pathDepartments = "departments.json";


        /// <summary>
        /// Поиск клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона клиента</param>
        /// <param name="clientList">Лист клиентов</param>
        /// <returns></returns>
        public static Client FindClientByPhoneNumber(this List<Client> clientList, string phoneNumber)
        {
            Client client = null;
            foreach (var item in clientList)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    client = item;
                    break;
                }
                
            }
            return client;
        }

        /// <summary>
        /// Получает информацию о клиентах из json
        /// </summary>
        /// <returns>Лист клиентов из json</returns>
        public static List<Client> GetClientsInfoFromJson()
        {

            List<Client> tempList = new List<Client>();
            if (File.Exists(Program.pathClients))
            {
                string JsonClients = File.ReadAllText(pathClients);
                tempList = JsonConvert.DeserializeObject<List<Client>>(JsonClients);
            }
            return tempList;
        }
        /// <summary>
        /// Получает информацию о департаментах из json
        /// </summary>
        /// <returns>Лист департаментов из json</returns>
        public static void GetDepartmentsInfoFromJson(this List<Department> listOfDepartmnets)
        {

            List<Department> tempList = new List<Department>();
            if (File.Exists(Program.pathDepartments))
            {
                string JsonDepartments = File.ReadAllText(Program.pathDepartments);
                tempList = JsonConvert.DeserializeObject<List<Department>>(JsonDepartments);
            }
            listOfDepartmnets = tempList;
        }
        /// <summary>
        /// Сохраняет информацию о клиентах в json
        /// </summary>
        /// <param name="tempList"></param>
        public static void SaveClientsToJson(this List<Client> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(Program.pathClients, jsonSer);
        }
        /// <summary>
        /// Сохраняет информацию о департаментах в json
        /// </summary>
        /// <param name="tempList"></param>
        public static void SaveDepartmentsToJson(this List<Department> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(Program.pathDepartments, jsonSer);
        }
        public static void GetOperationsFromJson()
        {
            if (File.Exists(Operation.jsonOperations))
            {
                string jsonOperations = File.ReadAllText(Operation.jsonOperations);
                Operation.OperationList = JsonConvert.DeserializeObject<List<Operation>>(jsonOperations);
            }
        }
        /// <summary>
        /// Сохранение списка операций в json
        /// </summary>
        /// <param name="OperationList"></param>
        public static void SaveOperationsToJson(this List<Operation> OperationList)
        {
            string jsonSer;
            jsonSer=JsonConvert.SerializeObject(OperationList);
            File.WriteAllText(Operation.jsonOperations, jsonSer);
        }


        /// <summary>
        /// Проверка номера телефона для перевода
        /// </summary>
        /// <param name="phoneNumber">номер для перевода</param>
        /// <returns></returns>
        public static bool CheckPhoneNumber(this string phoneNumber)
        {
            DeserializeBaseOfPhoneNumber();
            if (phoneNumber.Length == 11 && Client.phoneNumbers.Contains(phoneNumber))
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Проверка на ввод суммы для перевода
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static bool CheckAmountOfMoney(this string money)
        {
            bool success = false;
            success = uint.TryParse(money, out uint _money);
            return success;
        }

        public static void DeserializeBaseOfPhoneNumber()
        {
            if (File.Exists(Client.pathPhoneNumbers))
            {
                string jsonSer = File.ReadAllText(Client.pathPhoneNumbers);
                Client.phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer); //Если существует файл, дессериализация списка                                                                       //с номерами телефонов из Json
            }
        }

        public static void DeserializeBaseOfPassports()
        {
            if (File.Exists(Client.pathPassports))
            {
                string jsonSer = File.ReadAllText(Client.pathPassports);
                Client.passports = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSer); //Если существует файл, дессериализация библиотеки  
            }
        }

    }
}
