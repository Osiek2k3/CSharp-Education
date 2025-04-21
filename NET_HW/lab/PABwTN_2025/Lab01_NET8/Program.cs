using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Lab01_NET8
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=C:\Users\Jakub\source\semestr_VI\NET_HW\lab\PABwTN_2025\Lab01_Framework\Data\Northwind.mdf;
                Integrated Security=True;
                Connect Timeout=5";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.StateChange += Connection_StateChange;

                try
                {
                    Console.WriteLine("Próba połączenia z bazą danych...");

                    connection.Open();
                    Console.WriteLine("Połączenie otwarte.");

                    SqlCommand command = new SqlCommand("SELECT * FROM Products", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"Produkt: {reader["ProductName"]}, Cena: {reader["UnitPrice"]}");
                    }
                    reader.Close();
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
