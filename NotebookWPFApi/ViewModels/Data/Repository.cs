using Microsoft.AspNet.Identity.EntityFramework;
using NotebookWPFApi.Data;
using NotebookWPFApi.Interfaces;
using NotebookWPFApi.Models;
using NotebookWPFApi.Views;
using System.Collections.Generic;
using System.Windows;

namespace NotebookWPFApi.ViewModels.Data
{
    internal class Repository : IAccess
    {
        public void GiveAccess(List<string> roleList,  MainWindow main)
        {
            main.LogOutBtn.Visibility = Visibility.Visible;
            main.AddClientBtn.Visibility = Visibility.Visible;

            if (roleList.Contains(Roles.admin.Name))//привилегии админа 
            {
                main.fullContextMenu.Visibility = Visibility.Visible;//Открытие функции удаления пользователя
                main.shortContextMenu.Visibility = Visibility.Visible;

                main.fullfirstNameTC.IsReadOnly = false;//досутп к редактированию в полной информации о клиентах
                main.fulllastNameTC.IsReadOnly = false;
                main.fullpatronymicTC.IsReadOnly = false;
                main.fullphoneNumberTC.IsReadOnly = false;
                main.fulladdressTC.IsReadOnly = false;
                main.fulldescriptionTc.IsReadOnly = false;
            }
        }

        public void BlockAccess(List<string> roleList, MainWindow main)
        {
            main.LogOutBtn.Visibility = Visibility.Hidden;
            main.AddClientBtn.Visibility = Visibility.Hidden;

            if (roleList.Contains(Roles.admin.Name))//привилегии админа 
            {
                main.fullContextMenu.Visibility = Visibility.Hidden;//Закрытите функции удаления пользователя
                main.shortContextMenu.Visibility = Visibility.Hidden;

                main.fullfirstNameTC.IsReadOnly = true;//закрытие доступа к редактированию в полной информации о клиентах
                main.fulllastNameTC.IsReadOnly = true;
                main.fullpatronymicTC.IsReadOnly = true;
                main.fullphoneNumberTC.IsReadOnly = true;
                main.fulladdressTC.IsReadOnly = true;
                main.fulldescriptionTc.IsReadOnly = true;
            }

        }

        public void Update(MainWindow main, DataApi _dataApi)
        {
            main.shortphoneNumberTC.Visibility = Visibility.Hidden;
            main.shortaddressTC.Visibility = Visibility.Hidden;
            main.shortDescriptionTc.Visibility = Visibility.Hidden;
            main.shortGetAllInfDGTM.Visibility = Visibility.Visible;

            main.fullDataGrid.ItemsSource = _dataApi.GetClients();
            main.shortDataGrid.ItemsSource = _dataApi.GetClients();
        }
    }
}
