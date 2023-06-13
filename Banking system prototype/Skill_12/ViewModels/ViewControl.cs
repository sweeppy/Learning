using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainLogic;

namespace Skill_12
{
    public static class ViewControl
    {
        /// <summary>
        /// Перевод денег с одного счета на другой
        /// </summary>
        /// <typeparam name="T">Client</typeparam>
        /// <param name="client1">Счет с которого переводят</param>
        /// <param name="client2">Счет на который переводят</param>
        /// <param name="changingMoney">Сумма перевода</param>
        public static void Transfer<T>(T client1, T client2, int changingMoney)
            where T : Client
        {
            client1.NonDepositMoney.Balance -= changingMoney;
            client2.NonDepositMoney.Balance += changingMoney;
            MainWindow.clientList.SaveClientsToJson();
        }

        /// <summary>
        /// Визуальное представление доступных счетов клиента
        /// </summary>
        /// <param name="_mainWindow"></param>
        public static void CheckTypeOfAccount(MainWindow _mainWindow)
        {
            if (_mainWindow.ListViewClients.SelectedItem != null)
            {
                MainWindow.clientList = Program.GetClientsInfoFromJson();
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

        /// <summary>
        /// Лоигка сортировки
        /// </summary>
        /// <param name="criterionstr"></param>
        public static List<Client> SortLogic(this List<Client> clientList, MainWindow mainWindow, string criterionstr)
        {
            List<Client> list = new List<Client>();
            list = ViewControl.FindByDepartmentID(clientList, mainWindow);
            switch(criterionstr)
            {
                case "FirstName":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.FirstName)); //сортировка по имени
                    break;
                case "LastName":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.LastName)); //сортировка по фамилии
                    break;
                case "Patronymic":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.Patronymic)); //сортировка по отчеству
                    break;
                case "PhoneNumber":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.PhoneNumber)); //сортировка по номеру телефона
                    break;
                case "PassportSeries":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.PassportSeries)); //сортировка по серии паспорта
                    break;
                case "PassportID":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.PassportID)); //сортировка по номеру паспорта
                    break;
                case "DateOfChange":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.DateOfChange)); //сортировка по дате изменения
                    break;
                case "Account":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.Account)); //сортировка по типу счета
                    break;
                case "Money":
                    list.Sort(Sort.SortedBy(Sort.SortedCriterion.Money)); //сортировка по количеству денег на счету
                    break;
            }
            return list;
        }


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
    }
}
