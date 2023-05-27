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
using System.IO;
using Newtonsoft.Json;


namespace Skill_11
{
    
    /// <summary>
    /// Логика взаимодействия для AddDepatment.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private ManagerPage _managerWindow;
        List<string> errorStrList = new List<string>();

        List<Client> clients = new List<Client>();
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="managerWindow"></param>
        public AddClient(ManagerPage managerWindow)
        {
            InitializeComponent(); 
            _managerWindow = managerWindow;
        }
        /// <summary>
        /// Завершение добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Finish_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (Check())
            {
                Repository repository = new Repository();
                clients = repository.GetClientsInfoFromJson(); //получение списка клиентов из json
                clients.Add(new Client(
                    TbLastName.Text,
                    TbFirstName.Text,
                    TbPatrynomic.Text,
                    TbPhoneNumber.Text,                                     //Добавление нового клиента
                    Convert.ToInt32(TbPassportSeries.Text),
                    Convert.ToInt32(TbPassportID.Text),
                    Convert.ToInt32(TbDepartmentID.Text)));
                    repository.SaveClientsToJson(clients);
                
                    _managerWindow.ListViewClients.ItemsSource = _managerWindow.Find(clients); //обновление отображения клиентов
                this.Close();
            }
            else
            {
                string temp = "";
                foreach (var item in errorStrList)
                {
                    temp+=$"\n{item}";                              //Вывод ошибок, если они есть
                }
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"Error. {temp}");
                errorStrList.Clear();
            }
            
        }
        /// <summary>
        /// Закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (sender is Window w) w.Hide();
        }
        /// <summary>
        /// Проверка правильности введенных данных
        /// </summary>
        /// <returns>True, если все введенные данные правильные, False, если нет</returns>
        private bool Check()
        {
            string jsonSer;
            if (File.Exists(Client.pathPhoneNumbers))
            {
                jsonSer = File.ReadAllText(Client.pathPhoneNumbers);            //Если существует файл, дессериализация списка номеров телефона из json
                Client.phoneNumbers = JsonConvert.DeserializeObject<List<string>>(jsonSer);                                                                        //с номерами телефонов из Json
            }


            if (File.Exists(Client.pathPassports))
            {
                jsonSer = File.ReadAllText(Client.pathPassports);           //Если существует файл, дессериализация библиотеки паспортов из json
                Client.passports = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonSer);  
            }


            bool success = false; //главное подтверждение правильности ввода

            bool successOfPhoneNumber = Double.TryParse(TbPhoneNumber.Text, out var Phonenumber);//проверка номера телефона
            successOfPhoneNumber = !Client.phoneNumbers.Contains(Phonenumber.ToString());

            bool successOfSeriesPassport = int.TryParse(TbPassportSeries.Text, out var numberOfSeriesPassport);//проверка серии паспорта
            successOfSeriesPassport = !Client.passports.ContainsKey(numberOfSeriesPassport);

            bool successOfPassportID = int.TryParse(TbPassportID.Text, out var numberOfPassportID);//проверка номера паспорта
            successOfPassportID = !Client.passports.ContainsValue(numberOfPassportID);

            bool successOfDepartmentID = int.TryParse(TbDepartmentID.Text, out var numberOfDepartmentID);//проверка ID департамента
            bool successOfFirstName = TbFirstName.Text != "";//проверка имени
            bool successOfLastName = TbLastName.Text != "";//проверка фамилии
            bool successOfPatrynomic = TbPatrynomic.Text != "";//проверка отчества


            if (successOfDepartmentID
                && successOfPassportID
                && successOfPhoneNumber
                && successOfSeriesPassport
                && Phonenumber.ToString().Length == 11
                && numberOfSeriesPassport >= 1000
                && numberOfSeriesPassport <= 9999               //если все правильно
                && numberOfPassportID >= 100000
                && numberOfPassportID <= 999999
                && successOfFirstName
                && successOfLastName
                && successOfPatrynomic
                )
            {
                success = true;//подтверждаем правильность ввода

                Client.passports.Add(numberOfSeriesPassport, numberOfPassportID);//добавляем в библиотеку серию и номер паспорта
                Client.phoneNumbers.Add(Phonenumber.ToString());//добавляем в список номер телефонов

                jsonSer = JsonConvert.SerializeObject(Client.phoneNumbers); //сохранение списка номеров телефона в Json
                File.WriteAllText(Client.pathPhoneNumbers, jsonSer);

                jsonSer = JsonConvert.SerializeObject(Client.passports);//сохранение библиотеки паспортов в Json
                File.WriteAllText(Client.pathPassports, jsonSer);
            }
            else
            {   //Если есть ошибки добаление их в список ошибок
                if (!successOfFirstName)
                    errorStrList.Add("Введено неправильное имя клиента.");
                if(!successOfLastName)
                    errorStrList.Add("Введена неправильная фамилия клиента.");
                if (!successOfPatrynomic)
                    errorStrList.Add("Введено неправильное отчество клиента.");
                if (!successOfDepartmentID)
                    errorStrList.Add("Введен неправильный ID департамента.");
                if (!successOfSeriesPassport || numberOfSeriesPassport < 1000 || numberOfSeriesPassport > 9999)
                    errorStrList.Add("Введена неправильная серия паспорта клиента.");
                if (!successOfPassportID || numberOfPassportID < 100000 || numberOfPassportID > 999999)
                    errorStrList.Add("Введен неправильный номер паспорта клиента.");
                if (!successOfPhoneNumber || Phonenumber.ToString().Length != 11)
                    errorStrList.Add("Введен неправильный номер телефона клиента.");
            }
            return success;
        }
        
    }
}
