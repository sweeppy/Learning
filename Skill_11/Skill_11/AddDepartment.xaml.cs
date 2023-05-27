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

namespace Skill_11
{
    /// <summary>
    /// Логика взаимодействия для AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        List<string> errorStrList = new List<string>(); //лист ошибок

        Repository repository = new Repository();

        private ManagerPage _managerWin; //страница ManagerPage
        public AddDepartment(ManagerPage managerWin)
        {
            InitializeComponent();
            _managerWin = managerWin;
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (sender is Window w) w.Hide();
        }
        /// <summary>
        /// Подтверждение добавления нового департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Finish_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (successOfCreateNewDepartment(_managerWin.departmentList))//Если прошло проверку, то добавление нового департамента в список
            {
                _managerWin.departmentList.Add(new Department(TbNameOfDepartment.Text, Convert.ToInt32(TbIdDepartment.Text)));//добавление
                repository.SaveDepartmentsToJson(_managerWin.departmentList);//сохранение списка в json

                _managerWin.CbDepartments.Items.Refresh();//обновление визуальной части
                this.Close();
            }
            else//Если не прошло проверку, вывод списка ошибок:
            {
                string temp = "";
                foreach (var item in errorStrList)
                {
                    temp += $"\n{item}";
                }
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"Error. {temp}");
                errorStrList.Clear();
            }

        }
        /// <summary>
        /// Проверка ввода данных нового департамента
        /// </summary>
        /// <param name="departments"></param>
        /// <returns>True - в ведённых данных нет ошибок, False - есть</returns>
        private bool successOfCreateNewDepartment(List<Department> departments)
        {

            bool success = false;//Общее подтверждение

            bool successOfNameDepartment = false;
            bool successOfIdDepartment = int.TryParse(TbIdDepartment.Text, out var IdDepartment);//проверка ID деапртамента
            foreach (var item in departments)//Проверка на то, что департамента с введённым ID нет или введён ID, которые содержатся по умолчанию
            {
                if (item.DepartamentID == IdDepartment || IdDepartment == 0 || IdDepartment == 1)
                {
                    successOfIdDepartment = false;
                }
            }

            if (TbNameOfDepartment.Text != "")//проверка на пустое наименование
               successOfNameDepartment = true;
            
            if (successOfIdDepartment && successOfNameDepartment)//если нет ошибок, подтверждение
                success = true;

            else//Добавление ошибок в лист, если они есть
            {
                if (!successOfIdDepartment)
                    errorStrList.Add("Введен неправльноый ID департамента(ID (0) и (1) заняты по умолчанию)");
                if (!successOfNameDepartment)
                    errorStrList.Add("Введено некорректное имя департамента");
            }
            return success;
        }
    }
}
