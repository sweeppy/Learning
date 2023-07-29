using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Data.Entity;

namespace Shop
{
    class Program
    {
        public static MSSQLShopDBEntities ClientsContext;
        public static MSSQLPurchasesDBEntities2 PurchasesContext;
        /// <summary>
        /// Выполняется при запуске программы(загрузка данных)
        /// </summary>
        public static void SartProgram()
        {
            
            ClientsContext = new MSSQLShopDBEntities();
            ClientsContext.Clients.Load();

            
            PurchasesContext = new MSSQLPurchasesDBEntities2();
            PurchasesContext.Purchases.Load();
        }

    }
   

}
