using Microsoft.Data.SqlClient;
using System;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=.;Database=NORTHWND;User Id=sa;Password=praktyka;TrustServerCertificate=True;Connect Timeout=30;";

        using (var connection = new SqlConnection(connectionString))
        {
            GetSchemaInfo(connection);
        }
    }

    static void GetSchemaInfo(SqlConnection connection)
    { 
        SqlCommand command = new SqlCommand("SELECT CategoryID, CategoryName FROM Categories;", connection);

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        DataTable schemaTable = reader.GetSchemaTable();

        foreach (DataRow row in schemaTable.Rows)
        {
            foreach (DataColumn column in schemaTable.Columns)
            {
                Console.WriteLine($"{column.ColumnName} = {row[column]}");
            }
        }

        reader.Close();
    }
}
