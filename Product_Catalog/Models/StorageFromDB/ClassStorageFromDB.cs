using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassCatalog;
using System.IO;
using System.Data;


namespace ClassCatalog
{
    public class StorageFromDB : Storage
    {
        public StorageFromDB() 
        {            
            using (var connection = Sqlite.GetConnection()) 
            {
                connection.Open();
                string createTableCatalogSql = @"
                    CREATE TABLE IF NOT EXISTS units (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        description TEXT,
                        price REAL NOT NULL,
                        quantity INTEGER NOT NULL,
                        added_date TEXT NOT NULL
                        );";

                string createTableQuantityHistory = @"
                    CREATE TABLE IF NOT EXISTS quantity_history (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        unit_id INTEGER NOT NULL,
                        new_quantity INTEGER NOT NULL,
                        change_time TEXT NOT NULL,
                        FOREIGN KEY (unit_id) REFERENCES units(id)
                        );";

                using (var command = new SQLiteCommand(createTableCatalogSql, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand(createTableQuantityHistory, connection))
                {
                    command.ExecuteNonQuery();
                }

                EnsureStartId("units", 10000, connection);
            }

        }
        public override void SaveUnits(List<Unit> units)
        {

        }
        public override List<Unit> LoadUnits()
        {
            List<Unit> units = new List<Unit>();

            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();

                string sql = "SELECT * FROM units";
                using (var command = new SQLiteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        string discription = Convert.ToString(reader["description"]);
                        double price = Convert.ToDouble(reader["price"]);
                        int quantity = Convert.ToInt32(reader["quantity"]);
                        DateTime addedDate = Convert.ToDateTime(reader["added_date"]);

                        Unit unit = new Unit(id);
                        unit.Name = name;
                        unit.Description = discription;
                        unit.Price = price;
                        unit.Quantity = quantity;
                        unit.AddedDate = addedDate;

                        units.Add(unit);
                    }
                }
            }
            return units;
        }
        public override Unit InsertUnit(string name, string description, double price, int quantity)
        {
            int getId;
            DateTime addedDate = DateTime.Now;

            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string insertSql = @"INSERT INTO units (name, description, price, quantity, added_date) 
                                    VALUES (@name, @description, @price, @quantity, @added_date);
                                    ";
                using (var command = new SQLiteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@added_date", addedDate.ToString());
                    command.ExecuteNonQuery();

                }
                using (var getIdCommand = new SQLiteCommand("SELECT last_insert_rowid()", connection))
                {
                    getId = Convert.ToInt32(getIdCommand.ExecuteScalar());
                }
                Unit unit = new Unit(getId)
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Quantity = quantity,
                    AddedDate = addedDate
                };
                unit.QuantityHistory.Add($"час: {addedDate}:\t{quantity};");
                

                string insertSqlChangeQuantity = @"
                                    INSERT INTO quantity_history (unit_id, new_quantity, change_time) 
                                    VALUES (@unit_id, @new_quantity, @change_time)";
                using (var command = new SQLiteCommand(insertSqlChangeQuantity, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", unit.Id);
                    command.Parameters.AddWithValue("@new_quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@change_time", unit.AddedDate.ToString());
                    command.ExecuteNonQuery();
                }
                return unit;
            }
            
        }
        private void EnsureStartId (string tableName, int startFromId, SQLiteConnection connection)
        {
            string countSql = $"SELECT COUNT (*) FROM {tableName}";
            using (var countCmd = new SQLiteCommand(countSql, connection))
            {
                long count = (long)countCmd.ExecuteScalar();
                if ( count ==0 )
                {
                    string deleteSql = $"DELETE FROM sqlite_sequence WHERE name='{tableName}'";
                    using (var deleteCmd = new SQLiteCommand(deleteSql, connection))
                        deleteCmd.ExecuteNonQuery();

                    string insertSql = $"INSERT INTO sqlite_sequence (name, seq) VALUES ('{tableName}', {startFromId})";
                    using (var insertSmd = new SQLiteCommand(insertSql, connection))
                        insertSmd.ExecuteNonQuery();
                }
            }
        }

    }
}
