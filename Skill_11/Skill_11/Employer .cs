using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Skill_11
{
    
    abstract class Employer
    {
        Repository repository = new Repository();
        List<Department> departmentList = new List<Department>();
        /// <summary>
        /// Критерии для сортировки клиентов
        /// </summary>
        public enum SortedCriterion
        {
            LastName,
            FirstName,
            Patronymic,
            PhoneNumber,
            PassportSeries,
            PassportID,
            DateOfChange,
            InformationThatChanged,
            WhoChanged,
            TypeOfChanging
        }
        /// <summary>
        /// Сравнивание по имени клиента
        /// </summary>
        private class SortByFirstName : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.FirstName, Y.FirstName);
            }
        }
        /// <summary>
        /// сравнение по фамилии клиента
        /// </summary>
        private class SortByLastName : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;
                
                return String.Compare(X.LastName, Y.LastName);
            }
        }
        /// <summary>
        /// сравнение по отчеству клиента
        /// </summary>
        private class SortByPatronymic : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.Patronymic, Y.Patronymic);
            }
        }
        /// <summary>
        /// сравнение по номеру телефона клиента
        /// </summary>
        private class SortByPhoneNumber : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.PhoneNumber, Y.PhoneNumber);
            }
        }
        /// <summary>
        /// сравнение по серии паспорта клиента
        /// </summary>
        private class SortByPassportSeries : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;
                if (X.PassportSeries > Y.PassportSeries) return 0;
                else return -1;
            }
        }
        /// <summary>
        /// сравнение по номеру паспорта клиента
        /// </summary>
        private class SortByPassportID : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                if (X.PassportID > Y.PassportID) return 0;
                else return -1;
            }
        }
        /// <summary>
        /// сравнение по дате изменения
        /// </summary>
        private class SortByDateOfChange : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                if (X.DateOfChange > Y.DateOfChange) return 0;
                else return -1;
            }
        }
        /// <summary>
        /// сравнение по информации, которую изменили
        /// </summary>
        private class SortByInformationThatChanged : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.InformationThatChanged, Y.InformationThatChanged);
            }
        }
        /// <summary>
        /// сравнение по тому, кто изменил
        /// </summary>
        private class SortByWhoChanged : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.WhoChanged, Y.WhoChanged);
            }
        }
        /// <summary>
        /// сравнение по типу изменения
        /// </summary>
        private class SortByTypeOfChanging : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.TypeOfChanging, Y.TypeOfChanging);
            }
        }
        /// <summary>
        /// Сортировка клиентов
        /// </summary>
        /// <param name="Criterion"></param>
        /// <returns></returns>
        public static IComparer<Client> SortedBy(SortedCriterion Criterion)
        {
            if (Criterion == SortedCriterion.FirstName) return new SortByFirstName();
            if (Criterion == SortedCriterion.LastName) return new SortByLastName();
            if (Criterion == SortedCriterion.Patronymic) return new SortByPatronymic();
            if (Criterion == SortedCriterion.PhoneNumber) return new SortByPhoneNumber();
            if (Criterion == SortedCriterion.PassportSeries) return new SortByPassportSeries();
            if (Criterion == SortedCriterion.PassportID) return new SortByPassportID();
            if (Criterion == SortedCriterion.DateOfChange) return new SortByDateOfChange();
            if (Criterion == SortedCriterion.InformationThatChanged) return new SortByInformationThatChanged();
            if (Criterion == SortedCriterion.WhoChanged) return new SortByWhoChanged();
            else return new SortByTypeOfChanging();
        }
        /// <summary>
        /// Создание рандомного клиента
        /// </summary>
        /// <param name="clients">Лист клиентов</param>
        /// <param name="whoChanging">Кто добавляет</param>
        /// <param name="departmentList">Лист департаментов</param>
        public void GenerateRandomClients(List<Client> clients, string whoChanging, List<Department> departmentList)
        {
            bool isDepartmentOfRC = true;
            departmentList = repository.GetDepartmentsInfoFromJson();
            clients = repository.GetClientsInfoFromJson();

            for (int i = 0; i < 2; i++)
            {
                Client client = new Client(whoChanging, 0);
                clients.Add(client);
            }
            
            foreach (var item in departmentList)
            {
                if (item.DepartamentID == 0)        //проврка существования департамента с рандомными клиентами
                    isDepartmentOfRC = false;
            }

            if (isDepartmentOfRC) 
            {
                departmentList.Add(new Department("departmentOfRandomClients", 0));
                repository.SaveDepartmentsToJson(departmentList);
            }
            repository.SaveClientsToJson(clients);//сохранение клиентов в Json
            
        }
    }
}

