using Notebook.Models;


namespace Notebook.Data
{
    public class Checks
    {
        /// <summary>
        /// Проверка на пустой ввод
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static bool CheckNullInput(Client client)
        {
            if (client.FirstName == null) return false;                                        /*errors += "\n Incorrect FirstName*";*/
            if (client.LastName == null) return false;                                         /*errors += "\n Incorrect LastName*";*/
            if (client.Patronymic == null) return false;                                       /*errors += "\n Incorrect Patronymic*";*/
            if (client.PhoneNumber == null) return false;                                      /*errors += "\n Incorrect PhoneNumber*";*/
            if (client.Address == null) return false;                                          /*errors += "\n Incorrect Address*";*/
            if (client.Description == null) return false;                                      /*errors += "\n Incorrect Description*";*/
            return true;
        }


    }
}
