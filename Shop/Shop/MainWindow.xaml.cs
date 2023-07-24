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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DataRowView row;


        public MainWindow()
        {
            InitializeComponent();
            _ = Program.StartConnection();
            mainDataGrid.DataContext = Program.DTMain.DefaultView;
            SecondDataGrid.DataContext = Program.DTSecond.DefaultView;
        }
        /// <summary>
        /// Изменение информации клиента (нажатие на enter)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (DataRowView)mainDataGrid.SelectedItem;
            if (row == null)
            {
                return;
            }


            if (row.Row.ItemArray.Contains(""))//проверка на пустой ввод
            {
                MessageBox.Show("Cell can't be empty");
                Program.sqlDAMain.Update(Program.DTMain);
                Program.DTMain.RejectChanges();
                mainDataGrid.DataContext = Program.DTMain.DefaultView;
            }
            else
            {
                try
                {
                    if (!row["email"].ToString().Contains('@'))//проверка при редактировании email
                    {
                        MessageBox.Show("Wrong email");
                        Program.DTMain.RejectChanges();
                        mainDataGrid.DataContext = Program.DTMain.DefaultView;
                    }
                    else if (row["phoneNumber"].ToString().Length != 11||
                        Int32.TryParse(row["phoneNumber"].ToString(), out _))//проверка номера телефона
                    {
                        MessageBox.Show("Wrong phoneNumber");
                        Program.DTMain.RejectChanges();
                        mainDataGrid.DataContext = Program.DTMain.DefaultView;
                    }
                    else//Обновление данных
                    {
                        var dataRow = row.Row;

                        var id = (int)dataRow[0];
                        var firstName = (string)dataRow[1];
                        var lastName = (string)dataRow[2];
                        var patronymic = (string)dataRow[3];
                        var phoneNumber = (string)dataRow[4];
                        var email = (string)dataRow[5];
                        Program.UpdateShopDB(id, firstName, lastName, patronymic, phoneNumber, email);
                    }
                }
                catch (System.Data.SqlClient.SqlException)//Проверка уникальности email
                {
                    MessageBox.Show("Еhis email is already registered");
                    Program.DTMain.RejectChanges();
                    mainDataGrid.DataContext = Program.DTMain.DefaultView;
                }
            }
            
        }
        private void SecondDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            row = (DataRowView)SecondDataGrid.SelectedItem;
            if (row == null) return;
            if (row.Row.ItemArray.Contains(""))//проверка на пустой ввод
            {
                MessageBox.Show("Cell can't be empty");
                Program.DTSecond.RejectChanges();
                SecondDataGrid.DataContext = Program.DTSecond.DefaultView;
            }

            bool success = false;
            for (int i = 0; i < Program.DTMain.Rows.Count; i++)
            {
                if (Program.DTMain.Rows[i][5].ToString() == row["email"].ToString())
                {
                    var dataRow = row.Row;

                    var id = (int)dataRow[0];
                    var email = (string)dataRow[1];
                    var idProduct = (int)dataRow[2];
                    var productName = (string)dataRow[3];

                    Program.UpdatePurchasesDB(id, email, idProduct, productName);//Обновление данных
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                MessageBox.Show("Еhis email is not found");
                Program.DTSecond.RejectChanges();
                SecondDataGrid.DataContext = Program.DTSecond.DefaultView;
            }

        }


        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddNote_Click(object sender, RoutedEventArgs e)
        {
            DataRow r = Program.DTMain.NewRow();
            AddClient addClientWindow = new AddClient(r, this);
            addClientWindow.ShowDialog();

            try
            {
                if (addClientWindow.DialogResult.Value)//проверка на то, какая кнопка нажата(confirm/cancel)
                {
                    Program.DTMain.Rows.Add(r);
                    Program.sqlDAMain.Update(Program.DTMain);
                    mainDataGrid.DataContext = Program.DTMain.DefaultView;
                }
            }
            catch (Exception)//Проверка ункальности email
            {
                Program.DTMain.Rows.Remove(r);
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
            row = (DataRowView)mainDataGrid.SelectedItem;
            row.Row.Delete();
            Program.sqlDAMain.Update(Program.DTMain);
        }
        /// <summary>
        /// Удаляет выбранную покупку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteFromPurchases_Click(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)SecondDataGrid.SelectedItem;
            row.Row.Delete();
            Program.sqlDASecond.Update(Program.DTSecond);
        }
        /// <summary>
        /// Добавляет новую покупку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddNoteToPurchases_Click(object sender, RoutedEventArgs e)
        {
            DataRow r = Program.DTSecond.NewRow();
            AddPurchasWin add = new AddPurchasWin(r);
            add.ShowDialog();
            bool success = false;
            for (int i = 0; i < Program.DTMain.Rows.Count; i++)
            {
                if (Program.DTMain.Rows[i][5].ToString() == r["email"].ToString())
                {
                    success = true;
                    break;
                }
            }

            if (success && add.DialogResult.Value)
            {
                    Program.DTSecond.Rows.Add(r);
                    Program.sqlDASecond.Update(Program.DTSecond);

                    SecondDataGrid.DataContext = Program.DTSecond.DefaultView;
            }
            else if (add.DialogResult.Value)
            {
                MessageBox.Show("Client with given email was not found");
            }
            
        }
        /// <summary>
        /// Открывает список покупок выбранного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPurchases_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataRowView)mainDataGrid.SelectedItem;
            var client = row.Row.ItemArray;//получаем данные о выбранном клиенте
            PrintableTableWin PTW = new PrintableTableWin(client[5].ToString());// передаем email клиента в новое окно
            PTW.ShowDialog();
        }
        /// <summary>
        /// Чтобы не залипало меню во время редактирования
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
        /// Чтобы не залипало меню во время редактирования
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _lastName;
        public string lastName

        {
            get { return _lastName; }

            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    Program.sqlDAMain.Update(Program.DTMain);
                }
            }
        }
    }
}
