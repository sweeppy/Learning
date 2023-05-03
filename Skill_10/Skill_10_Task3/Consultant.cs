using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using Newtonsoft.Json;

namespace BankProgram_ver3
{

    class Consultant : Employer
    {
        
        public override void WhatToDoWithList(string whoChanging)
        {
            Console.WriteLine("Вам достуно только изменени номера телефона, нажмите лююую клавишу чтобы продолжить");
            Console.ReadKey();
            ChooseParamToChange(whoChanging);
        }
        public override void ChooseParamToChange(string whoChanging)
        {
            List<Client> clients = new List<Client>();
            clients = TakeInfoFromJson(clients);
            ChangePhoneNumber(clients, whoChanging);
            SaveToJson(clients);
        }
        /// <summary>
        /// Изменение номера клиента
        /// </summary>
        /// <param name="clients"></param>
        /// <param name="whoChanging"></param>
        public void ChangePhoneNumber(List<Client> clients, string whoChanging)
        {
            string showForRequest = "номер телефона";
            string phoneNumber = TakeInfOfClientToChange(showForRequest);
            bool successOfChanging = false;

            foreach (var item in clients)
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Введите новый номер телефона:");
                    string tempPhoneNumber = Console.ReadLine();
                    item.PhoneNumber = tempPhoneNumber;
                    item.TypeOfChanging = "Изменение";
                    item.WhoChanged = whoChanging;
                    item.DateOfChange = DateTime.Now;
                    successOfChanging = true;
                    break;
                }
            }
            if (successOfChanging)
                Console.WriteLine("Номер телефона был успешно изменен!");
            else
                Console.WriteLine("Клиента с данным номером телефона не было найдено");
        }
    }
}