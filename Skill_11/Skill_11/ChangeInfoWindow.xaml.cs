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
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace Skill_11
{
    /// <summary>
/// Для изменения данных
/// </summary>
    interface ChangeInfo
    {
        /// <summary>
        /// Вывод нужного текста в окне для изменений
        /// </summary>
        /// <param name="index">Указывает, на того, кто работает(Manager, Consultant)</param>
        void GetTextByIndexOfCommand(int index);
        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="index"></param>
        /// <param name="WhoWorking">Указывает, на того, кто работает(Manager, Consultant)</param>
        void ChangeParamsByIndexOfCommand(int index, string WhoWorking);
    }
    /// <summary>
    /// Логика взаимодействия для ChangeInfoWindow.xaml
    /// </summary>
    public partial class ChangeInfoWindow : Window, ChangeInfo
    {
        Repository repository = new Repository();
        
        List<Client> clients = new List<Client>();

        private ConsultantPage _consultantPage;

        private ManagerPage _managerPage;

        private int index = 0;//Указывает на номер выбранного item в ConextMenu

        private string WhoWorking = ""; //Указывает, на того, кто работает(Manager, Consultant)

        /// <summary>
        /// Конструктор для Менеджера
        /// </summary>
        /// <param name="managerPage"></param>
        public ChangeInfoWindow(ManagerPage managerPage)
        {
            InitializeComponent();
            _managerPage = managerPage;
            index = _managerPage.indexOfCommand;
            WhoWorking = "Manager";
            GetTextByIndexOfCommand(index); 
        }
        /// <summary>
        /// Конструктор для консультанта
        /// </summary>
        /// <param name="consultantPage"></param>
        public ChangeInfoWindow(ConsultantPage consultantPage)
        {
            InitializeComponent();
            _consultantPage = consultantPage;
            index = 3;
            WhoWorking = "Consultant";
            GetTextByIndexOfCommand(index);
        }
        /// <summary>
        /// Закрытие окна изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (sender is Window w) w.Hide();
            switch (WhoWorking)
            {
                case "Consultant"://Consulant
                        GetTextByIndexOfCommand(3);
                    break;

                case "Manager"://Manager
                    if (_managerPage.indexOfCommand != 7)
                    {
                        GetTextByIndexOfCommand(index);
                    }
                    break;
            }
        }
        /// <summary>
        /// Исчезновение текста при навождении мышки на окно изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeText_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ChangeText.Text != "")
            {
                System.Threading.Thread.Sleep(200);
                ChangeText.Clear();
            }

        }
        /// <summary>
        /// Проверка на нажатие Enter(Подтверждение ввода)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                ChangeParamsByIndexOfCommand(index, WhoWorking);
            }
        }
        /// <summary>
        /// Изменение данных
        /// </summary>
        /// <param name="index">Что нужно изменить</param>
        /// <param name="Who">Кто изменяет(Manager, Consultant)</param>
        public void ChangeParamsByIndexOfCommand(int index, string Who)
        {
            clients = repository.GetClientsInfoFromJson();
            int temp = 0;
            bool success = false;

            switch (Who)//Поиск клиента по номеру телефона
            {
                case "Manager":
                    foreach (var item in clients)//Менеджер
                    {
                        if (item.PhoneNumber == (_managerPage.ListViewClients.SelectedItem as Client).PhoneNumber)
                        {
                            temp = clients.IndexOf(item);
                        }
                    }
                    break;
                case "Consultant":
                    foreach (var item in clients)//Консультант
                    {
                        if (item.PhoneNumber == (_consultantPage.ListViewClients.SelectedItem as Client).PhoneNumber)
                        {
                            temp = clients.IndexOf(item);
                        }
                    }
                    break;
            }
            string jsonSer = "";//Строка для json
            switch (index)
            {
                case 0:
                    ChangeFirstName(temp, clients);//Изменение имени
                    success = true;
                    break;
                case 1:
                    ChangeLastName(temp, clients);//Изменение фамилии
                    success=true;
                    break;
                case 2:
                    ChangePatronymic(temp, clients);//Изменение отчества
                    success = true;
                    break;
                case 3:
                    success = ChangePhoneNumber(temp, clients, success, jsonSer);//Изменение номера телефона
                        break;
                case 4:
                    success = ChangePassportSeries(temp, clients, success, jsonSer);//Изменение серии паспорта
                    break;
                case 5:
                    success = ChangePassportID(temp, clients, success, jsonSer);//Изменение номера паспорта
                    break;

                case 6:
                    success = ChangeClientDepartmentID(temp, clients, success);
                    break;
            }
            this.Close();//Закрытия окна для изменений
            if (success)
            {
                switch (WhoWorking)
                {
                    case "Manager":
                        //Изменял Manager
                        _managerPage.ListViewClients.ItemsSource = _managerPage.Find(clients);//Обновление листа клиентов
                        clients[temp].WhoChanged = "Менеджер";
                        break;
                    case "Consultant":
                        //Изменял Consultant
                        _consultantPage.ListViewClients.ItemsSource = _consultantPage.Find(clients);//Обновление листа клиентов
                        clients[temp].WhoChanged = "Консультант";
                        break;
                }

                clients[temp].TypeOfChanging = "Изменение записи";
                clients[temp].DateOfChange = DateTime.Now;
                repository.SaveClientsToJson(clients);//сохранение измененных данных в json
            } 
        }
        public void GetTextByIndexOfCommand(int index)
        {
            
            switch (index)
            {
                case 0:
                    ChangeText.Text = "Введите новое имя:";
                    break;
                case 1:
                    ChangeText.Text = "Введите новую фамилию:";
                    break;
                case 2:
                    ChangeText.Text = "Введите новое отчество:";
                    break;
                case 3:
                    ChangeText.Text = "Введите новый номер телефона:";
                    break;
                case 4:
                    ChangeText.Text = "Введите новую серию паспорта:";
                    break;
                case 5:
                    ChangeText.Text = "Введите новый номер паспорта";
                    break;
                case 6:
                    ChangeText.Text = "Введите ID нового департамента";
                    break;
            }
        }
        /// <summary>
        /// Изменение имени
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        private void ChangeFirstName(int temp, List<Client> clients)
        {
            clients[temp].FirstName = ChangeText.Text;
            clients[temp].InformationThatChanged = "Изменено имя";
        }
        /// <summary>
        /// Изменение фамилии
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        private void ChangeLastName(int temp, List<Client> clients)
        {
            clients[temp].LastName = ChangeText.Text;
            clients[temp].InformationThatChanged = "Изменена фамилия";
        }
        /// <summary>
        /// Изменение Отчества
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        private void ChangePatronymic(int temp, List<Client> clients)
        {
            clients[temp].Patronymic = ChangeText.Text;
            clients[temp].InformationThatChanged = "Изменено отчество";
        }
        /// <summary>
        /// Изменение номера телефона
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="success">Подтверждение изменения</param>
        /// <param name="jsonSer">Строка для Des. и Ser. json</param>
        /// <returns>Общий success</returns>
        private bool ChangePhoneNumber(int temp, List<Client> clients, bool success, string jsonSer)
        {
            if (File.Exists(Client.pathPhoneNumbers))
            {
                jsonSer = File.ReadAllText(Client.pathPhoneNumbers);
                Client.phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer);                                                                        //с номерами телефонов из Json
            }
            //Если существует файл, дессериализация списка с номерами телефонов

            bool successOfPhoneNumber = Double.TryParse(ChangeText.Text, out var Phonenumber);//Проверка ввода номера телефона
            successOfPhoneNumber = !Client.phoneNumbers.Contains(Phonenumber.ToString());//Проверка на уникальность данного номера

            if (successOfPhoneNumber && Phonenumber.ToString().Length == 11)//Если всё правильно, поиск номера, который нужно заменить в базе номеров
            {
                switch (WhoWorking)
                {
                    case "Manager":
                        temp = Client.phoneNumbers.IndexOf((_managerPage.ListViewClients.SelectedItem as Client).PhoneNumber);//поиск для Менджера
                        break;
                    case "Consultant":
                        temp = Client.phoneNumbers.IndexOf((_consultantPage.ListViewClients.SelectedItem as Client).PhoneNumber);//поиск для Консультанта
                        break;
                }

                Client.phoneNumbers[temp] = Phonenumber.ToString();//Замена номера в базе данных номеров
                clients[temp].InformationThatChanged = "Изменен номер телефона";
                clients[temp].PhoneNumber = Phonenumber.ToString();//Замена номера у клиента

                jsonSer = JsonConvert.SerializeObject(Client.phoneNumbers); //сохранение списка номеров телефона в Json
                File.WriteAllText(Client.pathPhoneNumbers, jsonSer);

                success = true;
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"Error. Данный номер телефона уже существует, или выполнен неверный ввод");
            }


            return success;
        }
        /// <summary>
        /// Изменение серии паспорта
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="success">Подтверждение изменения</param>
        /// <param name="jsonSer">Строка для Des. и Ser. json</param>
        /// <returns>Общий success</returns>
        private bool ChangePassportSeries(int temp, List<Client> clients, bool success, string jsonSer)
        {
            if (File.Exists(Client.pathPassports))
            {
                jsonSer = File.ReadAllText(Client.pathPassports);
                Client.passports = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonSer);
            }
            //Если существует файл, дессериализация библиотеки паспортов

            bool successOfSeriesPassport = int.TryParse(ChangeText.Text, out var numberOfSeriesPassport);//проверка ввода
            successOfSeriesPassport = !Client.passports.Keys.Contains(numberOfSeriesPassport);//Проверка на уникальность
            

            if (successOfSeriesPassport && numberOfSeriesPassport > 1000 && numberOfSeriesPassport < 9999)
            {
                ChangeKey(Client.passports, (_managerPage.ListViewClients.SelectedItem as Client).PassportSeries, numberOfSeriesPassport);
                                                                                            //Замена серии в базе паспортов

                clients[temp].PassportSeries = numberOfSeriesPassport;//Замепна серии в базе клиентов
                clients[temp].InformationThatChanged = "Изменена серия паспорта";


                jsonSer = JsonConvert.SerializeObject(Client.passports); //сохранение списка паспортов в Json
                File.WriteAllText(Client.pathPassports, jsonSer);
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();//ОШИБКА!
                MessageBox.Show($"Error. Данная серия паспорта уже существует, или выполнен неверный ввод");
            }

            success = successOfSeriesPassport;
            return success;
        }
        /// <summary>
        /// Изменение номера паспорта
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="success">Подтверждение изменения</param>
        /// <param name="jsonSer">Строка для Des. и Ser. json</param>
        /// <returns>Общий success</returns>
        private bool ChangePassportID(int temp, List<Client> clients, bool success, string jsonSer)
        {
            if (File.Exists(Client.pathPassports))
            {
                jsonSer = File.ReadAllText(Client.pathPassports);//Если существует файл, дессериализация библиотеки паспортов
                Client.passports = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonSer);
            }
            

            bool successOfPassportID = int.TryParse(ChangeText.Text, out var numberOfPassportID);//Проверка ввода
            successOfPassportID = !Client.passports.Values.Contains(numberOfPassportID);//Проверка на уникальность

            if (successOfPassportID && numberOfPassportID > 100000 && numberOfPassportID < 999999)
            {
                if (Client.passports.TryGetValue((_managerPage.ListViewClients.SelectedItem as Client).PassportSeries, out var value))
                {
                    Client.passports[(_managerPage.ListViewClients.SelectedItem as Client).PassportSeries] = numberOfPassportID;
                                                                                            //Замена номера паспорта в базе паспортов
                }

                clients[temp].PassportID = numberOfPassportID;//Замена номера паспорта в базе клиентов
                clients[temp].InformationThatChanged = "Изменен номер паспорта";

                jsonSer = JsonConvert.SerializeObject(Client.passports); //сохранение списка паспортов в Json
                File.WriteAllText(Client.pathPassports, jsonSer);
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();//ОШИБКА!
                MessageBox.Show($"Error. Данный номер паспорта уже существует, или выполнен неверный ввод");
            }
            success = successOfPassportID;
            return success;
        }
        /// <summary>
        /// Смена департамента клиента
        /// </summary>
        /// <param name="temp">Индекс выбранного клиента</param>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="success">Подтверждение изменения</param>
        /// <returns>Общий success</returns>
        private bool ChangeClientDepartmentID(int temp, List<Client> clients, bool success)
        {
            bool successOfDepartmentID = int.TryParse(ChangeText.Text, out var numberOfDepartmentID);//Проверка ввода

            if (successOfDepartmentID)//Если ввод правильный, поиск департамента с введеным ID
            {
                foreach (var item in _managerPage.departmentList)
                {
                    if (item.DepartamentID == numberOfDepartmentID) //Поиск
                    {
                        clients[temp].DepartmentID = numberOfDepartmentID;
                        clients[temp].InformationThatChanged = "Изменён департамент";//сЕсли удалось найти, замена
                        success = true;
                        break;
                    }
                }
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();//ОШИБКА!
                MessageBox.Show($"Error. Департамента с данным ID не существует, или выполнен неверный ввод");
            }

            return success;
        }

        /// <summary>
        /// Изменяет значение Key в Dictionary по значению Key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="fromKey"></param>
        /// <param name="toKey"></param>
        private void ChangeKey<TKey, TValue>(IDictionary<TKey, TValue> dic,
                                      TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }
    }
}
