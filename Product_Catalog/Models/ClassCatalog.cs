using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassStorage;
using ClassUnit;

namespace ClassCatalog
{
    internal class Catalog
    {
        public List<Unit> units = new List<Unit>();        
        private int UnitId = 10001;
        Storage storage = new StorageFromFile();


        public Catalog(Storage storage)
        {
            this.storage = storage;
            units = storage.LoadUnits();
            UnitId = units.Count > 0 ? units[units.Count - 1].Id + 1 : 10001;
        }

        public void AddUnit(string name, string description, double price, int quantity)
        {
            Unit unit = new Unit { Id = UnitId++, Name = name, Description = description, Price = price, Quantity = quantity };
            units.Add(unit);
            DateTime time = DateTime.Now;
            unit.QuantityHistory.Add($"Початкова кількість: {quantity}; \t час: {time}");
            Console.WriteLine("Товар додадно.\n");
            unit.AddedDate = time;

        }
        public Unit GetUnitById(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit == null)
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");                
            }   
            return unit;
        }
        
       
        public void RemoveUnit(int id)
        {
            Unit unit = GetUnitById(id);
            
                units.Remove(unit);
                Console.WriteLine("Товар видалено\n");

        }
        public void FindUnit(string Query)
        {
            var found = units.FindAll(u => u.Name.IndexOf(Query, StringComparison.OrdinalIgnoreCase) >= 0);
            if (found.Count > 0)
            {
                foreach (var unit in found)
                {
                    unit.Info();
                }
            }
            else
            {
                Console.WriteLine("товар за запитом відсутній");
            }
            Console.WriteLine("для продовження натисніть 'enter'");           
        }

        ~Catalog()
        {
            storage.SaveUnits(units);
        }
    }
}
