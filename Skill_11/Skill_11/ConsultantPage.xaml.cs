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
    /// Логика взаимодействия для ConsultantPage.xaml
    /// </summary>
    public partial class ConsultantPage : Page
    {
        Repository repository = new Repository();
        Employer consultant = new Consultant();

        public List<Department> departmentList = new List<Department>();
        public List<Client> clientList = new List<Client>();

        public ContextMenu contextMenu;
        public ConsultantPage()
        {
            InitializeComponent();
            clientList = repository.GetClientsInfoFromJson();
            departmentList = repository.GetDepartmentsInfoFromJson();
            CbDepartments.ItemsSource = departmentList;
        }
        /// <summary>
        /// распределение клиентов по их департаментам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientList = repository.GetClientsInfoFromJson();
            ListViewClients.ItemsSource = Find(clientList);
        }

        /// <summary>
        /// Формирование листа клиентов по DepartmentID в соответсвии с выбраннным департаментом
        /// </summary>
        /// <returns>Лист клиентов</returns>
        public List<Client> Find(List<Client> clientList)
        {
            List<Client> tempList = new List<Client>();
            if (CbDepartments.SelectedItem != null)
            {
                foreach (var item in clientList)
                {
                    if ((CbDepartments.SelectedItem as Department).DepartamentID == item.DepartmentID)
                    {
                        tempList.Add(item);
                    }
                }
            }

            return tempList;
        }
        /// <summary>
        /// Открытие окна для изменения данных клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChangeInfoWindow changeInfoWindow = new ChangeInfoWindow(this);
            changeInfoWindow.ShowDialog();

        }

        /// <summary>
        /// Лоигка сортировки клиентов
        /// </summary>
        /// <param name="criterionstr">Критерии сортировки</param>
        private void SortLogic(string criterionstr)
        {
            List<Client> nowclientList = new List<Client>();
            nowclientList = Find(clientList);
            switch (criterionstr)
            {
                case "FirstName":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.FirstName)); //сортировка по имени
                    break;
                case "LastName":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.LastName)); //сортировка по фамилии
                    break;
                case "Patronymic":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.Patronymic)); //сортировка по отчеству
                    break;
                case "PhoneNumber":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PhoneNumber)); //сортировка по номеру телефона
                    break;
                case "PassportSeries":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PassportSeries)); //сортировка по серии паспорта
                    break;
                case "PassportID":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.PassportID)); //сортировка по номеру паспорта
                    break;
                case "DateOfChange":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.DateOfChange)); //сортировка по дате изменения
                    break;
                case "InformationThatChanged":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.InformationThatChanged)); //сортировка по информации, которую изменили
                    break;
                case "WhoChanged":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.WhoChanged));  //сортировка по тому, кто изменил
                    break;
                case "TypeOfChanging":
                    nowclientList.Sort(Employer.SortedBy(Employer.SortedCriterion.TypeOfChanging)); //сортировка по типу изменения
                    break;
            }
            if ((CbDepartments.SelectedItem as Department) != null)
                ListViewClients.ItemsSource = nowclientList;
        }
        /// <summary>
        /// Нажав на выбранную колонку происходит СОРТИРОВКА
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortstring = column.Tag.ToString();
            SortLogic(sortstring);
        }
    }
}
