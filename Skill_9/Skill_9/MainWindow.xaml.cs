using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Skill_9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Заполнение листбокса словами введеного предложения, где каждое слово выводиться с новой строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstCommandButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<string> listOfSplitedWords = new List<string>();
            listOfSplitedWords= SentenceRevers(SplitText(InputText.Text));
            listOfSplitedSentence.ItemsSource = listOfSplitedWords;
        }
        

        private void SecondCommandButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in ReverseSplitedText(SplitText(InputText.Text)))
            {
                labelOfSplitedSenence.Content +=$"{item}\n";
            }
        }

        /// <summary>
        /// Разбивает введенную строку на слова(строковый массив)
        /// </summary>
        /// <param name="str">строка</param>
        /// <returns>строковый массив</returns>
        static string[] SplitText(string str)
        {
            string[] splitStr = str.Split(' ');
            return splitStr;

        }
        /// <summary>
        /// Выводит строковый массив по элементам с новой строки в обратном порядке
        /// </summary>
        /// <param name="splitedStr">строковый массив</param>
        static List<string> SentenceRevers(string[] splitedStr)
        {
            List<string> tempList = new List<string>();
            foreach (var item in splitedStr)
            {
                tempList.Add(item);
            }
            return tempList;
        }
        static List<string> ReverseSplitedText(string[] splitedStr)
        {
            List<string> tempList = new List<string>();
            for (int i = splitedStr.Length - 1; i >= 0; i--)
            {
                tempList.Add(splitedStr[i]);
            }
            return tempList;
        }
    }
}
