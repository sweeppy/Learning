using NotebookWPFApi.Data;
using NotebookWPFApi.Models;
using NotebookWPFApi.ViewModels;
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

namespace NotebookWPFApi.Views
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
        }

        public AddClientWindow(DataApi _dataApi):this()
        {
            BtnCancel.Click += delegate { this.DialogResult = false; this.Close(); };

            BtnConfirm.Click += delegate
            {
                Client client = new Client//Создание нового клиента
                {
                    LastName = lastNameTb.Text,
                    FirstName = firstNameTb.Text,
                    Patronymic = patronymicTb.Text,
                    PhoneNumber = phoneNumberTb.Text,
                    Address = addressTb.Text,
                    Description = descriptionTb.Text
                };
                bool successInput = Checks.CheckNullInputOfClient(client);//Проверка на пустой ввод
                if (successInput)
                {
                    _dataApi.AddClient(client);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong input");
                    this.DialogResult = false;
                }
            };
        }
    }
}
