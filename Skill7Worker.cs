using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill7
{
    struct Worker
    {
        /// <summary>
        /// ID работника
        /// </summary>
        /// 
        public int Id { get; set; }
        /// <summary>
        /// Дата создания файла
        /// </summary>
        public DateTime DateOfCreate { get; set; }
        /// <summary>
        /// Ф.И.О. работника
        /// </summary>
        public string FIO { get; set; }
        /// <summary>
        /// Возраст работника
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Рост работника
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Дата рождения работника
        /// </summary>
        ///
        public DateTime DateOfBirthday { get; set; }
        /// <summary>
        /// Место рождения 
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }

        
        /// <summary>
        /// Конструктор Worker
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="DateOfCreate"></param>
        /// <param name="FIO"></param>
        /// <param name="Age"></param>
        /// <param name="Height"></param>
        /// <param name="DateOfBirthday"></param>
        /// <param name="City"></param>
        public Worker (int Id, DateTime DateOfCreate, string FIO, int Age, int Height, DateTime DateOfBirthday, string City)
        {
            this.Id = Id;
            this.DateOfCreate = DateOfCreate;
            this.FIO = FIO;
            this.Age = Age;
            this.Height = Height;
            this.DateOfBirthday = DateOfBirthday;
            this.City = City;
        }
        /// <summary>
        /// Возвращает данные, полученные из Worker в строковом виде
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{Id}) {DateOfCreate} {FIO} {Age} {Height} {DateOfBirthday} {City}";
        }
        /// <summary>
        /// Метод для записи данных из массива workers в файл
        /// </summary>
        /// <returns></returns>
        public string Print2(int newID)
        {
            return $"{newID}#{DateOfCreate}#{FIO}#{Age}#{Height}#{DateOfBirthday}#{City}";
        }


    }
}
