using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver3
{
    class Program
    {
        static void Main()
        {
            string whoChanging;
            Console.WriteLine("От имени какого лица хотите войти:" +
                "\n 1)Менеджер" +
                "\n 2)Консультант");


            bool success = int.TryParse(Console.ReadLine(), out var number);
            while (!success || number < 1 || number > 2)
            {
                Console.WriteLine("Введено некорректное число, повторите попытку:");
                success = int.TryParse(Console.ReadLine(), out number);
            }
            switch (number)
            {
                case 1:
                    whoChanging = "Менеджером";             //Вход для менеджера
                    //new Manager().MainLogic(whoChanging);
                    Employer manager = new Manager();
                    manager.Main(whoChanging);
                    break;
                case 2:
                    whoChanging = "Консультантом";          //Вход для консультанта
                    //new Consultant().Main(whoChanging);
                    Employer consultant = new Consultant();
                    consultant.Main(whoChanging);
                    break;
            }

        }
    }
}