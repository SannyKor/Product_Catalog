using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassCatalog;

namespace ClassCatalog
{
    public class ClassStorageFromDB : Storage
    {
        public ClassStorageFromDB() 
        {            
            using (var connection = Sqlite.GetConnection()) 
            {
                connection.Open();
                string createTableSql = @"
                        CREATE TABLE IF NOT EXISTS units (
                        id INTEGER PRIMERY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        description TEXT,
                        price REAL NOT NULL,
                        quantity INTEGER NOT NULL,
                        added_data TEXT NOT NULL
                        );";
                using (var command = new SQLiteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
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

                        Unit unit = new Unit(id);
                        unit.Name = name;
                        unit.Description = discription;
                        unit.Price = price;
                        unit.Quantity = quantity;

                        units.Add(unit);
                    }
                }
            }
            return units;
        }

    }
}
