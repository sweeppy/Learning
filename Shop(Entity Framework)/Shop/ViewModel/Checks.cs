using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class Checks
    {
        /// <summary>
        /// Проверка на пустой ввод
        /// </summary>
        /// <param name="checkClients"></param>
        /// <returns>редактируемый клиент</returns>
        public static bool CheckClient(Clients checkClients)
        {
            var cl = checkClients as Clients;
            if (cl.firstName == "" ||
                cl.lastName == "" ||
                cl.patronymic == "" ||
                cl.phoneNumber == "" ||
                cl.email == "")
            {
                return true;
            }
            else return false;
        }

        public static bool CheckPurchas(Purchases editPurchas)
        {
            var pur = editPurchas as Purchases;
            if (pur.email == "" ||
                pur.productName == "" ||
                pur.idProduct.ToString() == "")
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Если редактируется email, то этот метод редактирует email и у БД Purchases
        /// </summary>
        /// <param name="editClient"></param>
        public static void EditAllEmail(Clients editClient)
        {
            var oldClient = (Clients)Program.ClientsContext.Clients.AsNoTracking().FirstOrDefault(q => q.id == editClient.id);
            if ((oldClient as Clients).email != editClient.email)
            {
                foreach (var item in Program.PurchasesContext.Purchases.ToArray<Purchases>())
                {
                    if (item.email == oldClient.email)
                    {
                        item.email = editClient.email;
                    }
                }
            }
            Program.PurchasesContext.SaveChanges();
            Program.ClientsContext.SaveChanges();
        }

    }
}
