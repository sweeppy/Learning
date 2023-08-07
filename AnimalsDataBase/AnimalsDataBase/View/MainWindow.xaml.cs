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
using AnimalsDataBase.Models;
using System.Data.Entity;

namespace AnimalsDataBase.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Program.LoadFromDB();//Загрузка данных

            AmphibianDataGrid.ItemsSource = Program.animalContext.Amphibian.Local.ToBindingList<Amphibian>();   
            BirdDataGrid.ItemsSource = Program.animalContext.Bird.Local.ToBindingList<Bird>();               //Соединение с визуальной частью
            MammalDataGrid.ItemsSource = Program.animalContext.Mammal.Local.ToBindingList<Mammal>();
        }
        /// <summary>
        /// Добавление нового млекопитающего
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmphibianAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAmphibianNoteWin addAmphibianWin = new AddAmphibianNoteWin(this);
            addAmphibianWin.ShowDialog();

            if (addAmphibianWin.DialogResult.Value)
            {
                Program.animalContext.Amphibian.Add(addAmphibianWin.amphibian);//Добавление в БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveAmphibianToJson(Program.animalContext.Amphibian);//Сохранение в Json
            }
        }
        /// <summary>
        /// Удаление млекопитающего
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmphibianDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AmphibianDataGrid.SelectedItem != null)
            {
                Program.animalContext.Amphibian.Remove((AmphibianDataGrid.SelectedItem as Amphibian));//Удаление из БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveAmphibianToJson(Program.animalContext.Amphibian);//Сохранение в Json
            }
        }
        /// <summary>
        /// Добавление новой птицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirdAdd_Click(object sender, RoutedEventArgs e)
        {
            addBirdNoteWin addBirdWin = new addBirdNoteWin(this);
            addBirdWin.ShowDialog();

            if (addBirdWin.DialogResult.Value)
            {
                Program.animalContext.Bird.Add(addBirdWin.bird);//Добавление в БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveBirdToJson(Program.animalContext.Bird);//Сохранение в Json
            }
        }
        /// <summary>
        /// Удаление птицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BirdDataGrid.SelectedItem != null)
            {
                Program.animalContext.Bird.Remove((BirdDataGrid.SelectedItem as Bird));//Удаление из БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveBirdToJson(Program.animalContext.Bird);//Сохранение в Json
            }
        }
        /// <summary>
        /// Добавление нового земноводного
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MammalAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMammalNote addMammal = new AddMammalNote(this);
            addMammal.ShowDialog();

            if (addMammal.DialogResult.Value)
            {
                Program.animalContext.Mammal.Add(addMammal.mammal);//Добавление в БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveMammalToJson(Program.animalContext.Mammal);//Сохранение в Json
            }

        }
        /// <summary>
        /// Удаление земноводных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MammalDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MammalDataGrid.SelectedItem != null)
            {
                Program.animalContext.Mammal.Remove(MammalDataGrid.SelectedItem as Mammal);//Удаление из БД
                Program.animalContext.SaveChanges();//Сохранение в БД
                SaveAnimalsToJson.SaveMammalToJson(Program.animalContext.Mammal);//Сохранение в Json
            }
        }

        private void AmphibiansEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editingAmphibian = AmphibianDataGrid.SelectedItem as Amphibian;
            if (editingAmphibian == null) return;
            if (Checks.CheckAmphibianNullEdit(editingAmphibian))
            {
                Program.animalContext.SaveChanges();
                SaveAnimalsToJson.SaveAmphibianToJson(Program.animalContext.Amphibian);
            }
            else
            {
                MessageBox.Show("Cells mustn't be empty");
                Program.animalContext.Entry(editingAmphibian).Reload();
            }
        }

        private void BirdsEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editingBird = BirdDataGrid.SelectedItem as Bird;
            if (editingBird == null) return;
            if (Checks.CheckBirdNullEdit(editingBird))
            {
                Program.animalContext.SaveChanges();
                SaveAnimalsToJson.SaveBirdToJson(Program.animalContext.Bird);
            }
            else
            {
                MessageBox.Show("Cells mustn't be empty");
                Program.animalContext.Entry(editingBird).Reload();
            }
        }

        private void MammalsEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editingMammal = MammalDataGrid.SelectedItem as Mammal;
            if (editingMammal == null) return;
            if (Checks.CheckMammalNullEdit(editingMammal))
            {
                Program.animalContext.SaveChanges();
                SaveAnimalsToJson.SaveMammalToJson(Program.animalContext.Mammal);
            }
            else
            {
                MessageBox.Show("Cells mustn't be empty");
                Program.animalContext.Entry(editingMammal).Reload();
            }
        }
    }
}
