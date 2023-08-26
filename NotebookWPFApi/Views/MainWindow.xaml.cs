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
using NotebookWPFApi.Data;
using NotebookWPFApi.Interfaces;
using NotebookWPFApi.Models;
using NotebookWPFApi.ViewModels;
using NotebookWPFApi.ViewModels.Data;

namespace NotebookWPFApi.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static NotebookEntities notebookContext;
        private DataApi _dataApi;
        IAccess _access;
        public List<string> rolesOfNowUser;
        Repository repository;
        public MainWindow()
        {
            InitializeComponent();

            _dataApi = new DataApi();

            fullDataGrid.ItemsSource = _dataApi.GetClients();
            shortDataGrid.ItemsSource = _dataApi.GetClients();

            notebookContext = new NotebookEntities();

            _access = new Repository();
            repository = new Repository();

            List<string> rolesOfNowUser = new List<string>();
        }
        /// <summary>
        /// Нажатие на кнопку "Get all information"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Get_allInformation_Click(object sender, RoutedEventArgs e)
        {
            shortphoneNumberTC.Visibility = Visibility.Visible;
            shortaddressTC.Visibility = Visibility.Visible;
            shortDescriptionTc.Visibility = Visibility.Visible;
            shortGetAllInfDGTM.Visibility = Visibility.Hidden;

            shortDataGrid.ItemsSource = _dataApi.GetClients().Where(cl => cl.Id == (shortDataGrid.SelectedItem as Client).Id);
        }
        /// <summary>
        /// Нажатие на кнопку входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sign_in_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow(this);
            signInWindow.ShowDialog();

        }
        /// <summary>
        /// Нажатие на кнопку регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sign_up_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signWin = new SignUpWindow(_dataApi);
            signWin.ShowDialog();
        }
        /// <summary>
        /// Нажатие на кнопку обновления клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_all_dataGrid_Click(object sender, RoutedEventArgs e)
        {
            repository.Update(this, this._dataApi);
        }
        /// <summary>
        /// Нажатие на кнопку выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            _access.BlockAccess(rolesOfNowUser, this);
            OnlineTB.Text = "";
        }
        /// <summary>
        /// Нажатие на кнопку добавления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Client_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWin = new AddClientWindow(_dataApi);
            addClientWin.ShowDialog();
            if (addClientWin.DialogResult == true)
            {
                repository.Update(this, this._dataApi);
            }
        }
        /// <summary>
        /// Нажатие на удаление клиента из полной таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Client_FullDG_Click(object sender, RoutedEventArgs e)
        {
            if (fullDataGrid.SelectedItem != null)
            {
                _dataApi.DeleteClient((fullDataGrid.SelectedItem as Client).Id);
                fullDataGrid.ItemsSource = _dataApi.GetClients();
            }   
        }
        /// <summary>
        /// Нажатие на кнопку удаления из краткой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Client_ShortDG_Click(object sender, RoutedEventArgs e)
        {
            if (shortDataGrid.SelectedItem != null)
            {
                _dataApi.DeleteClient((shortDataGrid.SelectedItem as Client).Id);
                shortDataGrid.ItemsSource = _dataApi.GetClients();
            }
        }

        private void FullTI_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!ShortTI.IsSelected)
            {
                ShortTI.IsSelected = true;
            }
        }

        private void fullTI_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (!fullTI.IsSelected)
            {
                fullTI.IsSelected = true;
            }
        }

        private async void fullDG_EditEndingAsync(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editClient = fullDataGrid.SelectedItem as Client;
            if (editClient == null) return;

            bool successInput = Checks.CheckNullInputOfClient(editClient);

            if (!successInput)
            {
                MessageBox.Show("Wrong input");
                fullDataGrid.ItemsSource = _dataApi.GetClients();
                return;
            }

            await _dataApi.FinishEdit(editClient);

        }
    }
}
