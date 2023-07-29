using System;
using System.Collections.Generic;
using System.Data;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public Clients newClient;
        private AddClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Нажатие на кнопки отмены или подтверждения
        /// </summary>
        /// <param name="row"></param>
        public AddClient(MainWindow mainWin):this()
        {
            BtnCancel.Click += delegate { this.DialogResult = false; };

            BtnConfirm.Click += delegate
            {
                var client = new Clients(txtFirstName.Text, txtLastName.Text, txtPatronymic.Text,
                                         txtPhoneNumber.Text, txtEmail.Text);

                if (Checks.CheckClient(client) &&
                txtPhoneNumber.Text.Length == 11 &&
                txtEmail.Text.Contains("@") &&
                !mainWin.mainDataGrid.Items.Contains(txtEmail))
                {
                        newClient = new Clients(txtFirstName.Text,
                        txtLastName.Text,
                        txtPatronymic.Text,
                        txtPhoneNumber.Text,
                        txtEmail.Text);
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                    string errors = "";

                    if (txtFirstName.Text == "") errors += "Wrong First name";
                    if (txtLastName.Text == "") errors += "\nWrong Last name";
                    if (txtPatronymic.Text == "") errors += "\nWrong Patronymic";
                    if (txtPhoneNumber.Text == "" || txtPhoneNumber.Text.Length != 11) errors += "\nWrong phone number";
                    if (txtEmail.Text == "" || !txtEmail.Text.Contains("@") && mainWin.mainDataGrid.Items.Contains(txtEmail)) errors += "\nWrong email";
                    MessageBox.Show(errors);
                }
                
            };
        }

    }
}
