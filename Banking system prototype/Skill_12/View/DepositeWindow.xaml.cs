using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MainLogic;

namespace Skill_12
{
    /// <summary>
    /// Логика взаимодействия для DepositeWindow.xaml
    /// </summary>
    public partial class DepositeWindow : Window
    {

        private MainWindow _mainWindow;


        public DepositeWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void TbPhoneNumberForDeposit_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TbPhoneNumberForDeposit.Text == "Введите номер телефона клиента, счет которого хотите пополнить")
            {
                TbPhoneNumberForDeposit.Clear();
            }
        }

        private void TbAmountOfDeposite_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TbAmountOfDeposite.Text == "Введите сумму пополнения")
            {
                TbAmountOfDeposite.Clear();
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (sender is Window w) w.Hide();
        }

        private void FinishDeposit_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TbPhoneNumberForDeposit.Text.CheckPhoneNumber() && TbAmountOfDeposite.Text.CheckAmountOfMoney()
                && CbAccountToDeposit.SelectedItem != null)//проверка ввода
            {
                if (TbAmountOfDeposite.Text == "0")
                {
                    try
                    {
                        throw new Exceptions("Сумма пополнения должна быть больше 0");
                    }
                    catch (Exceptions t)
                    {
                        MessageBox.Show(t.Message);
                    }
                }
                else
                {
                    MainWindow.clientList = Program.GetClientsInfoFromJson();
                    Client client = MainWindow.clientList.FindClientByPhoneNumber(TbPhoneNumberForDeposit.Text);

                    bool success = false;
                    switch (CbAccountToDeposit.SelectedIndex)//проверка на то, какой счет выбран
                    {
                        case 0:
                            if (client.DepositMoney != null)//проврерка существования этого счета у выбранного клиента
                            {
                                Deposit.DepositToAccount(client.DepositMoney, Convert.ToDecimal(TbAmountOfDeposite.Text));
                                success = true;
                            }
                            break;
                        case 1:
                            if (client.NonDepositMoney != null)//проверка существования этого счета у выбранного клиента
                            {
                                Deposit.DepositToAccount(client.NonDepositMoney, Convert.ToDecimal(TbAmountOfDeposite.Text));
                                success = true;
                            }
                            break;
                    }
                    if (!success)
                    {
                        System.Media.SystemSounds.Hand.Play();
                        MessageBox.Show("Данного счета у выбранного клиента не найдено");
                    }
                    else
                    {

                        Program.GetOperationsFromJson();

                        Operation.AddNewItem += SaveOperations;
                        Operation.OnAddNewItem(new Operation("Пополнение",
                                                          Convert.ToDecimal(TbAmountOfDeposite.Text),
                                                          "Пополнение системой",
                                                          "-",
                                                          "-",
                                                          client.PhoneNumber,
                                                          client.Account,
                                                          TransferWindow.FindStatusOfClient?.Invoke(client)));
                        MainWindow.clientList.SaveClientsToJson();   //Сохранение новой суммы клиента
                        this.Close();
                    }
                }
            }

            else
            {
                List<string> errorList = new List<string>();
                string errors = "";
                if (!TbPhoneNumberForDeposit.Text.CheckPhoneNumber())//Если неверный номер телефона
                    errorList.Add("Клиента с данным номером телефона не существует");

                if (!TbAmountOfDeposite.Text.CheckAmountOfMoney())//Если неверная сумма денег
                    errorList.Add("Введена неверная сумма");

                if (CbAccountToDeposit.SelectedItem == null)
                    errorList.Add("Не выбран тип счета клиента");

                foreach (var item in errorList)
                {
                    errors += $"\n{item}";
                }
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show(errors);
            }
        }
        /// <summary>
        /// Сохранение операций в json
        /// </summary>
        public static void SaveOperations() => Operation.OperationList.SaveOperationsToJson();

    }
}
