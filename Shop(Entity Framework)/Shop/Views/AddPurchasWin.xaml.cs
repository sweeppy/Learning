using System;
using System.Collections.Generic;
using System.Data;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для AddPurchasWin.xaml
    /// </summary>
    public partial class AddPurchasWin : Window
    {
        public Purchases purchas;
        public bool cancel;
        public AddPurchasWin()
        {
            InitializeComponent();

            BtnCancel.Click += delegate { this.DialogResult = false; cancel = true; };

            BtnConfirm.Click += delegate
            {   
                cancel = false;

                bool success = Program.ClientsContext.Clients.Where
                (cl => cl.email == email.Text).Any();//проверка сущ клиента с введенным email


                if (email.Text != "" &&
                    idProduct.Text != "" &&
                    productName.Text != "" &&
                    int.TryParse(idProduct.Text, out var idProductINT) &&
                    success
                    )

                {
                    purchas = new Purchases(email.Text, idProductINT,productName.Text);
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                    string errors = "";
                    if (email.Text == "") errors += "\nWrong email";
                    if (idProduct.Text == "" || !int.TryParse(idProduct.Text, out _)) errors += "\nWrong idProduct";
                    if (productName.Text == "") errors += "\nWrong productName";
                    if (!success) errors += "\nUser with given email was not found";
                    MessageBox.Show(errors);
                }



            };
        }

    }

}

