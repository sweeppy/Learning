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
using System.IO;
using Newtonsoft.Json;
using MainLogic;

namespace Skill_12
{
    /// <summary>
    /// Логика взаимодействия для ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private MainWindow _mainWindow;
        
        public ActivityWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            Program.GetOperationsFromJson();
            LbActivity.ItemsSource = Operation.OperationList;
        }

    }
    
}
