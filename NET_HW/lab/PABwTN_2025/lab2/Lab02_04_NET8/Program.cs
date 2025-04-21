using Microsoft.Data.SqlClient;

string connectionString = "Server=.;Database=NORTHWND;User Id=sa;Password=praktyka;TrustServerCertificate=True;Connect Timeout=30;";


void InsertCategory()
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("INSERT INTO Categories (CategoryName) VALUES ('Nowa Kategoria')", connection);
    int affectedRows = command.ExecuteNonQuery();
    Console.WriteLine($"Dodano {affectedRows} wierszy.");
}

void CountCategories()
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("SELECT COUNT(*) FROM Categories", connection);
    int count = (int)command.ExecuteScalar();
    Console.WriteLine($"Liczba kategorii: {count}");
}

void AddCategoryWithProcedure(string categoryName)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("AddCategory", connection);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    command.Parameters.AddWithValue("@CategoryName", categoryName);

    command.ExecuteNonQuery();
    Console.WriteLine("Dodano kategorię przez procedurę.");
}

void GetCategoryCount(out int count)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("GetCategoryCount", connection);
    command.CommandType = System.Data.CommandType.StoredProcedure;

    var outputParam = new SqlParameter("@Count", System.Data.SqlDbType.Int)
    {
        Direction = System.Data.ParameterDirection.Output
    };
    command.Parameters.Add(outputParam);

    command.ExecuteNonQuery();
    count = (int)outputParam.Value;
}

void FindProductsByCategory(int categoryId)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("SELECT ProductName FROM Products WHERE CategoryID = @CategoryID", connection);
    command.Parameters.AddWithValue("@CategoryID", categoryId);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine(reader["ProductName"]);
    }
}

void GetCategoryNameById(int categoryId, out string categoryName)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("SELECT @CategoryName = CategoryName FROM Categories WHERE CategoryID = @CategoryID", connection);
    command.Parameters.AddWithValue("@CategoryID", categoryId);

    var outputParam = new SqlParameter("@CategoryName", System.Data.SqlDbType.NVarChar, 50)
    {
        Direction = System.Data.ParameterDirection.Output
    };
    command.Parameters.Add(outputParam);

    command.ExecuteNonQuery();
    categoryName = outputParam.Value?.ToString();
}

void TransactionExample()
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
    var transaction = connection.BeginTransaction();

    try
    {
        var command1 = new SqlCommand("UPDATE Products SET UnitPrice = UnitPrice + 1 WHERE ProductID = 1", connection, transaction);
        command1.ExecuteNonQuery();

        var command2 = new SqlCommand("UPDATE Products SET UnitPrice = UnitPrice + 1 WHERE ProductID = 2", connection, transaction);
        command2.ExecuteNonQuery();

        transaction.Commit();
        Console.WriteLine("Transakcja zakończona sukcesem.");
    }
    catch
    {
        transaction.Rollback();
        Console.WriteLine("Transakcja wycofana.");
    }
}

void GetCustomerOrderHistory(string customerId)
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();

    var command = new SqlCommand("CustOrderHist", connection);
    command.CommandType = System.Data.CommandType.StoredProcedure;
    command.Parameters.AddWithValue("@CustomerID", customerId);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"{reader["ProductName"]} - {reader["Total"]}");
    }
}

InsertCategory();
CountCategories();
AddCategoryWithProcedure("Przykładowa Kategoria");

GetCategoryCount(out int count);
Console.WriteLine($"Liczba kategorii z procedury: {count}");

FindProductsByCategory(1);

GetCategoryNameById(1, out string categoryName);
Console.WriteLine($"Nazwa kategorii ID=1: {categoryName}");

TransactionExample();

GetCustomerOrderHistory("ALFKI");
