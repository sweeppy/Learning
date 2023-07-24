using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Shop
{
    interface CombineTables
    {
        /// <summary>
        /// Вывод информации о покупках выбранного клиента
        /// </summary>
        /// <param name="email"></param>
        void CombineTables(string email);
    }
    public class Program : CombineTables
    {
        //Первая БД
        public static SqlConnection sqlConMain;
        public static SqlDataAdapter sqlDAMain;
        public static DataTable DTMain;

        //Вторая БД
        public static SqlConnection sqlConSecond;
        public static SqlDataAdapter sqlDASecond;
        public static DataTable DTSecond;


        public static SqlConnectionStringBuilder conStrMain;
        public static SqlConnectionStringBuilder ConStrSecond;

        /// <summary>
        /// Подготовка двух СУБД
        /// </summary>
        /// <returns></returns>
        public static async Task StartConnection()
        {
            var program = new Program();
            var mainTableTask = program.Prepearing_MainTable();
            var secondTableTask = program.Prepearing_SecondTable();
            await Task.WhenAll(mainTableTask, secondTableTask);
        }
        /// <summary>
        /// Подготовка главной таблицы
        /// </summary>
        /// <returns></returns>
        public async Task Prepearing_MainTable()
        {
            DTMain = new DataTable();
            sqlDAMain = new SqlDataAdapter();
            conStrMain = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MSSQLShopDB",
                IntegratedSecurity = true
            };


            sqlConMain = new SqlConnection(conStrMain.ConnectionString);

            //SQL SELECT
            var sql = @"SELECT * FROM Clients Order By Clients.Id";
            sqlDAMain.SelectCommand = new SqlCommand(sql, sqlConMain);

            //SQL INSERT
            sql = @"INSERT INTO Clients(firstName, lastName, patronymic, phoneNumber, email)
                                VALUES (@firstName, @lastName, @patronymic, @phoneNumber, @email);
                                SET @id = @@IDENTITY";
            sqlDAMain.InsertCommand = new SqlCommand(sql, sqlConMain);
            sqlDAMain.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").Direction = ParameterDirection.Output;
            sqlDAMain.InsertCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 30, "firstName");
            sqlDAMain.InsertCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 30, "lastName");
            sqlDAMain.InsertCommand.Parameters.Add(@"patronymic", SqlDbType.NVarChar, 30, "patronymic");
            sqlDAMain.InsertCommand.Parameters.Add(@"phoneNumber", SqlDbType.BigInt, 12, "phoneNumber");
            sqlDAMain.InsertCommand.Parameters.Add(@"email", SqlDbType.NVarChar, 30, "email");

            //SQL UPDATE
            sql = @"UPDATE Clients SET
                                        firstName = @firstName,
                                        lastName = @lastName,                    
                                        patronymic = @patronymic,             
                                        phoneNumber = @phoneNumber, 
                                        email = @email
                                   WHERE id = @id";

            sqlDAMain.UpdateCommand = new SqlCommand(sql, sqlConMain);
            sqlDAMain.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original;
            sqlDAMain.UpdateCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 30, "firstName");
            sqlDAMain.UpdateCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 30, "lastName");
            sqlDAMain.UpdateCommand.Parameters.Add(@"patronymic", SqlDbType.NVarChar, 30, "patronymic");
            sqlDAMain.UpdateCommand.Parameters.Add(@"phoneNumber", SqlDbType.BigInt, 30, "phoneNumber");
            sqlDAMain.UpdateCommand.Parameters.Add(@"email", SqlDbType.NVarChar, 30, "email");

            //SQL DELETE
            sql = @"DELETE FROM Clients WHERE id = @id";
            sqlDAMain.DeleteCommand = new SqlCommand(sql, sqlConMain);
            sqlDAMain.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");


            sqlDAMain.Fill(DTMain);
            await sqlConMain.OpenAsync();
        }
        /// <summary>
        /// Подготовка таблицы покупок
        /// </summary>
        /// <returns></returns>
        public async Task Prepearing_SecondTable()
        {
            DTSecond = new DataTable();
            sqlDASecond = new SqlDataAdapter();

            ConStrSecond = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MSSQLPurchasesDB",
                IntegratedSecurity = true
            };

            sqlConSecond = new SqlConnection(ConStrSecond.ConnectionString);

            //SQL SELECT
            var sql = @"SELECT * FROM Purchases Order By Purchases.id";
            sqlDASecond.SelectCommand = new SqlCommand(sql, sqlConSecond);

            //SQL INSERT
            sql = @"INSERT INTO Purchases(email, idProduct, productName)
                           VALUES (@email, @idProduct, @productName)
                           SET @id = @@IDENTITY";
            sqlDASecond.InsertCommand = new SqlCommand(sql, sqlConSecond);
            sqlDASecond.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").Direction = ParameterDirection.Output;
            sqlDASecond.InsertCommand.Parameters.Add(@"email", SqlDbType.NVarChar, 30, "email");
            sqlDASecond.InsertCommand.Parameters.Add(@"idProduct", SqlDbType.Int, 0, "idProduct");
            sqlDASecond.InsertCommand.Parameters.Add(@"productName", SqlDbType.NVarChar, 30, "productName");

            //SQL UPDATE
            sql = @"UPDATE Purchases
                    SET email = @email,
                        idProduct  = @idProduct,
                        productName = @productName
                    WHERE id = @id";
            sqlDASecond.UpdateCommand = new SqlCommand(sql, sqlConSecond);

            sqlDASecond.UpdateCommand.Parameters.Add(@"id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original;
            sqlDASecond.UpdateCommand.Parameters.Add(@"email", SqlDbType.NVarChar, 30, "email");
            sqlDASecond.UpdateCommand.Parameters.Add(@"idProduct", SqlDbType.Int, 0, "idProduct");
            sqlDASecond.UpdateCommand.Parameters.Add(@"productName", SqlDbType.NVarChar, 30, "productName");

            //SQL DELETE
            sql = @"DELETE FROM Purchases WHERE id = @id";
            sqlDASecond.DeleteCommand = new SqlCommand(sql, sqlConSecond);
            sqlDASecond.DeleteCommand.Parameters.Add(@"id", SqlDbType.Int, 4, "id");

            sqlDASecond.Fill(DTSecond);
            await sqlConMain.OpenAsync();
        }




        


        public static DataTable dt1 = new DataTable();

        /// <summary>
        /// Получает список покупок клиента и выводит информацию в dataGid
        /// </summary>
        /// <param name="email"></param>
        public void CombineTables(string email)
        {
            //Формирование строки подключения к первой БД
            SqlConnectionStringBuilder ConStr1 = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MSSQLShopDB",
                IntegratedSecurity = true
            };
            //Формирование строки подключения ко второй БД
            SqlConnectionStringBuilder ConStr2 = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MSSQLPurchasesDB",
                IntegratedSecurity = true
            };
            //Формирование connection
            SqlConnection sqlCon1 = new SqlConnection(ConStr1.ConnectionString);
            SqlConnection sqlCon2 = new SqlConnection(ConStr2.ConnectionString);

            
            
            

            //Запросы SQL

                SqlDataAdapter da1 = new SqlDataAdapter();

                var sql = @"SELECT
                        Clients.id as 'ID',
                        Clients.firstName as 'FirstName',
                        Clients.lastName as 'LastName',
                        Clients.patronymic as 'Patronymic',
                        Clients.phoneNumber as 'PhoneNumber',
                        Clients.email as 'Email'
                        FROM Clients
                        WHERE Clients.email = @email";
                da1.SelectCommand = new SqlCommand(sql, sqlCon1);
            da1.SelectCommand.Parameters.Add(@"email", SqlDbType.NVarChar).Value = email;

                da1.Fill(dt1);



                SqlDataAdapter da2 = new SqlDataAdapter();

                sql = @"SELECT
                        Purchases.idProduct as 'IdProduct',
                        productName as 'ProductName'
                        FROM Purchases
                        WHERE Purchases.email = @email";
                da2.SelectCommand = new SqlCommand(sql, sqlCon2);
                da2.SelectCommand.Parameters.Add(@"email", SqlDbType.NVarChar).Value = email;

  
              da2.Fill(dt1);

        }
        /// <summary>
        /// Обновление данных клиентов
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public static void UpdateShopDB(
            int id,
            string firstName,
            string lastName,
            string patronymic,
            string phoneNumber,
            string email)
        {
            using (sqlConMain = new SqlConnection(conStrMain.ConnectionString))
            {
                var sql = @"UPDATE Clients 
                            SET 
                            firstName = @firstName,
                            lastName = @lastName,                    
                            patronymic = @patronymic,             
                            phoneNumber = @phoneNumber, 
                            email = @email
                        WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, sqlConMain);

                cmd.Parameters.Add(@"firstName", SqlDbType.NVarChar).Value = firstName;
                cmd.Parameters.Add(@"lastName", SqlDbType.NVarChar).Value = lastName;
                cmd.Parameters.Add(@"patronymic", SqlDbType.NVarChar).Value = patronymic;
                cmd.Parameters.Add(@"phoneNumber", SqlDbType.NVarChar).Value = phoneNumber;
                cmd.Parameters.Add(@"email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                sqlConMain.Open();
                cmd.ExecuteNonQuery();
                sqlConMain.Close();
            }
        }

        /// <summary>
        /// Обновление данных покупок
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="idProduct"></param>
        /// <param name="productName"></param>
        public static void UpdatePurchasesDB(int id, string email, int idProduct, string productName)
        {
            using (sqlConSecond = new SqlConnection(ConStrSecond.ConnectionString))
            {
                var sql = @"UPDATE Purchases
                       SET
                       email = @email,
                       idProduct = @idProduct,
                       productName = @productName
                       WHERE
                       id = @id";
                SqlCommand cmd = new SqlCommand(sql, sqlConSecond);

                cmd.Parameters.Add(@"email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add(@"idProduct", SqlDbType.Int).Value = idProduct;
                cmd.Parameters.Add(@"productName", SqlDbType.NVarChar).Value = productName;
                cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;

                sqlConSecond.Open();
                cmd.ExecuteNonQuery();
                sqlConSecond.Close();
            }
            
        }
        
    }
}
