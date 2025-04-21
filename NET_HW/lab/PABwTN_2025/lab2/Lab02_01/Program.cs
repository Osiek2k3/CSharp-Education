using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_01
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".";
            builder.InitialCatalog = "test";
            builder.IntegratedSecurity = false;
            builder.UserID = "sa";
            builder.Password = "praktyka";
            builder.ConnectTimeout = 30;

            Console.WriteLine("Connection string variables:");
            Console.WriteLine($"DataSource: {builder.DataSource}");
            Console.WriteLine($"InitialCatalog: {builder.InitialCatalog}");
            Console.WriteLine($"UserID: {builder.UserID}");
            Console.WriteLine($"ConnectTimeout: {builder.ConnectTimeout}");

            string connectionString = builder.ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("otwarcie poloczenia");

                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = @"
                    BEGIN
                        IF OBJECT_ID('dbo.MyTable', 'U') IS NOT NULL
                            DROP TABLE dbo.MyTable;

                        CREATE TABLE dbo.MyTable (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            Name NVARCHAR(100),
                            Age INT
                        );

                        INSERT INTO dbo.MyTable (Name, Age) VALUES 
                            (N'Jan Kowalski', 30),
                            (N'Anna Nowak', 25),
                            (N'Piotr Wiśniewski', 28);
                    END;
                    ";

                    command.ExecuteNonQuery();
                    Console.WriteLine("Tabela zostal stworzona");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                    MessageBox.Show("zamkniecie poloczenia");
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

}