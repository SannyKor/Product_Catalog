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
        private readonly List<Unit> units = new List<Unit>();
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

                        Unit unit = new Unit(id)
                        {
                            Name = name,
                            Description = discription,
                            Price = price,
                            Quantity = quantity,
                            AddedDate = addedDate
                        };

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
                var saveQuantityHistory = new Unit.SaveQuantityChange(unit.Id, unit.Quantity, unit.AddedDate);
                unit.QuantityHistory.Add(saveQuantityHistory);


                string insertSqlChangeQuantity = @"
                                    INSERT INTO quantity_history (unit_id, new_quantity, change_time) 
                                    VALUES (@unit_id, @new_quantity, @change_time)";
                using (var command = new SQLiteCommand(insertSqlChangeQuantity, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", saveQuantityHistory.UnitId);
                    command.Parameters.AddWithValue("@new_quantity", saveQuantityHistory.NewUnitQuantity);
                    command.Parameters.AddWithValue("@change_time", saveQuantityHistory.DateOfChange);
                    command.ExecuteNonQuery();
                }
                return unit;
            }
            
        }
        public override Unit GetUnitById(int id)
        {          
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string sqlUnit = @"SELECT * FROM units WHERE id = @id";
                using (var command = new SQLiteCommand(sqlUnit, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Unit(Convert.ToInt32(reader["id"]))
                            {
                                Name = Convert.ToString(reader["name"]),
                                Description = Convert.ToString(reader["description"]),
                                Price = Convert.ToDouble(reader["price"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                AddedDate = Convert.ToDateTime(reader["added_date"])
                            };
                        }
                    }
                }
                
            }
            return null;
        }
        public override List<Unit.SaveQuantityChange> GetUnitQuantityHistory(int id)
        {
            List < Unit.SaveQuantityChange > quantityHistory = new List<Unit.SaveQuantityChange>();
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string sqlUnitQuantityHistory = @"SELECT * FROM quantity_history WHERE  unit_id = @unit_id";
                using (var command = new SQLiteCommand(sqlUnitQuantityHistory, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int quantity = Convert.ToInt32(reader["new_quantity"]);
                            DateTime dateOfChenge = Convert.ToDateTime(reader["change_time"]);
                            var saveQuantity = new Unit.SaveQuantityChange(id, quantity, dateOfChenge);
                            quantityHistory.Add(saveQuantity);
                        }
                        return quantityHistory;
                    }
                }
            }            
        }
        public override bool RemoveUnit(int id)
        {
            bool wasDelete;
            bool wasDeletedUnitHistory;
            Unit unit = GetUnitById(id);
            if (unit == null)
            {
                return false;
            }
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string deleteSql = @"DELETE FROM units WHERE id=@id";
                using (var command = new SQLiteCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    wasDelete = command.ExecuteNonQuery() > 0;
                }
                string historySql = @"
                    INSERT INTO quantity_history (unit_id, new_quantity, change_time)
                    VALUES (@unit_id, @new_quantity, @change_time)";
                using (var command = new SQLiteCommand(historySql, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", unit.Id);
                    command.Parameters.AddWithValue("@new_quantity", 0);
                    command.Parameters.AddWithValue("@change_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    wasDeletedUnitHistory = command.ExecuteNonQuery() > 0;
                }
            }
            if (wasDelete && wasDeletedUnitHistory)
                return true;
            else
                return false;
        }
        public override void UpdateUnit(Unit unit)
        {
            var oldUnit = GetUnitById(unit.Id);
            bool wasChangedQuantity = oldUnit.Quantity != unit.Quantity;
            DateTime dateTime = DateTime.Now;
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string updateUnitSql = @"UPDATE units SET 
                                    name = @name,
                                    description = @description,
                                    price = @price,
                                    quantity = @quantity
                                    WHERE id = @id";
                using (var command = new SQLiteCommand(updateUnitSql, connection))
                {
                    command.Parameters.AddWithValue("@name", unit.Name);
                    command.Parameters.AddWithValue("@description", unit.Description);
                    command.Parameters.AddWithValue("@price", unit.Price);
                    command.Parameters.AddWithValue("@quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@id", unit.Id);

                    command.ExecuteNonQuery();
                }

                if (wasChangedQuantity)
                {
                    string insertSqlChangeQuantity = @"
                                    INSERT INTO quantity_history (unit_id, new_quantity, change_time) 
                                    VALUES (@unit_id, @new_quantity, @change_time)";
                    using (var command = new SQLiteCommand(insertSqlChangeQuantity, connection))
                    {
                        command.Parameters.AddWithValue("@unit_id", unit.Id);
                        command.Parameters.AddWithValue("@new_quantity", unit.Quantity);
                        command.Parameters.AddWithValue("@change_time", dateTime);
                        command.ExecuteNonQuery();
                    }
                }
            }
            Unit unitInList = units.Find(u => u.Id == unit.Id);
            if (unitInList != null)
            { 
                unitInList.Name = unit.Name; 
                unitInList.Description = unit.Description;
                unitInList.Price = unit.Price;
                unitInList.Quantity = unit.Quantity;
                if (wasChangedQuantity)
                {
                    var saveQuantityHistory = new Unit.SaveQuantityChange(unit.Id, unit.Quantity, dateTime);
                    unitInList.QuantityHistory.Add(saveQuantityHistory);
                }
            }
        }
        public override List<Unit> FindUnit(string query)
        {            
            var foundResults = new List<Unit>();
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM units WHERE name LIKE @search COLLATE NOCASE";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@search", "%" + query + "%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var unit = new Unit(Convert.ToInt32(reader["id"]))
                            {
                                Name = Convert.ToString(reader["name"]),
                                Description = Convert.ToString(reader["description"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Price = Convert.ToDouble(reader["price"])
                            };
                            foundResults.Add(unit);
                        }
                    }
                }
            }
            return foundResults;
        }
        private void EnsureStartId (string tableName, int startFromId, SQLiteConnection connection)
        {
            string countSql = $"SELECT COUNT (*) FROM {tableName}";
            using (var countCmd = new SQLiteCommand(countSql, connection))
            {
                long count = (long)countCmd.ExecuteScalar();
                if ( count == 0 )
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
