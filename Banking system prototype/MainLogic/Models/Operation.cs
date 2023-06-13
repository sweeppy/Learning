using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic
{
    public class Operation
    {
        public static List<Operation> OperationList = new List<Operation>();
        public static string jsonOperations = "operations.json";
        public static event Action AddNewItem; 
        /// <summary>
        /// Тип Операции
        /// </summary>
        public string TypeOfOperation { get; set; }
        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// От кого
        /// </summary>
        public string PhoneNumberFrom { get; set; }
        /// <summary>
        /// Тип счета отправляемого
        /// </summary>
        public string TypeOfAccountFrom { get; set; }
        /// <summary>
        /// К кому
        /// </summary>
        public string PhoneNumberTo { get; set; }
        /// <summary>
        /// Тип счета получаемого
        /// </summary>
        public string TypeOfAccountTo { get; set; }

        /// <summary>
        /// Статус клиента отправителя
        /// </summary>
        public string StatusFrom { get; set; }
        /// <summary>
        /// Статус клиента получателя
        /// </summary>
        public string StatusTo { get; set; }
        /// <summary>
        /// Время создания записи
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Для выделения сомнительных переводов и пополнений
        /// </summary>
        public string Warning { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="TypeOfOperation"></param>
        /// <param name="Amount"></param>
        /// <param name="PhoneNumberFrom"></param>
        /// <param name="TypeOfAccountFrom"></param>
        /// <param name="StatusFrom"></param>
        /// <param name="PhoneNumberTo"></param>
        /// <param name="TypeOfAccountTo"></param>
        /// <param name="DepartmentID"></param>
        public Operation(string TypeOfOperation, decimal Amount, string PhoneNumberFrom, string TypeOfAccountFrom, string StatusFrom,
            string PhoneNumberTo, string TypeOfAccountTo, string StatusTo)
        {
            this.TypeOfOperation = $" {TypeOfOperation} ";
            this.Amount = Amount;
            this.PhoneNumberFrom = $" {PhoneNumberFrom} ";
            this.TypeOfAccountFrom = $" {TypeOfAccountFrom} ";
            this.StatusFrom = StatusFrom;


            this.PhoneNumberTo = $" {PhoneNumberTo} ";
            this.TypeOfAccountTo = $" {TypeOfAccountTo} ";
            this.StatusTo = StatusTo;
            this.Date = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// Добавление новой операции
        /// </summary>
        /// <param name="oper"></param>
        public static void OnAddNewItem(Operation oper)
        {
            Operation.OperationList.Add(oper);
            oper.CheckDeposit();
            oper.CheckTransfer();
            AddNewItem?.Invoke();
        }
        /// <summary>
        /// Проверка сомнительных депозитов
        /// </summary>
        /// <param name="operation"></param>
        private void CheckDeposit()
        {
            foreach (var item in Operation.OperationList.FindAll(e => e.Amount > 1000000m))
            {
                item.Warning = "Warning. Deposit > 1.000.000";
            }
        }
        /// <summary>
        /// Проверка сомнительных переводов
        /// </summary>
        private void CheckTransfer()
        {
            foreach (var item in Operation.OperationList)
            {
                if (item.StatusFrom != "-")
                {
                    if (item.StatusFrom != StatusTo || item.Amount > 1000000m ||
                    item.TypeOfAccountFrom != item.TypeOfAccountTo)
                    {
                        item.Warning = "Warning";
                    }
                }
                
            }
        }
    }
}
