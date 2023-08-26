using Microsoft.AspNet.Identity.EntityFramework;
using NotebookWPFApi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookWPFApi.Interfaces
{
   public interface IAccess
    {
        /// <summary>
        /// Открытие функций доступных авторризованному пользователю, а также админу
        /// </summary>
        /// <param name="roleList"></param>
        /// <param name="main"></param>
        void GiveAccess(List<string> roleList, MainWindow main);
        /// <summary>
        /// Закрытие функций доступных авторизованному пользователю
        /// </summary>
        /// <param name="roleList"></param>
        /// <param name="main"></param>
        void BlockAccess(List<string> roleList, MainWindow main);

    }
}
