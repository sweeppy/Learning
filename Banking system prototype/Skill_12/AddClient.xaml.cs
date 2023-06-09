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

namespace Skill_12
{
    public partial class AddClient : Window
    {
        private MainWindow _mainWindow;
        List<string> errorList = new List<string>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mainWindow"></param>
        public AddClient(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
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
                MainWindow.clientList = Program.GetClientsInfoFromJson(); //получение списка клиентов из json
                Client newClient = new Client(
                    TbLastName.Text,
                    TbFirstName.Text,
                    TbPatrynomic.Text,
                    TbPhoneNumber.Text,                                     //Добавление нового клиента
                    TbPassportSeries.Text,
                    TbPassportID.Text,                                              
                    CbTypeOfAccount.Text,
                    Status.SelectedIndex);

                MainWindow.clientList.Add(newClient);
                Program.SaveClientsToJson(MainWindow.clientList);
                this.Close();
                
            }
            else
            {
                string temp = "";
                foreach (var item in errorList)
                {
                    temp += $"\n{item}";                              //Вывод ошибок, если они есть
                }
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"Error. {temp}");
                errorList.Clear();
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
                Client.passports = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonSer);
            }


            bool success = false; //главное подтверждение правильности ввода

            bool successOfPhoneNumber = long.TryParse(TbPhoneNumber.Text, out var Phonenumber);//проверка номера телефона
            successOfPhoneNumber = !Client.phoneNumbers.Contains(Phonenumber.ToString());

            bool successOfSeriesPassport = int.TryParse(TbPassportSeries.Text, out var numberOfSeriesPassport);//проверка серии паспорта
            successOfSeriesPassport = !Client.passports.ContainsKey(TbPassportSeries.Text);

            bool successOfPassportID = int.TryParse(TbPassportID.Text, out var numberOfPassportID);//проверка номера паспорта
            successOfPassportID = !Client.passports.ContainsValue(TbPassportID.Text);

            bool successOfAccount = false;
            if (CbTypeOfAccount.SelectedItem != null)
                successOfAccount = true;

            bool successOfDepartmentID = false;
                if(Status.SelectedItem != null)
                successOfDepartmentID = true;

            bool successOfFirstName = TbFirstName.Text != "";//проверка имени
            bool successOfLastName = TbLastName.Text != "";//проверка фамилии
            bool successOfPatrynomic = TbPatrynomic.Text != "";//проверка отчества


            if (successOfDepartmentID
                && successOfPassportID
                && successOfPhoneNumber
                && successOfSeriesPassport
                && Phonenumber.ToString().Length == 11
                && TbPassportSeries.Text.Length == 4                //если все правильно
                && TbPassportID.Text.Length == 6
                && successOfAccount
                && successOfFirstName
                && successOfLastName
                && successOfPatrynomic
                )
            {
                success = true;//подтверждаем правильность ввода

                Client.passports.Add(TbPassportSeries.Text, TbPassportID.Text);//добавляем в библиотеку серию и номер паспорта
                Client.phoneNumbers.Add(Phonenumber.ToString());//добавляем в список номер телефонов

                jsonSer = JsonConvert.SerializeObject(Client.phoneNumbers); //сохранение списка номеров телефона в Json
                File.WriteAllText(Client.pathPhoneNumbers, jsonSer);

                jsonSer = JsonConvert.SerializeObject(Client.passports);//сохранение библиотеки паспортов в Json
                File.WriteAllText(Client.pathPassports, jsonSer);
            }
            else
            {   //Если есть ошибки добаление их в список ошибок
                if (!successOfFirstName)
                    errorList.Add("Введено неправильное имя клиента.");
                if (!successOfLastName)
                    errorList.Add("Введена неправильная фамилия клиента.");
                if (!successOfPatrynomic)
                    errorList.Add("Введено неправильное отчество клиента.");
                if (!successOfDepartmentID)
                    errorList.Add("Введен неправильный ID департамента.");
                if (!successOfSeriesPassport || TbPassportSeries.Text.Length != 4)
                    errorList.Add("Введена неправильная серия паспорта клиента.");
                if (!successOfPassportID || TbPassportID.Text.Length != 6)
                    errorList.Add("Введен неправильный номер паспорта клиента.");
                if (!successOfPhoneNumber || Phonenumber.ToString().Length != 11)
                    errorList.Add("Введен неправильный номер телефона клиента.");
                if (!successOfAccount)
                    errorList.Add("Не выбран тип счета.");
            }
            return success;
        }

    }
}

