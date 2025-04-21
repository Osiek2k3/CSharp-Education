using Microsoft.Data.SqlClient;
using System;

class Program
{
    static string connectionString = "Server=.;Database=NORTHWND;User Id=sa;Password=praktyka;TrustServerCertificate=True;Connect Timeout=30;";

    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Connecting to SQL Server...");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to SQL Server.");

                ExecuteSpHelp(connection);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void ExecuteSpHelp(SqlConnection connection)
    {
        var command = new SqlCommand("sp_help dbo.Categories", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine("\nResults from sp_help dbo.Categories:");

            int resultIndex = 0;
            while (reader.Read())
            {
                if (resultIndex == 0)
                {
                    Console.WriteLine("\nTable Info:");
                    Console.WriteLine($"{reader["Name"]}: {reader["Type"]}");
                }
                else if (resultIndex == 1)
                {
                    Console.WriteLine("\nColumns Info:");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)} = {reader[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("\nOther Info:");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)} = {reader[i]}");
                    }
                }

                resultIndex++;
            }
        }
    }
}
