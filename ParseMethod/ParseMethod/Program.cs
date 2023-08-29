using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseMethod
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string[] returnMessage;

            double returnValue = 0;

            if (args.Length != 0)
                Console.WriteLine(args[0]);
            else
                Console.WriteLine("Введите число:");



            
            string number = Console.ReadLine();//Ввод числа пользователем

            bool check = true;
            char[] rightChars = new char[11];
            if (!rightChars.Contains('1'))
            {
                rightChars = new char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'};
            }
            foreach (var item in number)//проверка на то, что введены числа
            {
                if (!rightChars.Any(e => e == item))
                {
                    check = false;
                    break;
                }
            }

            if (!check || number == "")//если введены не числа или пустая строка
            {
                number = null;
                returnMessage = new string[] { "Введено некорректное число, повторите попытку:" };
                Main(returnMessage);
            }
            else
            {
                int countBeforeDot = 0;
                int countAfterDot = 0;
                bool dot = number.Contains('.');
                if(dot)
                {
                    foreach (var item in number)
                    {
                        if (item == '.')
                            break;

                            countBeforeDot++;
                    }
                    foreach (var item in number)
                    {
                        if (item == '.')
                        {
                            countAfterDot = 0;
                        }
                        countAfterDot++;
                    }
                }

                int[] ConvertedNumbersAfterDot;
                if (countAfterDot > 1)
                {
                    ConvertedNumbersAfterDot = new int[countAfterDot - 1];
                }
                else
                {
                    ConvertedNumbersAfterDot = new int[1];
                }

                int[] ConvertedNumbersBeforeDot = new int[countBeforeDot];
                

                int[] ConvertedNumbersWithoutDot = new int[number.Length];

                int[] trueNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                if(!dot)//число без точки
                {
                        for (int i = 0; i < number.Length; i++)
                        {
                            for (int j = 0; j < trueNumbers.Length; j++)
                            {
                                if (trueNumbers[j].ToString() == number[i].ToString())
                                    ConvertedNumbersWithoutDot[i] = trueNumbers[j];
                            }
                        }
                }
                else//число с точкой
                {
                    for (int i = 0; i < countBeforeDot; i++)
                    {
                        for (int j = 0; j < trueNumbers.Length; j++)
                        {
                            if (trueNumbers[j].ToString() == number[i].ToString())
                                ConvertedNumbersBeforeDot[i] = trueNumbers[j];
                        }
                    }

                    for (int i = 0; i < countAfterDot - 1; i++)
                    {
                        for (int j = 0; j < trueNumbers.Length; j++)
                        {
                            if (trueNumbers[j].ToString() == number[i + countBeforeDot + 1].ToString())
                            {
                                ConvertedNumbersAfterDot[i] = trueNumbers[j];
                                j = trueNumbers.Length;
                            }
                                
                        }
                    }
                }
                int flag = 0;
                if (!dot)
                {
                    while (flag < ConvertedNumbersWithoutDot.Length)
                    {
                        try
                        {
                            returnValue += ConvertedNumbersWithoutDot[flag] * Math.Pow(10, (ConvertedNumbersWithoutDot.Length - flag - 1));
                            flag++;
                        }
                        catch (Exception)//слишом большое число
                        {
                            returnMessage = new string[] { "Число слишком большое повторите попытку:" };
                            Main(returnMessage);

                        }

                    }
                }
                else
                {
                    while (flag < ConvertedNumbersBeforeDot.Length)
                    {
                        try
                        {
                            returnValue += ConvertedNumbersBeforeDot[flag] *
                            Math.Pow(10, (ConvertedNumbersBeforeDot.Length - flag - 1));

                            flag++;
                        }
                        catch (Exception)//слишом большое число
                        {
                            returnMessage = new string[] { "Число слишком большое повторите попытку:" };
                            Main(returnMessage);
                        }

                    }
                    flag = 0;
                    while (flag < ConvertedNumbersAfterDot.Length)
                    {
                        try
                        {
                            returnValue += ConvertedNumbersAfterDot[flag] *
                            Math.Pow(10, -(flag + 1));

                            flag++;
                        }
                        catch (Exception)//слишом большое число
                        {
                            returnMessage = new string[] { "Число слишком большое повторите попытку:" };
                            Main(returnMessage);
                        }

                    }
                }




                Console.WriteLine(returnValue);

                Console.ReadKey();

            }
        }

    }
}
