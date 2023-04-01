using System;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Skill8_4
{
    public class Task4
    {
        User user = new User();
        string path = "Users.xml";
        static void Main()
        {
            
            Task4 task_4 = new Task4();
            User user = new User();
            List<User> users = new List<User>();


            if (File.Exists(task_4.path))
            {
                users = DeserializeUser(task_4.path); //Проверка существования файла, если есть, то дессериализуем данные из него  в наш лист
            }
            
            user = GetInformation();
            users.Add(user);
            SerializeUser(users, task_4.path);
        }
        /// <summary>
        /// Заполнение информации о пользователе
        /// </summary>
        /// <returns></returns>
        static User GetInformation()
        {
            Console.WriteLine("Введите ФИО пользователя:");
            string FIO = Console.ReadLine();


            Console.WriteLine("Введите название улицы");
            string street = Console.ReadLine();


            Console.WriteLine("Введите номер дома пользователся");
            bool successOfHouseNumber = int.TryParse(Console.ReadLine(), out var houseNumber);
            while (!successOfHouseNumber)
            {
                Console.WriteLine("Введен некорректный номер дома, повторите попытку:");
                successOfHouseNumber = int.TryParse(Console.ReadLine(), out houseNumber);
            }


            Console.WriteLine("Введите номер квариты пользователся");
            bool successOfFlatNumber = int.TryParse(Console.ReadLine(), out var flatNumber);
            while (!successOfFlatNumber)
            {
                Console.WriteLine("Введен некорректный номер дома, повторите попытку:");
                successOfFlatNumber = int.TryParse(Console.ReadLine(), out flatNumber);
            }


            Console.WriteLine("Введите мобильный номер телефона пользователя");
            string mobilePhone = Console.ReadLine();
            while (mobilePhone.Length != 11 || mobilePhone[0] == 0)
            {
                Console.WriteLine("Введен некорректный номер телефона, повторите попытку:");
                mobilePhone = Console.ReadLine();
            }


            Console.WriteLine("Введите домашний номер телефона пользователя");
            string flatPhone = Console.ReadLine();
            while (flatPhone.Length != 7 || flatPhone[0] == 0)
            {
                Console.WriteLine("Введен некорректный номер телефона, повторите попытку:");
                flatPhone = Console.ReadLine();
            }


            User tempUser = new User(FIO, street, houseNumber, flatNumber, mobilePhone, flatPhone);
            return tempUser;
        }
        /// <summary>
        /// Сериализация листа пользователй
        /// </summary>
        /// <param name="ListOfUsers">Лист с пользователями, который нужно сохранить</param>
        /// <param name="path">Путь к файлу</param>
        static void SerializeUser(List<User> ListOfUsers, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);

            serializer.Serialize(stream, ListOfUsers);

            stream.Close();
        }
        /// <summary>
        /// Дессериализация листа пользователей
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
         static List<User> DeserializeUser(string path)
        {
            List<User> tempList = new List<User>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));

            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            tempList = (List<User>)xmlSerializer.Deserialize(stream);

            stream.Close();

            return tempList;
        }
    }

}