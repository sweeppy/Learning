using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.Data.Entity;
namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Program.SartProgram();

            mainDataGrid.ItemsSource = Program.ClientsContext.Clients.Local.ToBindingList<Clients>();
            SecondDataGrid.ItemsSource = Program.PurchasesContext.Purchases.Local.ToBindingList<Purchases>();
        }
        /// <summary>
        /// Изменение информации клиента (нажатие на enter)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Clients editClient = (Clients)mainDataGrid.SelectedItem;
            if (editClient == null)
            {
                return;
            }


            if (Checks.CheckClient(editClient))//проверка на пустой ввод
            {
                MessageBox.Show("Cell can't be empty");
                Program.ClientsContext.Entry(editClient).Reload();
            }
            else
            {
                if (!editClient.ToString().Contains('@'))//проверка при редактировании email
                {
                    MessageBox.Show("Wrong email");
                    Program.ClientsContext.Entry(editClient).Reload();
                }
                else if ((editClient as Clients).phoneNumber.Length != 11 ||
                    Int32.TryParse((editClient as Clients).phoneNumber, out _))//проверка номера телефона
                {
                    MessageBox.Show("Wrong phoneNumber");
                    Program.ClientsContext.Entry(editClient).Reload();
                }
                else if (Program.ClientsContext.Clients.Any(t => t.email == editClient.email))
                {
                    MessageBox.Show("Еhis email is already registered");
                    Program.ClientsContext.Entry(editClient).Reload();
                }
                else//Обновление данных
                {
                    Checks.EditAllEmail(editClient);//Замена email и у покупок
                    SecondDataGrid.Items.Refresh();
                }
            }

        }
        private void SecondDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editPurchas = SecondDataGrid.SelectedItem as Purchases;
            if (editPurchas == null) return;
            if (Checks.CheckPurchas(editPurchas))//проверка на пустой ввод
            {
                MessageBox.Show("Cell can't be empty");
                return;
            }

            bool success = false;
            if (Program.ClientsContext.Clients.Any(cl => cl.email == editPurchas.email))
            {
                success = true;
            }

            if (!success)
            {
                MessageBox.Show("Еhis email is not found");
                Program.PurchasesContext.Entry(editPurchas).Reload();
            }
            else Program.PurchasesContext.SaveChanges();

        }


        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddNote_Click(object sender, RoutedEventArgs e)
        {
            
            AddClient addClientWindow = new AddClient(this);
            addClientWindow.ShowDialog();

            try
            {
                if (addClientWindow.DialogResult.Value)//проверка на то, какая кнопка нажата(confirm/cancel)
                {
                    Program.ClientsContext.Clients.Add(addClientWindow.newClient);
                    Program.ClientsContext.SaveChanges();
                }
            }
            catch (Exception)//Проверка ункальности email
            {
                
                Program.ClientsContext.Clients.Local.Remove(addClientWindow.newClient);
                MessageBox.Show("Еhis email is already registered");
            }

        }
        /// <summary>
        /// Удаляет запись о выбранном клиенте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mainDataGrid.SelectedItem != null)
            {
                Program.ClientsContext.Clients.Remove(mainDataGrid.SelectedItem as Clients);
                Program.ClientsContext.SaveChanges();
            }
        }
        /// <summary>
        /// Удаляет выбранную покупку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteFromPurchases_Click(object sender, RoutedEventArgs e)
        {
            if (SecondDataGrid.SelectedItem != null)
            {
                Program.PurchasesContext.Purchases.Remove(SecondDataGrid.SelectedItem as Purchases);
                Program.PurchasesContext.SaveChanges();
            }
        }
        /// <summary>
        /// Добавляет новую покупку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddNoteToPurchases_Click(object sender, RoutedEventArgs e)
        {
            AddPurchasWin add = new AddPurchasWin();
            add.ShowDialog();
            

            if (add.DialogResult.Value)
            {
                Program.PurchasesContext.Purchases.Add(add.purchas);
                Program.PurchasesContext.SaveChanges();
            }

        }
        /// <summary>
        /// Открывает список покупок выбранного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPurchases_Click(object sender, RoutedEventArgs e)
        {
            var client = mainDataGrid.SelectedItem as Clients;
            PrintableTableWin PTW = new PrintableTableWin(client.email);// передаем email клиента в новое окно
            PTW.ShowDialog();
        }
        /// <summary>
        /// Чтобы не залипало меню(TabControl) во время редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!Main.IsSelected)
            {
                Main.IsSelected = true;
            }
        }
        /// <summary>
        /// Чтобы не залипало меню(TabControl) во время редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Second_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!Second.IsSelected)
            {
                Second.IsSelected = true;
            }
        }
    }
}
