using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AnimalsDataBase.Models
{
    class SaveAnimalsToJson
    {
        private static string pathAmphibian = "Amphibian.json";
        private static string pathBird = "Bird.json";
        private static string pathMammal = "Mammal.json";
        public static void SaveAmphibianToJson(System.Data.Entity.DbSet<AnimalsDataBase.Models.Amphibian> amphibianList)
        {
            string jsonSer = JsonConvert.SerializeObject(amphibianList);
            File.WriteAllText(pathAmphibian, jsonSer);
        }

        public static void SaveBirdToJson(System.Data.Entity.DbSet<AnimalsDataBase.Models.Bird> birdList)
        {
            string jsonSer = JsonConvert.SerializeObject(birdList);
            File.WriteAllText(pathBird, jsonSer);
        }

        public static void SaveMammalToJson(System.Data.Entity.DbSet<AnimalsDataBase.Models.Mammal> mammalList)
        {
            string jsonSer = JsonConvert.SerializeObject(mammalList);
            File.WriteAllText(pathMammal, jsonSer);
        }
    }
}
