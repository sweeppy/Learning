using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram_ver2
{
     class Program
    {
        static void Main()
        {
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
                    new Manager().MainLogic();
                    break;
                case 2:
                    new Consultant().Main();
                    break;
            }

        }
    }
}
