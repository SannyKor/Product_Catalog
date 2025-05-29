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
            Unit unit = storage.InsertUnit(name, description, price, quantity);
            var saveQuantityHistory = new Unit.SaveQuantityChange(unit.Id, unit.Quantity, DateTime.Now);
            unit.QuantityHistory.Add(saveQuantityHistory);
            units.Add(unit);

        }
        public Unit GetUnitById(int id)
        {
            //Unit unit = units.Find(u => u.Id == id);
            return storage.GetUnitById(id);                        
        }


        public bool RemoveUnit(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit == null)
            {
                return false;
            }            
                return storage.RemoveUnit(id) && units.Remove(unit);
        }
    
        
        public List<Unit> FindUnit(string query)
        {
            /*var found = units
                .FindAll(u => u.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(u => u.Id).ToList();
            return found;*/
            return storage.FindUnit(query);
        }
        public void UpdateUnit (Unit unit)
        {      
            int id = unit.Id;
            storage.UpdateUnit(unit);
            Unit updatingUnitInList = units.Find(u => u.Id == id);
            updatingUnitInList.Name = unit.Name;
            updatingUnitInList.Price = unit.Price;
            updatingUnitInList.Quantity = unit.Quantity;
            updatingUnitInList.Description = unit.Description;

            var saveQuantityHistory = new Unit.SaveQuantityChange(unit.Id, unit.Quantity, DateTime.Now);
            unit.QuantityHistory.Add(saveQuantityHistory);
        }
        public List<Unit.SaveQuantityChange> GetUnitQuantityHistory(int id)
        {
            return storage.GetUnitQuantityHistory(id);
        }

        ~Catalog()
        {
            //storage.SaveUnits(units);
        }
    }
}
