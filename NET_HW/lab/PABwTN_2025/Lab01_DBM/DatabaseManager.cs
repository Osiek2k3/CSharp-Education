using Microsoft.Data.SqlClient;
using System.Data;

namespace Lab01_DBM
{
    public class DatabaseManager
    {
        private string connectionString;
        private SqlConnection connection;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    Console.WriteLine("Połączenie otwarte.");
                }
                else
                {
                    Console.WriteLine("Połączenie już jest otwarte.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd otwierania połączenia: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Połączenie zamknięte.");
                }
                else
                {
                    Console.WriteLine("Połączenie już jest zamknięte.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zamykania połączenia: {ex.Message}");
            }
        }
    }
}
