using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution
{
    /// <summary>
    /// Разбивает введенную строку на слова(строковый массив)
    /// </summary>
    /// <param name="str">строка</param>
    /// <returns>строковый массив</returns>
    static string[] SplitText(string str)
    {
        string[] splitStr = str.Split(" ");
        return splitStr;

    }
    /// <summary>
    /// Выводит строковый массив по элементам с новой строки в обратном порядке
    /// </summary>
    /// <param name="splitedStr">строковый массив</param>
    static void SentenceRevers(string[] splitedStr)
    {
        for (int i = splitedStr.Length - 1; i >= 0; i--)
        {
            Console.WriteLine(splitedStr[i]);
        }
    }


    static void Main()
    {
        Console.WriteLine($"Введите предложение:");

        string takenStr = Console.ReadLine(); /*Ввод предложения пользователем*/

        SentenceRevers((SplitText(takenStr))); /*выполнение методов*/

        Console.WriteLine();
        Console.ReadKey();

    }

}
