using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution
{
    /// <summary>
    /// Разбивает введенную строку на слова
    /// </summary>
    /// <param name="str">вводимая строчка пользователем</param>
    /// <returns>массив слов из строки</returns>
    static string[] SplitText(string str)
    {
        string[] splitStr = str.Split(" ");
        return splitStr;

    }
    /// <summary>
    /// Выводит строковый массив по элементам с новой строки
    /// </summary>
    /// <param name="splitedStr">входной строковый массив</param>
    static void OutputSplitedStr(string[] splitedStr)
    {
        foreach (var item in splitedStr)
        {
            Console.WriteLine(item);
        }
    }


    static void Main()
    {
        Console.WriteLine($"Введите предложение:");

        string takenStr = Console.ReadLine(); /*Ввод предложения пользователем*/

        OutputSplitedStr(SplitText(takenStr)); /*выполнение методов*/

        Console.WriteLine();
        Console.ReadKey();

    }

}
