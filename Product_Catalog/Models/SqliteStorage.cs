using ClassCatalog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Catalog.Models
{
    public class SqliteStorage : Storage
    {
        public SqliteStorage()
        {
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();

                // Create a table
                string createTableSql = @"
                CREATE TABLE IF NOT EXISTS units (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    description TEXT,
                    price REAL NOT NULL,
                    quantity INTEGER NOT NULL,
                    added_date TEXT NOT NULL
                );";

                using (var command = new SQLiteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();

            }

            Random random = new Random();
          
            for (int i = 5; i < 1000; i++) {

                Unit unit = new Unit(1000+i);
                unit.Name = "tovar_" + i;
                unit.Description = "Description_" + i;
                unit.Price = random.Next(1, 1000);
                unit.Quantity = random.Next(1, 1000);
                unit.AddedDate = DateTime.Now;

                this.AddUnit(unit);
            }

           
        }
        public override List<Unit> LoadUnits()
        {
            List<Unit> units = new List<Unit>();

            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();

                // Read records
                string selectSql = "SELECT * FROM units LIMIT 100";
                using (var command = new SQLiteCommand(selectSql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        double price = Convert.ToDouble(reader["price"]);
                        int quantity = Convert.ToInt32(reader["quantity"]);
                        string addedDate = Convert.ToString(reader["added_date"]);

                        Unit unit = new Unit(id);
                        unit.Name = name;
                        unit.Price = price;
                        unit.Quantity = quantity;
                        unit.AddedDate = DateTime.Parse(addedDate);
                        units.Add(unit);
                    }
                }

                connection.Close();

            }

            return units;
        }

        public override void SaveUnits(List<Unit> units)
        {
           foreach(var unit in units)
           {
                this.AddUnit(unit);
           }
        }

        public void AddUnit(Unit unit)
        {
            if (this.UnitExist(unit))
            {
                //update unit
                return;
            }


            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();

                string sql = @"
                    INSERT INTO units (name, description, price, quantity, added_date)
                    VALUES (@name, @description, @price, @quantity, @added_date);
                ";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", unit.Name);
                    command.Parameters.AddWithValue("@description", unit.Description);
                    command.Parameters.AddWithValue("@price", unit.Price);
                    command.Parameters.AddWithValue("@quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@added_date", unit.AddedDate.ToString()); // ISO 8601 format

                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }

        public bool UnitExist(Unit unit)
        {
            int count = 0;
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();

                string selectSql = "SELECT COUNT(id) AS cnt FROM units WHERE id = @id";
                using (var command = new SQLiteCommand(selectSql, connection))
                {
                    command.Parameters.AddWithValue("@id", unit.Id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            count = Convert.ToInt32(reader["cnt"]);
                        }
                    }
                }
            }

            return count > 0;
        }

    }
}
