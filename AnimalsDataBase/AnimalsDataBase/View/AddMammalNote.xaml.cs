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

namespace AnimalsDataBase.View
{
    /// <summary>
    /// Логика взаимодействия для AddMammalNote.xaml
    /// </summary>
    public partial class AddMammalNote : Window
    {
        public Mammal mammal;
        public AddMammalNote()
        {
            InitializeComponent();
        }

        public AddMammalNote(MainWindow mainWindow) : this()
        {
            CancelBtn.Click += delegate { this.DialogResult = false; };

            ConfirmBtn.Click += delegate
            {
                bool successInput = Checks.CheckNullInput(animalFamily_TB.Text, animalGenus_TB.Text, animalSpecies_TB.Text, animalSquad_TB.Text);

                if (successInput)
                {
                    mammal = (Mammal)AnimalsDataBase.ViewModel.AnimalFactory.GetAnimal("mammal",
                                                                               animalSquad_TB.Text,
                                                                               animalFamily_TB.Text,
                                                                               animalGenus_TB.Text,
                                                                               animalSpecies_TB.Text);
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                    MessageBox.Show("Wrong input");
                }
                
            };
        }
    }
}
