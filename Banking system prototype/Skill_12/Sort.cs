using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill_12
{
    class Sort
    {
        public enum SortedCriterion
        {
            LastName,
            FirstName,
            Patronymic,
            PhoneNumber,
            PassportSeries,
            PassportID,
            DateOfChange,
            Money,
            Account
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
        class SortByLastName : IComparer<Client>
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
        class SortByPatronymic : IComparer<Client>
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
        class SortByPhoneNumber : IComparer<Client>
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
        class SortByPassportSeries : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;
                return String.Compare(X.PassportSeries, Y.PassportSeries);
            }
        }
        /// <summary>
        /// сравнение по номеру паспорта клиента
        /// </summary>
        class SortByPassportID : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.PassportID, Y.PassportID);
            }
        }
        /// <summary>
        /// сравнение по дате изменения
        /// </summary>
        class SortByDateOfChange : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                if (X.DateOfCreate > Y.DateOfCreate) return 0;
                else return -1;
            }
        }
        /// <summary>
        /// Сорртировка по количеству денег на счету
        /// </summary>
        class SortByMoney : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                if (X.NonDepositMoney.Balance > Y.NonDepositMoney.Balance) return 0;
                else return -1;
            }
        }

        class SortByAccount : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = x as Client;
                Client Y = y as Client;

                return String.Compare(X.Account, Y.Account);
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
            if (Criterion == SortedCriterion.Money) return new SortByMoney();
            else return new SortByAccount();
        }
    }
}
