using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassCatalog;


namespace ClassCatalog
{
    public class Catalog
    {
        
    

        protected List<Unit> units = new List<Unit>();
        public IReadOnlyList<Unit> Units => units;
        //private int UnitId;
        protected Storage storage;// = new StorageFromFile();




        public Catalog(Storage storage)
        {
            this.storage = storage;
            units = storage.LoadUnits();            
        }

        //protected int GetNextId()
        //{
        //    return units.Count > 0 ? units[units.Count - 1].Id + 1 : 10001;
        //}

        public void AddUnit(string name, string description, double price, int quantity)
        {
            //Unit unit = new Unit(GetNextId()) { Name = name, Description = description, Price = price, Quantity = quantity };
            Unit tempUnit = new Unit() { 
                    Name = name, 
                    Description = description, 
                    Price = price, 
                    Quantity = quantity };

            int getId;
            
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string insertSql = @"INSERT INTO units (name, description, price, quantity, added_date) 
                                    VALUES (@name, @description, @price, @quantity, @added_date);
                                    SELECT last_insert_rowid()";
                using (var command = new SQLiteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@name", tempUnit.Name);
                    command.Parameters.AddWithValue("@description", tempUnit.Description);
                    command.Parameters.AddWithValue("@price", tempUnit.Price);
                    command.Parameters.AddWithValue("@quantity", tempUnit.Quantity);
                    command.Parameters.AddWithValue("@added_date", tempUnit.AddedDate.ToString());
                    command.ExecuteNonQuery();
                    getId = Convert.ToInt32(command.ExecuteScalar());
                }
                Unit unit = new Unit(getId)
                {
                    Name = tempUnit.Name,
                    Description = tempUnit.Description,
                    Price = tempUnit.Price,
                    Quantity = tempUnit.Quantity                     
                };
                unit.QuantityHistory.Add($"час: {tempUnit.AddedDate}:\t{quantity};");
                units.Add(unit);

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
            }            
        }
        public Unit GetUnitById(int id)
        {
            //Unit unit = units.Find(u => u.Id == id);
            using (var connection = new SQLiteConnection(Sqlite.GetConnection()))
            {
                connection.Open();
                string sql = "SELECT * FROM units WHERE id = @id";
                using (var command = new SQLiteCommand(sql, connection))
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
                                Price = Convert.ToInt32(reader["price"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                            };
                        }
                    }
                }
            }
            return null;            
        }


        public bool RemoveUnit(int id)
        {
            Unit unit = GetUnitById(id);
            if (unit == null)
            {
                return false;
            }

            using (var connection = new SQLiteConnection(Sqlite.GetConnection()))
            {
                connection.Open();
                string deleteSql = "DELETE FROM units WHERE id=@id";
                using (var command = new SQLiteCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                string historySql = @"
                    INSERT INTO quantity_history (unit_id, new_quantity, change_time)
                    VALUES (@unit_id, @new_quantity, @change_time)";
                using (var command = new SQLiteCommand(historySql, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", unit.Id);
                    command.Parameters.AddWithValue("@new_quantity", 0);
                    command.Parameters.AddWithValue("@change_time", DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"));
                    command.ExecuteNonQuery();
                }
            }
                return units.Remove(unit);
        }
    
        
        public List<Unit> FindUnit(string query)
        {
            /*var found = units
                .FindAll(u => u.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(u => u.Id).ToList();
            return found;*/
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
                        while(reader.Read())
                        {
                            var unit = new Unit(Convert.ToInt32(reader["id"]))
                            {
                                Name = Convert.ToString(reader["name"]),
                                Description = Convert.ToString(reader["description"]),
                                Quantity = Convert.ToInt32(reader ["quantity"]),
                                Price = Convert.ToDouble(reader["price"])
                            };
                            foundResults.Add(unit);
                        }
                    }
                }

            }
            return foundResults;
        }
        public void UpdateUnit (Unit unit)
        {
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string updateSql = @"UPDATE units SET 
                                    name = @name,
                                    description = @description,
                                    price = @price,
                                    quantity = @quantity,
                                WHERE id = @id";
                using (var command = new SQLiteCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@name", unit.Name);
                    command.Parameters.AddWithValue("@description", unit.Description);
                    command.Parameters.AddWithValue("@price", unit.Price);
                    command.Parameters.AddWithValue("@quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@id", unit.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        ~Catalog()
        {
            //storage.SaveUnits(units);
        }
    }
}
