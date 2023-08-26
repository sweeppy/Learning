using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Microsoft.AspNet.Identity.EntityFramework;
using NotebookWPFApi.Data;
using NotebookWPFApi.Interfaces;
using NotebookWPFApi.Models;
using NotebookWPFApi.ViewModels;
using NotebookWPFApi.ViewModels.Data;

namespace NotebookWPFApi.Views
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        MainWindow _mainWin;
        DataApi _dataApi;
        IAccess _access;
        public SignInWindow()
        {
            InitializeComponent();
            _dataApi = new DataApi();
            _access = new Repository();
        }

        public SignInWindow(MainWindow mainWin):this()
        {
            _mainWin = mainWin;
        }

        private async void Sign_In_ClickAsync(object sender, RoutedEventArgs e)
        {
            bool successInput = Checks.CheckforSignIn(LoginTB.Text, PasswordTB.Text);//проверка на пустой ввод

            if (successInput)
            {
               await _dataApi.Login(LoginTB.Text, PasswordTB.Text);//Проверяем данные для входа
            }
            else
            {
                MessageBox.Show("Wrong Input");
                return;
            }

                var rolesTask = _dataApi.GetUserRoles(LoginTB.Text);//получение ролей пользователя
                List<string> roles = await rolesTask;  // ожидание завершения задачи и получение ролей пользователя\

                _mainWin.rolesOfNowUser = roles;//сохранение ролей пользователя(для дальнейшего выхода)

                _mainWin.OnlineTB.Text = LoginTB.Text + " Online";

                _access.GiveAccess(roles, _mainWin);//Дает доступ к функциям авторизованного пользователя или админа
                
                this.Close();
        }
    }
}
