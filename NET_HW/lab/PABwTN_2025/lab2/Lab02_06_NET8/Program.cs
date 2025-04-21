using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString = "Server=.;Database=NORTHWND;User Id=sa;Password=praktyka;TrustServerCertificate=True;Connect Timeout=30;";

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Connecting string ... ");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to SQL Server.");

                Console.Write("Dropping and creating database 'SampleTestDB' ... ");
                DropAndCreateDatabase(connection);

                Console.WriteLine("Creating sample table with example data...");
                CreateTableAndInsertData(connection);

                while (true)
                {
                    ShowMenu();
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            InsertData(connection);
                            break;
                        case "2":
                            UpdateData(connection);
                            break;
                        case "3":
                            DeleteData(connection);
                            break;
                        case "4":
                            ReadData(connection);
                            break;
                        case "5":
                            Console.WriteLine("Disconnecting...");
                            return;
                        default:
                            Console.WriteLine("Invalid option, try again.");
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void DropAndCreateDatabase(SqlConnection connection)
    {
        var command = new SqlCommand("IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SampleTestDB') DROP DATABASE SampleTestDB;", connection);
        command.ExecuteNonQuery();

        command = new SqlCommand("CREATE DATABASE SampleTestDB;", connection);
        command.ExecuteNonQuery();
        Console.WriteLine("Database 'SampleTestDB' created.");
    }

    static void CreateTableAndInsertData(SqlConnection connection)
    {
        using (var switchDbCommand = new SqlCommand("USE SampleTestDB;", connection))
        {
            switchDbCommand.ExecuteNonQuery();
        }

        var createTableCommand = new SqlCommand(
            "CREATE TABLE Employees (" +
            "Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
            "Name NVARCHAR(30), " +
            "Location NVARCHAR(40));", connection);
        createTableCommand.ExecuteNonQuery();

        var insertDataCommand = new SqlCommand(
            "INSERT INTO Employees (Name, Location) VALUES " +
            "('Piotr', 'Austria'), " +
            "('Anna', 'Włochy'), " +
            "('Tomek', 'Polska');", connection);
        insertDataCommand.ExecuteNonQuery();

        Console.WriteLine("Sample table created and data inserted.");
    }

    static void ShowMenu()
    {
        Console.WriteLine("\nChoose an option:");
        Console.WriteLine("1 - Inserting a new row into table...");
        Console.WriteLine("2 - Updating rows in table...");
        Console.WriteLine("3 - Deleting rows from table...");
        Console.WriteLine("4 - Reading data from table...");
        Console.WriteLine("5 - Disconnect");
        Console.Write("Enter your choice: ");
    }

    static void InsertData(SqlConnection connection)
    {
        Console.WriteLine("\nInserting a new row into table...");
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter location: ");
        string location = Console.ReadLine();

        var insertCommand = new SqlCommand("INSERT INTO Employees (Name, Location) VALUES (@Name, @Location);", connection);
        insertCommand.Parameters.AddWithValue("@Name", name);
        insertCommand.Parameters.AddWithValue("@Location", location);
        insertCommand.ExecuteNonQuery();

        Console.WriteLine("Row inserted successfully.");
    }

    static void UpdateData(SqlConnection connection)
    {
        Console.WriteLine("\nUpdating rows in table...");
        Console.Write("Enter the ID of the employee to update: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter new name: ");
        string name = Console.ReadLine();
        Console.Write("Enter new location: ");
        string location = Console.ReadLine();

        var updateCommand = new SqlCommand(
            "UPDATE Employees SET Name = @Name, Location = @Location WHERE Id = @Id;", connection);
        updateCommand.Parameters.AddWithValue("@Id", id);
        updateCommand.Parameters.AddWithValue("@Name", name);
        updateCommand.Parameters.AddWithValue("@Location", location);
        updateCommand.ExecuteNonQuery();

        Console.WriteLine("Row updated successfully.");
    }

    static void DeleteData(SqlConnection connection)
    {
        Console.WriteLine("\nDeleting rows from table...");
        Console.Write("Enter the ID of the employee to delete: ");
        int id = int.Parse(Console.ReadLine());

        var deleteCommand = new SqlCommand("DELETE FROM Employees WHERE Id = @Id;", connection);
        deleteCommand.Parameters.AddWithValue("@Id", id);
        deleteCommand.ExecuteNonQuery();

        Console.WriteLine("Row deleted successfully.");
    }

    static void ReadData(SqlConnection connection)
    {
        Console.WriteLine("\nReading data from table...");
        var readCommand = new SqlCommand("SELECT Id, Name, Location FROM Employees;", connection);
        var reader = readCommand.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Location: {reader["Location"]}");
        }
        reader.Close();
    }
}
