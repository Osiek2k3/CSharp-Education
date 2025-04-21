using System;
using System.Data.SqlClient;
using System.Data;


namespace Lab01_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            string localConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=C:\Users\Jakub\source\semestr_VI\NET_HW\lab\PABwTN_2025\Lab01_Framework\Data\Northwind.mdf;
                Integrated Security=True;
                Connect Timeout=5";

            string remoteConnectionString = @"Data Source=sever;
                Initial Catalog=Northwind;
                Integrated Security=True;
                Connect Timeout=5";

            TestConnection(localConnectionString, "Lokalna baza danych");

            TestConnection(remoteConnectionString, "Zdalna baza danych (błędny serwer)");

            Console.ReadLine();
        }

        static void TestConnection(string connectionString, string opis)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.StateChange += Connection_StateChange;

                try
                {
                    Console.WriteLine($"\nPróba połączenia: {opis}");

                    connection.Open();
                    Console.WriteLine("Połączenie otwarte.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd połączenia: {ex.Message}");
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        Console.WriteLine("Połączenie zamknięte.");
                    }
                }
            }
        }
        private static void Connection_StateChange(object sender, StateChangeEventArgs e)
        {
            Console.WriteLine($"Stan połączenia zmienił się z {e.OriginalState} na {e.CurrentState}");
        }
    }
}
