using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic
{
    /// <summary>
    /// Интерфейс для депозита
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAccount<out T>
        where T : class
    {
        /// <summary>
        /// Добавление денег на счет
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        T Deposit(decimal amount);
    }
    /// <summary>
    /// Интерфейс для перевода
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransferProcessor<T>
        where T : class
    {
         void Transfer(T from, T to, decimal amount,
            bool fromthisAccount, bool toThisAccount);
    }
    /// <summary>
    /// Не депозитный счет
    /// </summary>
    public class AccountA : IAccount<AccountA>
    {
        public decimal Balance { get; set; }
        
        public AccountA Deposit(decimal amount)
        {
            Balance += amount;
            return this;
        }

    }
    /// <summary>
    /// Депозитный счет
    /// </summary>
    public class AccountB : IAccount<AccountB>
    {
        public decimal Balance { get; set; }

        public AccountB Deposit(decimal amount)
        {
            Balance += amount;
            return this;
        }
    }
    /// <summary>
    /// Пополнение депозитного счета
    /// </summary>
    public class Deposit
    {
        public static void DepositToAccount(IAccount<object> account, decimal amount)
        {
            account.Deposit(amount);
        }
    }

    public class TransferProcessor : ITransferProcessor<Client>
    {
        /// <summary>
        /// Перевод средств с одного счета на другой
        /// </summary>
        /// <param name="from">Клиент, от которого будет производиться перевод</param>
        /// <param name="to">Клиент, к которому будет производиться перевод</param>
        /// <param name="amount">Сумма денег</param>
        /// <param name="fromThisAccount">Указывает с какого аккаунта нужно перевести(false - депозитный, true - не депозитный)</param>
        /// <param name="toThisAccount">Указывает на какой аккаунт нужно перевести(false - депозитный, true - не депозитный)</param>
        /// <returns></returns>
        public void  Transfer(Client from, Client to, decimal amount, bool fromThisAccount, bool toThisAccount)
        {
            decimal fromAccountBalance = fromThisAccount ? from.NonDepositMoney.Balance : from.DepositMoney.Balance;//с какого счета переводить

            if (fromAccountBalance >= amount)//проверка наличия денег для перевода
            {
                if (fromThisAccount)
                {
                    from.NonDepositMoney.Balance -= amount;
                }
                else
                {
                    from.DepositMoney.Balance -= amount;
                }

                if (toThisAccount)
                {
                    to.NonDepositMoney.Balance += amount;
                }
                else
                {
                    to.DepositMoney.Balance += amount;
                }
            }
        }
    }



}
