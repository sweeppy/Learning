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
using System.Windows.Shapes;
using AnimalsDataBase.Models;
using AnimalsDataBase.ViewModel;

namespace AnimalsDataBase.View
{
    /// <summary>
    /// Логика взаимодействия для addBirdNoteWin.xaml
    /// </summary>
    public partial class addBirdNoteWin : Window
    {
        public Bird bird;
        public addBirdNoteWin()
        {
            InitializeComponent();
        }

        public addBirdNoteWin(MainWindow mainWin) : this()
        {
            CancelBtn.Click += delegate { this.DialogResult = false; };//Нажатие на кнопку Cancel

            ConfirmBtn.Click += delegate//нажатие на кнопку Confirm
            {

                bool succesInput = Checks.CheckNullInput(animalSquad_TB.Text,
                                                        animalFamily_TB.Text,
                                                        animalGenus_TB.Text,
                                                        animalSpecies_TB.Text);
                if (succesInput)
                {
                    bird = (Bird)AnimalFactory.GetAnimal("bird",
                                                        animalSquad_TB.Text,
                                                        animalFamily_TB.Text,
                                                        animalGenus_TB.Text,
                                                        animalSpecies_TB.Text);
                    this.DialogResult = true;
                }
                else//Если есть ошибки при вводе:
                {
                    this.DialogResult = false;
                    MessageBox.Show("Incorrect Input");
                }
            };
        }
    }


}
