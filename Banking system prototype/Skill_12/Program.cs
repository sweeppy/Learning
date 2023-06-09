using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Skill_12
{
    public class Program
    {
        static string pathClients = "clients.json";
        static string pathDepartments = "departments.json";




        /// <summary>
        /// Формирование листа клиентов по DepartmentID в соответсвии с выбраннным департаментом
        /// </summary>
        /// <returns>Лист клиентов</returns>
        public static List<Client> FindByDepartmentID(List<Client> clientList, MainWindow mainWindow)
        {
            List<Client> tempList = new List<Client>();
            if (mainWindow.CbDepartments.SelectedItem != null)
            {
                foreach (var item in clientList)
                {
                    if ((mainWindow.CbDepartments.SelectedItem as Department).DepartamentID == item.DepartmentID)
                    {
                        tempList.Add(item);
                    }
                }
            }

            return tempList;
        }
        /// <summary>
        /// Поиск клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона клиента</param>
        /// <param name="clientList">Лист клиентов</param>
        /// <returns></returns>
        public static Client FindClientByPhoneNumber(string phoneNumber, List<Client> clientList)
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
        public static List<Department> GetDepartmentsInfoFromJson()
        {

            List<Department> tempList = new List<Department>();
            if (File.Exists(Program.pathDepartments))
            {
                string JsonDepartments = File.ReadAllText(Program.pathDepartments);
                tempList = JsonConvert.DeserializeObject<List<Department>>(JsonDepartments);
            }
            return tempList;
        }
        /// <summary>
        /// Сохраняет информацию о клиентах в json
        /// </summary>
        /// <param name="tempList"></param>
        public static void SaveClientsToJson(List<Client> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(Program.pathClients, jsonSer);
        }
        /// <summary>
        /// Сохраняет информацию о департаментах в json
        /// </summary>
        /// <param name="tempList"></param>
        public static void SaveDepartmentsToJson(List<Department> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(Program.pathDepartments, jsonSer);
        }

        /// <summary>
        /// Лоигка сортировки
        /// </summary>
        /// <param name="criterionstr"></param>
        public static List<Client> SortLogic(string criterionstr, List<Client> clientList, MainWindow mainWindow)
        {
            List<Client> nowclientList = new List<Client>();
            nowclientList = Program.FindByDepartmentID(clientList, mainWindow);
            switch (criterionstr)
            {
                case "FirstName":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.FirstName)); //сортировка по имени
                    break;
                case "LastName":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.LastName)); //сортировка по фамилии
                    break;
                case "Patronymic":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.Patronymic)); //сортировка по отчеству
                    break;
                case "PhoneNumber":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.PhoneNumber)); //сортировка по номеру телефона
                    break;
                case "PassportSeries":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.PassportSeries)); //сортировка по серии паспорта
                    break;
                case "PassportID":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.PassportID)); //сортировка по номеру паспорта
                    break;
                case "DateOfChange":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.DateOfChange)); //сортировка по дате изменения
                    break;
                case "Account":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.Account)); //сортировка по типу счета
                    break;
                case "Money":
                    nowclientList.Sort(Sort.SortedBy(Sort.SortedCriterion.Money)); //сортировка по количеству денег на счету
                    break;
            }
            return nowclientList;
        }
        /// <summary>
        /// Перевод денег с одного счета на другой
        /// </summary>
        /// <typeparam name="T">Client</typeparam>
        /// <param name="client1">Счет с которого переводят</param>
        /// <param name="client2">Счет на который переводят</param>
        /// <param name="changingMoney">Сумма перевода</param>
        public static void Transfer<T>(T client1, T client2, int changingMoney)
            where T: Client
        {
            client1.NonDepositMoney.Balance -= changingMoney;
            client2.NonDepositMoney.Balance += changingMoney;
            Program.SaveClientsToJson(MainWindow.clientList);
        }
        /// <summary>
        /// Проверка номера телефона для перевода
        /// </summary>
        /// <param name="phoneNumber">номер для перевода</param>
        /// <returns></returns>
        public static bool CheckPhoneNumber(string phoneNumber)
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
        public static bool CheckAmountOfMoney(string money)
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
        /// <summary>
        /// Визуальное представление доступных счетов клиента
        /// </summary>
        /// <param name="_mainWindow"></param>
        public static void CheckTypeOfAccount(MainWindow _mainWindow)
        {
            if(_mainWindow.ListViewClients.SelectedItem != null)
            {
                MainWindow.clientList = GetClientsInfoFromJson();
                System.Windows.Controls.ContextMenu CM =
                            (_mainWindow.ListViewClients.FindResource("LVContextMenu")) as System.Windows.Controls.ContextMenu;
                if ((_mainWindow.ListViewClients.SelectedItem as Client).DepositMoney != null)
                {
                    (CM.Items[2] as System.Windows.Controls.MenuItem).IsChecked = true;
                }
                else
                {
                    (CM.Items[2] as System.Windows.Controls.MenuItem).IsChecked = false;
                }


                if ((_mainWindow.ListViewClients.SelectedItem as Client).NonDepositMoney != null)
                {
                    (CM.Items[1] as System.Windows.Controls.MenuItem).IsChecked = true;
                }
                else
                {
                    (CM.Items[1] as System.Windows.Controls.MenuItem).IsChecked = false;
                }
            }
            
            
        }
    }
}
