using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsDataBase.Models
{
    internal class nullAnimal : IAnimal
    {
        public int animalId { get; set; }
        public string animalClass { get; set; }
        public string animalSquad { get; set; }
        public string animalFamily { get; set; }
        public string animalGenus { get; set; }
        public string animalSpecies { get; set; }

        public nullAnimal()
        {
            this.animalClass = "wrong input";
            this.animalSquad = "wrong input";
            this.animalFamily = "wrong input";
            this.animalGenus = "wrong input";
            this.animalSpecies = "wrong input";
        }
    }
}
