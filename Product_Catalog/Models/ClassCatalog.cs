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
        private List<Unit> units = new List<Unit>();
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
            unit.AddeDate = time;

        }
        public void ShowUnit(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                Console.WriteLine($"артикул: \t{unit.Id}");
                Console.WriteLine($"назва:\t\t{unit.Name}");
                Console.WriteLine($"кількість: \t{unit.Quantity}");
                Console.WriteLine($"опис: \t\t{unit.Description}");
                Console.WriteLine($"ціна: \t\t{unit.Price}\n");
            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ShowAllUnits()
        {
            if (units.Count != 0)
            {
                foreach (Unit unit in units)
                {
                    ShowUnit(unit.Id);
                }
                Console.WriteLine($"\nнатисніть 'enter' для продовження");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("в каталозі ще немає доданих товарів. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void RemoveUnit(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                units.Remove(unit);
                Console.WriteLine("Товар видалено\n");

            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ChangeQuantity(int id, int quantity)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                DateTime time = DateTime.Now;
                unit.QuantityHistory.Add($"{quantity}\t{time}");
                unit.Quantity = quantity;
                Console.WriteLine($"кількість змінено.\nнатисніть 'enter' для продовження");
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }

        }
        public void ChangeId(int id, int NewId)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                if(units.Exists(u => u.Id == NewId))
                {
                    Console.WriteLine("Товар з таким артикулом вже існує. натисніть 'enter' для продовження\n");
                    Console.ReadLine();
                    return;
                }
                unit.Id = NewId;
                Console.WriteLine($"новий артикул:\t{unit.Id}\nнатисніть 'enter' для продовження");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ChangeName(int id, string name)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                unit.Name = name;
                Console.WriteLine($"нове ім'я:\t{unit.Name}\nнатисніть 'enter' для продовження");
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ChangeDiscription(int id, string description)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                unit.Description = description;
                Console.WriteLine($"новий опис:\t{unit.Description}\nнатисніть 'enter' для продовження");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ChangePrice(int id, double price)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                unit.Price = price;
                Console.WriteLine($"нова ціна:\t{unit.Price}\nнатисніть 'enter' для продовження");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void ShowQuantityHistory(int id)
        {
            Unit unit = units.Find(u => u.Id == id);
            if (unit != null)
            {
                foreach (var quantity in unit.QuantityHistory)
                {
                    Console.WriteLine($"{quantity}\nнатисніть 'enter' для продовження\n");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Товар не знайдено. натисніть 'enter' для продовження\n");
                Console.ReadLine();
            }
        }
        public void FindUnit(string Query)
        {
            var found = units.FindAll(u => u.Name.IndexOf(Query, StringComparison.OrdinalIgnoreCase) >= 0);
            if (found.Count > 0)
            {
                foreach (var unit in found)
                {
                    ShowUnit(unit.Id);
                }
            }
            else
            {
                Console.WriteLine("товар за запитом відсутній");
            }
            Console.WriteLine("для продовження натисніть 'enter'");
            Console.ReadLine();
        }

        ~Catalog()
        {
            storage.SaveUnits(units);
        }
    }
}
