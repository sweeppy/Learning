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
using System.Windows.Threading;

namespace Skill_12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        public static List<Client> clientList = new List<Client>();
        public static List<Department> departments = new List<Department>();
        public bool Checked { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            
            departments = Program.GetDepartmentsInfoFromJson();
            if (departments.Count == 0)//Если первый запуск программы добавление 3-х департаментов
            {
                departments.Add(new Department("Обычные клиенты", 0));
                departments.Add(new Department("VIP клиенты", 1));
                departments.Add(new Department("Юридические лица", 2));
                Program.SaveDepartmentsToJson(departments);
            }

            CbDepartments.ItemsSource = departments;
        }
        /// <summary>
        /// Инициализация таймера
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }
        /// <summary>
        /// каждые 10 секунд умножение депозитного баланса в завсисимости от статуса клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            MainWindow.clientList = Program.GetClientsInfoFromJson();
            foreach (var item in clientList)
            {
                if (item.DepositMoney != null)
                {
                    switch (item.DepartmentID)
                    {
                        case 0:
                            item.DepositMoney.Balance *= 1.1m;
                            break;
                        case 1:
                            item.DepositMoney.Balance *= 1.12m;
                            break;
                        case 2:
                            item.DepositMoney.Balance *= 1.08m;
                            break;
                    }
                    Program.SaveClientsToJson(MainWindow.clientList);
                }
            }
            // Обновить отображение баланса, если необходимо
            // LabelBalance.Content = _balance.ToString("C");
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
                ListViewClients.ItemsSource = Program.FindByDepartmentID(clientList, this);//обновление отображаемого списка
            
            

        }

        private void CbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientList = Program.GetClientsInfoFromJson();
            ListViewClients.ItemsSource = Program.FindByDepartmentID(clientList, this);
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortstring = column.Tag.ToString();
            if ((CbDepartments.SelectedItem as Department) != null)
                ListViewClients.ItemsSource = Program.SortLogic(sortstring, clientList, this);
            
        }

        private void MenuItem_Click_Transfer(object sender, RoutedEventArgs e)
        {
            TransferWindow transWin = new TransferWindow(this);
            transWin.ShowDialog();
            if ((CbDepartments.SelectedItem as Department) != null)
                ListViewClients.ItemsSource = Program.FindByDepartmentID(clientList, this);//Обновление списка
        }

        private void DepositBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DepositeWindow DW = new DepositeWindow(this);
            DW.ShowDialog();
            if ((CbDepartments.SelectedItem as Department) != null)
                ListViewClients.ItemsSource = Program.FindByDepartmentID(clientList, this);//Обновление списка
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            Client client = Program.FindClientByPhoneNumber((ListViewClients.SelectedItem as Client).PhoneNumber, MainWindow.clientList);
            if (!(sender as MenuItem).IsChecked)//если не депозитный счет открыт
            {
                MessageBoxResult confirmResult = MessageBox.Show("Вы уверены, что хотите закрыть депозитный счет?", "Подтвердите действие!",
                    MessageBoxButton.YesNo);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    client.NonDepositMoney = null;
                    success = true;
                }
            }
            else//если не депозитный счет закрыт
            {
                MessageBoxResult confirmResult = MessageBox.Show("Вы уверены, что хотите открыть не депозитный счет?", "Подтвердите действие!",
                    MessageBoxButton.YesNo);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    client.NonDepositMoney = new AccountA();
                    success = true;
                }
            }
            if (success)
            {
                Program.SaveClientsToJson(MainWindow.clientList);
                this.ListViewClients.ItemsSource = Program.FindByDepartmentID(MainWindow.clientList, this);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            bool success = false;
            Client client = Program.FindClientByPhoneNumber((ListViewClients.SelectedItem as Client).PhoneNumber, MainWindow.clientList);
            if (!(sender as MenuItem).IsChecked)//если депозитный счет открыт
            {
                MessageBoxResult confirmResult = MessageBox.Show("Вы уверены, что хотите закрыть данный счет?", "Подтвердите действие!",
                    MessageBoxButton.YesNo);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    client.DepositMoney = null;
                    success=true;
                }
            }
            else//если не депозитный счет закрыт
            {
                MessageBoxResult confirmResult = MessageBox.Show("Вы уверены, что хотите открыть данный счет?", "Подтвердите действие!",
                    MessageBoxButton.YesNo);
                if (confirmResult == MessageBoxResult.Yes)
                {
                    client.DepositMoney = new AccountB();
                    success = true;
                }
            }
            if (success)
            {
                Program.SaveClientsToJson(MainWindow.clientList);
                this.ListViewClients.ItemsSource = Program.FindByDepartmentID(MainWindow.clientList, this);
            }
        }

        public void AccountControl()
        {

        }

        private void ListViewClients_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            Program.CheckTypeOfAccount(this);
        }
    }
}
