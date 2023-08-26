using Microsoft.AspNet.Identity;
using NotebookWPFApi.Models;
using NotebookWPFApi.Views;
using NotebookWPFApi.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

namespace NotebookWPFApi.ViewModels
{
    public class Checks
    {
        private UserManager<User> _userManager;
        public Checks()
        {

        }

        /// <summary>
        /// Проверка на пустой ввод
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static bool CheckNullInputOfClient(Client client)
        {
            if (client.FirstName == "") return false;

            if (client.LastName == "") return false;

            if (client.Patronymic == "") return false;

            if (client.PhoneNumber == "" || client.PhoneNumber.Length != 11
                || !ulong.TryParse(client.PhoneNumber, out var _)) return false;

            if (client.Address == "") return false;

            if (client.Description == "") return false;
            return true;
        }
        /// <summary>
        /// Проверка ввода данных для входа
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckforSignIn(string login, string password)
        {
            bool result = true;

            if (login == "" || password == "" ||
                login == "Write your login..." || password == "Write your password...") return false;//проверка на пустой ввод

            foreach (var item in MainWindow.notebookContext.AspNetUsers)
            {
                if (item.UserName == login)//проверка на существование логина
                {
                    result = true;
                    break;
                }
                else result = false;
            }



            return result;

        }
        /// <summary>
        /// Проверка надежности пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsPasswordStrong(string password)
        {
            // минимальная длина пароля
            const int minLength = 8;

            // проверка на минимальную длину
            if (password.Length < minLength) return false;

            // проверка на наличие чисел
            if (!password.Any(char.IsDigit)) return false;

            // проверка на наличие строчных букв
            if (!password.Any(char.IsLower)) return false;

            // проверка на наличие заглавных букв
            if (!password.Any(char.IsUpper)) return false;

            // проверка на наличие не буквенно-цифровых символов
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;

            return true;
        }
    }
}
