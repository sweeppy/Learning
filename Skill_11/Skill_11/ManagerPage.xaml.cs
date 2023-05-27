using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;


namespace Skill_11
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {


        Repository repository = new Repository();
        Employer manager = new Manager();

        public List<Department> departmentList = new List<Department>();
        public List<Client> clientList = new List<Client>();

        public ContextMenu contextMenu; //контекстное меню по нажатию ПКМ по клиенту
        public int indexOfCommand; //какой item выбран в ContextMenu(ПКМ по клиенту)
        public ManagerPage()
        {
            InitializeComponent();
            clientList = repository.GetClientsInfoFromJson();//получение списка клентов из json
            departmentList = repository.GetDepartmentsInfoFromJson();//Получение списка департаментов из json
            CbDepartments.ItemsSource = departmentList;
        }
        /// <summary>
        /// распределение клиентов по их департаментам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientList = repository.GetClientsInfoFromJson();
            ListViewClients.ItemsSource = Find(clientList);
        }
        /// <summary>
        /// Формирование листа клиентов по DepartmentID в соответсвии с выбраннным департаментом
        /// </summary>
        /// <returns>Лист клиентов</returns>
        public List<Client> Find(List<Client> clientList)
        {
            List<Client> tempList = new List<Client>();
            if (CbDepartments.SelectedItem != null)
            {
                foreach (var item in clientList)
                {
                    if ((CbDepartments.SelectedItem as Department).DepartamentID == item.DepartmentID)
                    {
                        tempList.Add(item);
                    }
                }
            }

            return tempList;

        }
        /// <summary>
        /// Добавление двух рандомных клиентов в отдельный департамент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRandomClients_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            manager.GenerateRandomClients(clientList, "Менеджер", departmentList);//добавление 2ух клиентов в список клментов
            if ((CbDepartments.SelectedItem as Department) != null && (CbDepartments.SelectedItem as Department).DepartamentID == 0)//Если департмент
            {                                                                                                                       //с рандомными 
                clientList = repository.GetClientsInfoFromJson();                                                                   //клиентами
                ListViewClients.ItemsSource = Find(clientList);                                                                     //выбран 
            }                                                                                                                       
            else
            {
                departmentList = repository.GetDepartmentsInfoFromJson();
                CbDepartments.ItemsSource = departmentList;
            }

        }
        /// <summary>
        /// Изменение данных клиента или его удаление
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextMenu = ListViewClients.FindResource("ItemContextMenu") as ContextMenu;
            indexOfCommand = contextMenu.Items.IndexOf(sender);

            if (indexOfCommand != 8)
            {
                ChangeInfoWindow changeInfoWindow = new ChangeInfoWindow(this);//Изменение данных клиента
                changeInfoWindow.ShowDialog();
            }
            else
            {
                MessageBoxResult confirmResult = MessageBox.Show
               ("Вы уверены, что хотите удалить этого клиента?", "Подтвердите действие!", MessageBoxButton.YesNo);

                if (confirmResult == MessageBoxResult.Yes)//Удаление клиента
                {
                    string json;
                    clientList = repository.GetClientsInfoFromJson();
                    Client.passports.Remove((ListViewClients.SelectedItem as Client).PassportSeries); //  удаление данных паспорта,
                                                                                                      //  выбранного клиента из списка и
                    json = JsonConvert.SerializeObject(Client.passports);                             //  сохранение списка паспортов в Json
                    File.WriteAllText(Client.pathPassports, json);

                    Client.phoneNumbers.Remove((ListViewClients.SelectedItem as Client).PhoneNumber); // удаление номера телефона,
                                                                                                      // выбранного клиента из списка и  
                    json = JsonConvert.SerializeObject(Client.phoneNumbers);                          //сохранение списка номеров телефона в Json
                    File.WriteAllText(Client.pathPhoneNumbers, json);

                    RemoveClient(ListViewClients.SelectedItem as Client);         //Удаление пользователся
                    ListViewClients.ItemsSource = Find(clientList);
                }


            }

        }
        /// <summary>
        /// Поиск и удаление клиента из базы данных клиентов
        /// </summary>
        /// <param name="client">Клиент, которого нужно удалить</param>
        private void RemoveClient(Client client)
        {
            foreach (var item in clientList)
            {
                if (client.PassportSeries == item.PassportSeries && client.PassportID == item.PassportID)
                {
                    clientList.Remove(item);
                    repository.SaveClientsToJson(clientList);
                    break;
                }
            }
        }
        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddClient addClientWin = new AddClient(this);
            addClientWin.ShowDialog();
            if ((CbDepartments.SelectedItem as Department) != null && (CbDepartments.SelectedItem as Department).DepartamentID == 0)
                ListViewClients.ItemsSource = Find(clientList);//обновление отображаемого списка

        }


        /// <summary>
        /// Лоигка сортировки
        /// </summary>
        /// <param name="criterionstr"></param>
        private void SortLogic(string criterionstr)
        {
            List <Client> nowclientList = new List<Client>();
            nowclientList = Find(clientList);
            switch (criterionstr)
            {
                case "FirstName":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.FirstName)); //сортировка по имени
                    break;
                case "LastName":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.LastName)); //сортировка по фамилии
                    break;
                case "Patronymic":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.Patronymic)); //сортировка по отчеству
                    break;
                case "PhoneNumber":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PhoneNumber)); //сортировка по номеру телефона
                    break;
                case "PassportSeries":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PassportSeries)); //сортировка по серии паспорта
                    break;
                case "PassportID":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PassportID)); //сортировка по номеру паспорта
                    break;
                case "DateOfChange":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.DateOfChange)); //сортировка по дате изменения
                    break;
                case "InformationThatChanged":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.InformationThatChanged)); //сортировка по информации, которую изменили
                    break;
                case "WhoChanged":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.WhoChanged));  //сортировка по тому, кто изменил
                    break;
                case "TypeOfChanging":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.TypeOfChanging)); //сортировка по типу изменения
                    break;
            }
            if ((CbDepartments.SelectedItem as Department) != null)
                ListViewClients.ItemsSource = nowclientList;
        }
        /// <summary>
        /// Нажатие на выбранную колонку(Сортировка)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortstring = column.Tag.ToString();
            SortLogic(sortstring);
        }
        /// <summary>
        /// Добавление нового департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDepartment_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddDepartment addDepartmentWin = new AddDepartment(this);
            addDepartmentWin.ShowDialog();
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int temp = (CbDepartments.SelectedItem as Department).DepartamentID;
            List<Client> removedClients = new List<Client>();
            if (temp == 1)//Удаление департамента удалённых клиентов(безвозвратно)
            {
                MessageBoxResult confirm = MessageBox.Show
                    ("Вы уверены, что хотите безвозвратно удалить этот департамент?", "Подтвердите действие!", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    removedClients = Find(clientList);
                    foreach (var item in removedClients)
                    {
                        if (Client.passports.ContainsKey(item.PassportSeries))
                        {
                            Client.passports.Remove(item.PassportSeries);//Удаление паспортов из их базы данных
                        }
                        if (Client.phoneNumbers.Contains(item.PhoneNumber))
                        {
                            Client.phoneNumbers.Remove(item.PhoneNumber);//Удалиение номеров телефона из их базы данных
                        }
                            
                        clientList.Remove(item);//Удаление клиентов
                    }
                    string json = "";
                    json = JsonConvert.SerializeObject(Client.passports); //сохранение списка паспортов в Json
                    File.WriteAllText(Client.pathPassports, json);

                    json = JsonConvert.SerializeObject(Client.phoneNumbers); //сохранение списка номеров в Json
                    File.WriteAllText(Client.pathPhoneNumbers, json);
                }

            }
            else
            {
                MessageBoxResult confirmResult = MessageBox.Show//Удаление департамента(все клиенты этого департамента перемещаются в департамент
                                                                //для удалённых клиентов)

            ("Вы уверены, что хотите удалить этот департамент?", "Подтвердите действие!", MessageBoxButton.YesNo);
                
                if (confirmResult == MessageBoxResult.Yes)
                {
                    Department department = new Department("DepartmentOfDeletedClients", 1);
                    if (!departmentList.Contains(department))//Проверка существования департамента для удаленных клиентов
                    {
                        departmentList.Add(department);
                    }

                    removedClients = Find(clientList);
                    foreach (var item in removedClients)
                    {
                        clientList.Remove(item);
                        item.DepartmentID = 1;//Перераспределение клиентов
                        clientList.Add(item);
                    }

                    
                    departmentList.RemoveAt(temp);//Удаление департамента
                    repository.SaveDepartmentsToJson(departmentList);//Сохранение базы данных департаментов после удаления
                    
                    
                } 
            }
            
            repository.SaveClientsToJson(clientList);//Сохранение клиентов
            CbDepartments.Items.Refresh();
            CbDepartments.ItemsSource = departmentList;//Обновление департаментов
            ListViewClients.ItemsSource = Find(clientList);
        }

    }
}
