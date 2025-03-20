using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassCatalog
{
    public class Catalog
    {
        protected List<Unit> units = new List<Unit>();
        public IReadOnlyList<Unit> Units => units;
        private int UnitId;
        Storage storage = new StorageFromFile();

        


        public Catalog(Storage storage)
        {
            this.storage = storage;
            units = storage.LoadUnits();
            UnitId = units.Count > 0 ? units[units.Count - 1].Id + 1 : 10001;
        }

        public void AddUnit(string name, string description, double price, int quantity)
        {
            Unit unit = new Unit { Name = name, Description = description, Price = price, Quantity = quantity };
            units.Add(unit);
            DateTime time = DateTime.Now;
            unit.QuantityHistory.Add($"час: {time}:\t{quantity};");
            Console.WriteLine("Товар додадно.\n");
            unit.AddedDate = time;

        }
        public Unit GetUnitById(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit == null)
            {
                //Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");                
                return null;
            }   
            return unit;
        }
        
       
        public bool RemoveUnit(int id)
        {
            Unit unit = GetUnitById(id);
            if (unit == null)
            {
                return false;
            }
            else
            {
                units.Remove(unit);
                //Console.WriteLine("Товар видалено\n");
                return true;
            }

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
