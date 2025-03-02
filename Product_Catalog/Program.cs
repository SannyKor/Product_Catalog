using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
//using System.Text.Json;


namespace Catalog
{
    class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddeDate { get; set; } = DateTime.Now;
        public List<string> QuantityHistory { get; set; } = new List<string>();


    }

    class Catalog
    {
        private List<Unit> units = new List<Unit>();
        private int UnitId = 10001;
        private const string FileName = "catalog.bin";

        public Catalog() 
        {
            LoadFromFile();
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
        private void SaveToFile()
        {
            //var json = JsonSerializer.Serialize(units, new JsonSerializerOptions { WriteIndented = true });
            //File.WriteAllText(FileName, json);
            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
                {
                    writer.Write(units.Count);
                    foreach (var unit in units)
                    {
                        writer.Write(unit.Id);
                        writer.Write(unit.Name);
                        writer.Write(unit.Description);
                        writer.Write(unit.Price);
                        writer.Write(unit.Quantity);
                        writer.Write(unit.AddeDate.Ticks);
                        writer.Write(unit.QuantityHistory.Count);
                        foreach (var history in unit.QuantityHistory)
                        {
                            writer.Write(history);
                        }
                    }
                }
            }
            
        }
        private void LoadFromFile()
        {
            if (File.Exists(FileName))
            {
                //var json = File.ReadAllText(FileName);
                //units = JsonSerializer.Deserialize<List<Unit>>(json);
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
                    {
                        int count = reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            Unit unit = new Unit
                            {
                                Id = reader.ReadInt32(),
                                Name = reader.ReadString(),
                                Description = reader.ReadString(),
                                Price = reader.ReadDouble(),
                                Quantity = reader.ReadInt32(),
                                AddeDate = new DateTime(reader.ReadInt64()),
                            };
                            int historyCount = reader.ReadInt32();
                            for (int j = 0; j < historyCount; j++)
                            {
                                unit.QuantityHistory.Add(reader.ReadString());
                            }
                            units.Add(unit);
                        }
                    }
                }
            }
        }
        ~Catalog()
        {
            SaveToFile();
        }
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("windows-1251");
            Console.InputEncoding = Encoding.GetEncoding("windows-1251");

            Catalog catalog = new Catalog();
            

            while (true)
            {
                Console.WriteLine("виберіть один із варіантів: " +
                    "\n1. додати новий товар; " +
                    "\n2. видалити товар; " +
                    "\n3. змінити кількість; " +
                    "\n4. змінити інформацію про товар; " +
                    "\n5. вивести інформацію про товар;" +
                    "\n6. показати весь каталог;" +
                    "\n7. показати рух кількості по товару;" +
                    "\n8. знайти по назві або частині назви;" +
                    "\n9. вийти;\n");
                string choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":

                        Console.WriteLine("введіть ім'я: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("введіть кількість: ");
                        int quantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("введіть ціну: ");
                        double prise = double.Parse(Console.ReadLine());
                        Console.WriteLine("введіть опис: ");
                        string description = Console.ReadLine();
                        catalog.AddUnit(name, description, prise, quantity);
                        break;
                    case "2":
                        Console.WriteLine("введіть артикул товару для видалення: ");
                        int id = int.Parse(Console.ReadLine());
                        catalog.RemoveUnit(id);
                        break;
                    case "3":
                        Console.WriteLine("введіть артикул: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("введіть кількість: ");
                        quantity = int.Parse(Console.ReadLine());
                        catalog.ChangeQuantity(id, quantity);
                        break;
                    case "4":
                        Console.WriteLine("введіть артикул: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("виберіть варіант: " +
                            "\n1. змінити ім'я;" +
                            "\n2. змінити ціну;" +
                            "\n3. змінити опис;\n");
                        int changeChoise = int.Parse(Console.ReadLine());
                        if (changeChoise == 1)
                        {
                            Console.WriteLine("введіть нове ім'я: ");
                            name = Console.ReadLine();
                            catalog.ChangeName(id, name);
                        }
                        else if (changeChoise == 2)
                        {
                            Console.WriteLine("введіть нову ціну: ");
                            prise = double.Parse(Console.ReadLine());
                            catalog.ChangePrice(id, prise);
                        }
                        else if (changeChoise == 3)
                        {
                            Console.WriteLine("Додайте новий опис: ");
                            description = Console.ReadLine();
                            catalog.ChangeDiscription(id, description);
                        }
                        else
                        {
                            Console.WriteLine("невірний ввибір!");
                        }
                        break;

                    case "5":
                        Console.WriteLine("введіть артикул: ");
                        id = int.Parse(Console.ReadLine());
                        catalog.ShowUnit(id);
                        break;

                    case "6":
                        Console.WriteLine("ваш каталог: ");
                        catalog.ShowAllUnits();
                        break;
                    case "7":
                        Console.WriteLine("введіть артикул: ");
                        id = int.Parse(Console.ReadLine());
                        catalog.ShowQuantityHistory(id);
                        break;
                    case "8":
                        Console.WriteLine("введіть запит:");
                        string query = Console.ReadLine();
                        catalog.FindUnit(query);
                        break;
                    case "9":
                        return;

                    default:
                        Console.WriteLine("невірний вибір!");
                        break;
                }

            }


        }
    }
}