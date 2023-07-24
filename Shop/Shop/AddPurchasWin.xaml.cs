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
        public AddPurchasWin()
        {
            InitializeComponent();
        }

        public AddPurchasWin(DataRow row):this()
        {
            BtnCancel.Click += delegate { this.DialogResult = false; };



            BtnConfirm.Click += delegate
            {
                bool success = false;
                for (int i = 0; i < Program.DTMain.Rows.Count; i++)
                {
                    if (Program.DTMain.Rows[i][5].ToString() == email.Text)
                    {
                        success = true;
                        break;
                    }

                }
                if (email.Text != "" &&
                    idProduct.Text != "" &&
                    productName.Text != "" &&
                    int.TryParse(idProduct.Text, out _) &&
                    success)

                {
                    row["email"] = email.Text;
                    row["idProduct"] = idProduct.Text;
                    row["productName"] = productName.Text;
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
