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

namespace Skill_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManagerPage managerPage;
        ConsultantPage consultantPage;
        Employer manager = new Manager();
        List<Client> clients = new List<Client>();
        public MainWindow()
        {
            InitializeComponent();
            managerPage = new ManagerPage();
            consultantPage = new ConsultantPage();
        }

        private void Manager_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = managerPage; //вход менеджера
        }

        private void ConsultantButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = consultantPage; //вход консультанта
        }


    }
}
