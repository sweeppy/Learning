using NotebookWPFApi.Authorization;
using NotebookWPFApi.Data;
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
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        DataApi _dataApi;
        public SignUpWindow()
        {
            InitializeComponent();
        }

        public SignUpWindow(DataApi dataApi):this()
        {
            _dataApi = dataApi;
        }

        private void Sign_Up_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (PasswordTB.Text == ConfirmPasswordTB.Text)
            {
                bool strongPassword = Checks.IsPasswordStrong(PasswordTB.Text);//проверка надежности пароля

                if (strongPassword)
                {
                    var user = new UserRegistration
                    {
                        LoginProp = LoginTB.Text,
                        Password = PasswordTB.Text,
                        ConfirmPassword = ConfirmPasswordTB.Text
                    };
                    _dataApi.Registration(user);
                }
                else MessageBox.Show("Your password must contains :" +
                    "                 \n 1)Capital English letters" +
                    "                 \n 2)English letters" +
                    "                 \n 3)Numbers" +
                    "                 \n 4)Sprcial sybmols, like: '!, @, #, ...'");
                                
            }
            else MessageBox.Show("Passwords must match");
            this.Close();
        }
    }
}
