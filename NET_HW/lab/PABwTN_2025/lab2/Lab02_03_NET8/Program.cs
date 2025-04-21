using Microsoft.Data.SqlClient;

namespace Lab02_03_NET8
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @".";
            builder.InitialCatalog = "NORTHWND";
            builder.IntegratedSecurity = false;
            builder.UserID = "sa";
            builder.Password = "praktyka";
            builder.ConnectTimeout = 30;
            builder.TrustServerCertificate = true;

            string connectionString = builder.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Połączenie otwarte.");

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT TABLE_NAME 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_TYPE = 'BASE TABLE';
                    ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Tabele w bazie:");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["TABLE_NAME"]);
                        }
                    }

                    command = connection.CreateCommand();
                    command.CommandText = @"
                        select productid, productname, unitprice, unitsInStock from Products;
                    ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"ID: {reader["productid"]}, " +
                                $"Nazwa: {reader["productname"]}, " +
                                $"Cena: {reader["unitprice"]}, " +
                                $"Ilość na stanie: {reader["unitsInStock"]}"
                            );
                        }
                    }

                    command = connection.CreateCommand();
                    command.CommandText = @"
                        select CategoryName from Categories;
                    ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(
                                $"Nazwa: {reader["CategoryName"]}"
                            );
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                    Console.WriteLine("Połączenie zamknięte.");
                }
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
            Console.ReadKey();
        }
    }
}
