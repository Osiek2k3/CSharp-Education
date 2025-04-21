using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static string connectionString = "Server=.;Database=NORTHWND;User Id=sa;Password=praktyka;TrustServerCertificate=True;Connect Timeout=30;";

    static async Task Main(string[] args)
    {
        try
        {
            List<Task> tasks = new List<Task>();

            Console.WriteLine("Starting 500 concurrent insertions...");

            for (int i = 0; i < 500; i++)
            {
                int taskId = i + 1;
                tasks.Add(InsertCategoryAsync(taskId));
            }

            await Task.WhenAll(tasks);

            await DisplayCategoriesCountAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static async Task InsertCategoryAsync(int taskId)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string categoryName = $"Category_{taskId}";
            string description = $"Description for category {taskId}";

            var command = new SqlCommand("INSERT INTO Categories (CategoryName, Description) VALUES (@CategoryName, @Description)", connection);
            command.Parameters.AddWithValue("@CategoryName", categoryName);
            command.Parameters.AddWithValue("@Description", description);

            await command.ExecuteNonQueryAsync();
            Console.WriteLine($"Inserted Category_{taskId}");
        }
    }

    static async Task DisplayCategoriesCountAsync()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand("SELECT COUNT(*) FROM Categories", connection);
            var result = await command.ExecuteScalarAsync();
            Console.WriteLine($"Total records in Categories table: {result}");
        }
    }
}
