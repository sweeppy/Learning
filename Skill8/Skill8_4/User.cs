using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill8_4
{
    public struct User
    {
        /// <summary>
        ///ФИО пользователя
        /// </summary>
        public string FIO { get; set; }
        /// <summary>
        /// Улица пользователя
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Номер дома пользователя
        /// </summary>
        public int HouseNumber { get; set; }
        /// <summary>
        /// Номер квартиры пользователя
        /// </summary>
        public int FlatNumber { get; set; }
        /// <summary>
        /// Мобильный номер телефона пользователя
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// Домашний номер телефона пользователя
        /// </summary>
        public string FlatPhone { get; set; }

        public User(string FIO, string Street, int HouseNumber, int FlatNumber, string MobilePhone, string FlatPhone)
        {
            this.FIO = FIO;
            this.Street = Street;
            this.HouseNumber = HouseNumber;
            this.FlatNumber = FlatNumber;
            this.MobilePhone = MobilePhone;
            this.FlatPhone = FlatPhone;
        }
        public string Print()
        {
            return $"ФИО: {FIO}" +
                 $"\nУлица: {Street}" +
                 $"\nНомер дома: {HouseNumber}" +
                 $"\nНомер квартиры: {FlatNumber}" +
                 $"\nМобильный номер телефона: {MobilePhone}" +
                 $"\nДомашний номер телфона: {FlatPhone}";
        }


    }
}
