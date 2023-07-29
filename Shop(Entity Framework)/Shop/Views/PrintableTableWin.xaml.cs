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
using System.Data.Entity;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для PrintableTableWin.xaml
    /// </summary>
    public partial class PrintableTableWin : Window
    {
        public PrintableTableWin(string email)
        {
            InitializeComponent();
            
            PrintableDataGridTable.ItemsSource = Program.PurchasesContext.Purchases.Local.ToBindingList<Purchases>().Where(p => p.email == email);      
        }
        /// <summary>
        /// Отчищение dataGrid от прошлой информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    }
}
