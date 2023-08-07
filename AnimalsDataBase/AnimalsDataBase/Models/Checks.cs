using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsDataBase.Models
{
    static class Checks
    {
        public static bool CheckNullInput(string value1, string value2, string value3, string value4)
        {
            bool result = true;
            List<string> valuesForCheck = new List<string>() { value1, value2, value3,value4 };
            foreach (var item in valuesForCheck)
            {
                if (item == "")
                {
                    result = false;
                }
            }
            return result;
        }

        public static bool CheckAmphibianNullEdit(Amphibian editingAmphibian)
        {
            if (editingAmphibian.animalFamily == "" ||
                editingAmphibian.animalGenus == "" ||
                editingAmphibian.animalSquad == "" ||
                editingAmphibian.animalSpecies == "")
            {
                return false;
            }
            else return true;
        }

        public static bool CheckBirdNullEdit(Bird editingBird)
        {
            if (editingBird.animalFamily == "" ||
                editingBird.animalGenus == "" ||
                editingBird.animalSquad == "" ||
                editingBird.animalSpecies == "")
            {
                return false;
            }
            else return true;
        }

        public static bool CheckMammalNullEdit(Mammal editingMammal)
        {
            if (editingMammal.animalFamily == "" ||
                editingMammal.animalGenus == "" ||
                editingMammal.animalSquad == "" ||
                editingMammal.animalSpecies == "")
            {
                return false;
            }
            else return true;
        }
    }
}
