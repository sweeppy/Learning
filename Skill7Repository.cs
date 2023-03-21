using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill7
{
    class Repository
    {
        public Worker[] workers;
        public int index;
        public string path = @"e:\workersFile.txt";
        /// <summary>
        /// Создает файл, если его не существует
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void CreationFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
                
        }
        
        
        /// <summary>
        /// Увеличивает массив workers в 2 раза, если не хватает места для добовления новго работника
        /// </summary>
        /// <param name="Flag">переменная типа bool, которая проверяет хватает ли места для добавления нового работника</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }
        /// <summary>
        /// Коструктор
        /// </summary>
        /// <param name="path"></param>
        public Repository(string path)
        {
            this.path = path;
            this.workers = new Worker[2];
        }
        
        /// <summary>
        /// Добовляет нового сотрудника в массив workers
        /// </summary>
        /// <param name="worker"></param>
         public void AddWorker(Worker worker)
        {
            Resize(index >= this.workers.Length);
            this.workers[index] = worker;
            index++;
        }

        /// <summary>
        /// Возвращает данные полученные из фала в массив (Worker[] workers)
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split("#");

                    AddWorker(new Worker(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2],
                        Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), Convert.ToDateTime(args[5]), args[6]));
                }
                return this.workers;
            }
        }
        /// <summary>
        /// Возвращает рабочего с указанным ID
        /// </summary>
        /// <param name="id">ID рабочего, которого нужно найти</param>
        /// <returns></returns>
        public Worker GetWorkerById(int id)
        {
            return workers[id - 1];
        }
        /// <summary>
        /// Удаляет рабочего по его ID
        /// </summary>
        /// <param name="id">ID рабочего, которого стоит удалить</param>
        public void DeleteWorker(int id)
        {
                for (int i = id - 1; i < index - 1; i++)
                {
                    workers[i] = workers[i + 1];
                }
                Array.Resize(ref workers, index - 1);
                index--;
            
            using (StreamWriter st = new StreamWriter(path, false))
            {
                for (int j = 0; j < index; j++)
                {
                    if (j == 0)
                    {
                        st.Write($"{workers[j].Print2(j + 1)}");
                    }
                    else
                    {
                        st.WriteLine();
                        st.Write($"{workers[j].Print2(j + 1)}");
                    }
                }
            }

        }
        /// <summary>
        /// Вывод в консоль сотрудников в выбранном диапозоне дат
        /// </summary>
        /// <param name="dateFrom">с этой даты</param>
        /// <param name="dateTo">по эту дату</param>
        public void GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            IEnumerable<Worker> datasort = workers.OrderBy(w => w.DateOfCreate);
            foreach (var item in datasort)
            {
                if (item.Id != 0 && item.DateOfCreate > dateFrom && item.DateOfCreate < dateTo)
                {
                    Console.WriteLine(item.Print());
                }
            }
        }
    }

}

