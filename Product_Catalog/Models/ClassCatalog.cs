using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
            Unit unit = new Unit() { Name = name, Description = description, Price = price, Quantity = quantity };

            units.Add(unit);
            DateTime time = DateTime.Now;
            unit.QuantityHistory.Add($"час: {time}:\t{quantity};");
            Console.WriteLine("Товар додадно.\n");
            unit.AddedDate = time;
            using (var connection = Sqlite.GetConnection())
            {
                connection.Open();
                string insertSql = "INSERT INTO units (name, description, price, quntity) VALUES (@name, @description, @price, @quantity)";
                using (var command = new SQLiteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@name", unit.Name);
                    command.Parameters.AddWithValue("@description", unit.Description);
                    command.Parameters.AddWithValue("@price", unit.Price);
                    command.Parameters.AddWithValue("@quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@added_data", time.ToString());
                    command.ExecuteNonQuery();
                }
                string insertSqlChangeQuantity = "INSERT INTO quantity_history (unit_id, new_quantity, change_time) VALUES (@unit_id, new_quantity, change_time)";
                using (var command = new SQLiteCommand(insertSqlChangeQuantity, connection))
                {
                    command.Parameters.AddWithValue("@unit_id", unit.Id);
                    command.Parameters.AddWithValue("@new_quantity", unit.Quantity);
                    command.Parameters.AddWithValue("@change_time", time.ToString());
                    command.ExecuteNonQuery();
                }
            }
            
        }
        public Unit GetUnitById(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            return unit;
        }


        public bool RemoveUnit(int id)
        {
            Unit unit = GetUnitById(id);
            /*if (unit == null)
            {
                return false;
            }*/

            return units.Remove(unit);


        }
    
        
        public List<Unit> FindUnit(string Query)
        {
            var found = units.FindAll(u => u.Name.IndexOf(Query, StringComparison.OrdinalIgnoreCase) >= 0);
            return found;

        }

        ~Catalog()
        {
            storage.SaveUnits(units);
        }
    }
}
