using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;


using ClassCatalog;
using System.Data.SQLite;

//using System.Text.Json;


namespace Product_Catalog
{
   

    


    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("windows-1251");
            Console.InputEncoding = Encoding.GetEncoding("windows-1251");

            //Storage storage = new StorageFromFile();
            Storage storage = new StorageFromDB();
            Catalog catalog = new Catalog(storage);
            ConsoleUI consoleUI = new ConsoleUI(catalog);

            consoleUI.RunMainMenu();


            //Create a new SQLite database in memory(or use a file path like "Data Source=mydb.db")

            //using (var connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
            //{
            //    connection.Open();

            //    // Create a table
            //    string createTableSql = @"
            //    CREATE TABLE users (
            //        id INTEGER PRIMARY KEY AUTOINCREMENT,
            //        name TEXT NOT NULL,
            //        age INTEGER
            //    );";

            //    using (var command = new SQLiteCommand(createTableSql, connection))
            //    {
            //        command.ExecuteNonQuery();
            //    }

            //    // Insert a record
            //    string insertSql = "INSERT INTO users (name, age) VALUES (@name, @age)";
            //    using (var command = new SQLiteCommand(insertSql, connection))
            //    {
            //        command.Parameters.AddWithValue("@name", "Alice");
            //        command.Parameters.AddWithValue("@age", 30);
            //        command.ExecuteNonQuery();
            //    }

            //    // Read records
            //    string selectSql = "SELECT id, name, age FROM users";
            //    using (var command = new SQLiteCommand(selectSql, connection))
            //    using (var reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Age: {reader["age"]}");
            //        }
            //    }

            //    connection.Close();
            //}

        }
    }
}