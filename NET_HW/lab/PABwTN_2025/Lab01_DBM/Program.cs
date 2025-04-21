
namespace Lab01_DBM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=C:\Users\Jakub\source\semestr_VI\NET_HW\lab\PABwTN_2025\Lab01_Framework\Data\Northwind.mdf;
                Integrated Security=True;
                Connect Timeout=5";

            DatabaseManager dbManager = new DatabaseManager(connectionString);

            // Otwieranie połączenia
            dbManager.OpenConnection();

            // Zamknięcie połączenia
            dbManager.CloseConnection();

            Console.ReadLine();
        }
    }
}
