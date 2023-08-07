using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsDataBase.Models
{
    public interface IAnimal
    {
        int animalId { get; set; }
        string animalClass { get; set; }
        string animalSquad { get; set; }
        string animalFamily { get; set; }
        string animalGenus { get; set; }
        string animalSpecies { get; set; }
    }
}
