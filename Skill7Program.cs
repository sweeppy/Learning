using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill7
{
    class Solution
    {
        static void Main()
        {
            Repository repository = new Repository(@"e:\workersFile.txt");
            string path = repository.path;
            repository.CreationFile(path);


            Console.WriteLine($"Ведите цифру для выполнения соответствующей команды" +
                    $"\n 1:Вывести всех работников в консоль и при желании отсортировать данные" +
                    $"\n 2:Вывести данные конкретного работника на экран" +
                    $"\n 3:Создать новую запись" +
                    $"\n 4:Удаление записи" +
                    $"\n 5:Вывести записи в выбранном диапазоне дат");
            AllCommands(repository, path);  
        }

        /// <summary>
        /// Основа алгоритма вывода или редактирования записей
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="path">путь к файлу</param>
        public static void AllCommands(Repository repository, string path)
        {
            bool success = int.TryParse(Console.ReadLine(), out var result);
            while (success == false || result > 5 || result < 1)
            {
                Console.WriteLine($"Введено некоректное число, повторите ввод:");
                success = int.TryParse(Console.ReadLine(), out result);
            }
            switch (result)
            {
                case 1:///вывод всех сотрудников
                    repository.GetAllWorkers();
                    Console.WriteLine($"Желаете ли отсортировать данные?" +
                        $"\n введите 1 для выбора критерия для сортировки" +
                        $"\n введите 2 для вывода всех сотрудников");
                    Sort(repository);
                    break;
                case 2:///вывод конкретного сотрудника
                    GetOneWorker(repository);
                    break;
                case 3:///добавление новой записи
                    CreationAnEnrtyOfNewWorker(repository, path);
                    break;
                case 4:
                    DeleteOneWorker(repository);
                    break;
                case 5:
                    GetWorkersByDate(repository);
                    break;
            }
        }
        /// <summary>
        /// Метод сортировки и вывода в консоль сотрудников по выбранному пункту
        /// </summary>
        public static void Sort(Repository repository)
        {
            IEnumerable<Worker> query = repository.workers.OrderBy(worker => worker.Id); ///для сортировки сотрудников

            bool bl = int.TryParse(Console.ReadLine(), out var onetwo);
            while (bl == false || onetwo > 2 || onetwo < 1)
            {
                Console.WriteLine($"Введено некоректное число, повторите ввод:");
                bl = int.TryParse(Console.ReadLine(), out onetwo);
            }
            switch (onetwo) ///Выбор вывода: с сортировкой/без
            {
                case 1:
                    SortBy(query, repository);
                    break;
                case 2:
                    for (int i = 0; i < repository.index; i++)
                    {
                        Console.WriteLine(repository.workers[i].Print());           ///вывод без сортировки
                    }
                    break;
            }
        }
        /// <summary>
        /// сортирует сотрудников по выбранному пункту и выводит в консоль
        /// </summary>
        /// <param name="query"></param>
        public static void SortBy(IEnumerable<Worker> query, Repository repository)
        {
            Console.WriteLine($"Ведите цифру для выполнения соответствующей команды" +
                        $"\n 1:Отсортировать по дате добавления файла" +
                        $"\n 2:Отсортировать по ФИО" +
                        $"\n 3:Отсортировать по возрасту" +
                        $"\n 4:Отсортировать по росту" +
                        $"\n 5:Отсортировать по дате рождения" +
                        $"\n 6:Отсортировать по назанию города");
            bool ss = int.TryParse(Console.ReadLine(), out var sort);
            while (ss == false || sort > 6 || sort < 1)
            {
                Console.WriteLine($"Введено некоректное число, повторите ввод:");
                ss = int.TryParse(Console.ReadLine(), out sort);
            }
            switch (sort)
            {
                case 1:
                    query = repository.workers.OrderBy(worker => worker.DateOfCreate);
                    break;
                case 2:
                    query = repository.workers.OrderBy(worker => worker.FIO);
                    break;
                case 3:
                    query = repository.workers.OrderBy(worker => worker.Age);
                    break;                                                                      ///сортировка в соответсвии с пунктом
                case 4:
                    query = repository.workers.OrderBy(worker => worker.Height);
                    break;
                case 5:
                    query = repository.workers.OrderBy(worker => worker.DateOfBirthday);
                    break;
                case 6:
                    query = repository.workers.OrderBy(worker => worker.City);
                    break;
            }
            foreach (var item in query)
            {
                if (item.Id != 0)
                {
                    Console.WriteLine(item.Print());                        ///вывод отсортированных данных в консоль
                }

            }
            
        }
        /// <summary>
        /// Вывод конкретного сотрудника в консоль
        /// </summary>
        /// <param name="repository"></param>
        public static void GetOneWorker(Repository repository)
        {
            repository.GetAllWorkers();
            Console.WriteLine("Ведите ID сотрудника");
            bool suc = int.TryParse(Console.ReadLine(), out var res);
            while (suc == false || res > repository.index)
            {
                Console.WriteLine($"Такого сотрудника нет, повторите попытку");
                suc = int.TryParse(Console.ReadLine(), out res);
            }
            Console.WriteLine(repository.GetWorkerById(res).Print());
        }
        /// <summary>
        /// Создание записи о новом работнике с дальнейшим переносом в txt файл
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="path">путь к файлу</param>
        public static void CreationAnEnrtyOfNewWorker(Repository repository, string path)
        {
            repository.GetAllWorkers();
            int id = repository.index + 1; ///установка ID рабочего


            DateTime timeOfCreate = DateTime.Now; ///установка времени создания записи

            Console.WriteLine($"Введите Ф.И.О. рабочего");///установка ФИО рабочего
            string FIO = Console.ReadLine();

            Console.WriteLine($"Введите возраст рабочего");///установка возраста
            bool successOfAge = int.TryParse(Console.ReadLine(), out var age);
            while (successOfAge == false || age > 80 || age < 16)
            {
                Console.WriteLine($"Введено некоректное число, повторите ввод:");
                successOfAge = int.TryParse(Console.ReadLine(), out age);
            }

            Console.WriteLine($"Введите рост рабочего");///установка роста рабочего
            bool successOfHeight = int.TryParse(Console.ReadLine(), out var height);
            while (successOfAge == false || height > 230 || height < 130)
            {
                Console.WriteLine($"Введено некоректное число, повторите ввод:");
                successOfHeight = int.TryParse(Console.ReadLine(), out height);
            }

            Console.WriteLine($"Введите дату рождения работника в формате ####.##.##");
            bool data = DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirthday);///установка даты рождения работника
            while (data == false)
            {
                Console.WriteLine($"Введена некоректная дата, попробуйте снова");
                data = DateTime.TryParse(Console.ReadLine(), out dateOfBirthday);
            }

            Console.WriteLine($"Введите место рождения:");///установка места рождения
            string placeOfBirthday = Console.ReadLine();

            repository.AddWorker(new Worker(id, timeOfCreate, FIO, age, height, dateOfBirthday, placeOfBirthday));///добавление нового рабочего в список

            using (StreamWriter sw = new StreamWriter(path, true)) ///запись рабочего в файл
            {
                sw.WriteLine();
                sw.Write($"{repository.workers[repository.index - 1].Print2(repository.index)}");
            }
        }
        /// <summary>
        /// Удаляет запись о рабочем в соответсвии с его ID
        /// </summary>
        /// <param name="repository"></param>
        public static void DeleteOneWorker(Repository repository)
        {
            repository.GetAllWorkers();
            Console.WriteLine("Ведите ID сотрудника");
            bool ff = int.TryParse(Console.ReadLine(), out var r);
            while (ff == false || r > repository.index)
            {
                Console.WriteLine($"Такого сотрудника нет, повторите попытку");             ///удаление сотрудника
                ff = int.TryParse(Console.ReadLine(), out r);
            }
            repository.DeleteWorker(r);
        }
        /// <summary>
        /// Вывод работников в консоль с определенной даты по определенную дату
        /// </summary>
        /// <param name="repository"></param>
        public static void GetWorkersByDate(Repository repository)
        {
            repository.GetAllWorkers();
            Console.WriteLine($"Введите дату с котрой нужно вывести сотрудников в формате ####.##.##");
            bool datafr = DateTime.TryParse(Console.ReadLine(), out DateTime datefrom);///установка даты рождения работника
            while (datafr == false)
            {
                Console.WriteLine($"Введена некоректная дата, попробуйте снова");
                datafr = DateTime.TryParse(Console.ReadLine(), out datefrom);
            }
            Console.WriteLine($"Введите дату по которую нужно вывести сотрудников в формате ####.##.##");
            bool datat = DateTime.TryParse(Console.ReadLine(), out DateTime dateto);///установка даты рождения работника
            while (datat == false)
            {
                Console.WriteLine($"Введена некоректная дата, попробуйте снова");
                datat = DateTime.TryParse(Console.ReadLine(), out dateto);
            }
            repository.GetWorkersBetweenTwoDates(datefrom, dateto);
        }
    }
}
