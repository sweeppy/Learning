using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalsDataBase.Models;

namespace AnimalsDataBase.ViewModel
{
    static class AnimalFactory
    {

        public static IAnimal GetAnimal(string animalClass,
                                        string animalSquad,
                                        string animalFamily,
                                        string animalGenus,
                                        string animalSpecies)
        {
            switch (animalClass)
            {
                case"amphibian":
                    return new Amphibian(animalClass, animalSquad, animalFamily, animalGenus, animalSpecies);
                case "bird":
                    return new Bird(animalClass, animalSquad, animalFamily, animalGenus, animalSpecies);
                case"mammal":
                    return new Mammal(animalClass, animalSquad, animalFamily, animalGenus, animalSpecies);
                default:
                    return new nullAnimal();
            }
        }
    }
}
