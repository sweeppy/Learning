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
using AnimalsDataBase.ViewModel;
using AnimalsDataBase.Models;

namespace AnimalsDataBase.View
{
    /// <summary>
    /// Логика взаимодействия для AddAmphibianNoteWin.xaml
    /// </summary>
    public partial class AddAmphibianNoteWin : Window
    {
        public Amphibian amphibian;
        public AddAmphibianNoteWin()
        {
            InitializeComponent();
        }
        
        public AddAmphibianNoteWin(MainWindow mainWin) : this()
        {
            CancelBtn.Click += delegate { this.DialogResult = false; };//Нажатие на кнопку Cancel

            ConfirmBtn.Click += delegate//Нажатие на кнопку Confirm
            {
                
                bool succesInput = Checks.CheckNullInput(animalSquad_TB.Text,
                                                        animalFamily_TB.Text,
                                                        animalGenus_TB.Text,
                                                        animalSpecies_TB.Text);
                if (succesInput)
                {
                    amphibian = (Amphibian)AnimalFactory.GetAnimal("amphibian",
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
