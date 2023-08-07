using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AnimalsDataBase.Models
{
    public class Program
    {
        public static MSSQLLocalAnimalDBEntities animalContext;

        public static void LoadFromDB()
        {
            animalContext = new MSSQLLocalAnimalDBEntities();

            animalContext.Amphibian.Load();
            animalContext.Mammal.Load();
            animalContext.Bird.Load();
        }
    }
}
