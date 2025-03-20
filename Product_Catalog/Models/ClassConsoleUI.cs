using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassCatalog;


namespace ClassCatalog

{
    public class ConsoleUI
    {
        private Catalog catalog;
        public ConsoleUI(Catalog catalog)
        {
            this.catalog = catalog;
        }
        
        
        public void CreateNewUnit()
        {
            Console.WriteLine("введіть ім'я: ");
            string name = Console.ReadLine();
            Console.WriteLine("введіть кількість: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("введіть ціну: ");
            double prise = double.Parse(Console.ReadLine());
            Console.WriteLine("введіть опис: ");
            string description = Console.ReadLine();
            catalog.AddUnit(name, description, prise, quantity);
        }

        public void RemoveUnit()
        {
            Console.WriteLine("введіть артикул товару для видалення: ");
            int id = int.Parse(Console.ReadLine());

            if (catalog.RemoveUnit(id))
            {
                Console.WriteLine("Товар видалено\n");
            }
            else
            {
                Console.WriteLine("Товар не знайдено\n");
            }
        }
        public void ChangeQuantity()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("введіть кількість: ");
            int quantity = int.Parse(Console.ReadLine());
            Unit unit = catalog.GetUnitById(id);
            if (unit == null)
            {
                Console.WriteLine("товар не знайдено\n");
                return;
            }
            else
            {
                unit.Quantity = quantity;
                DateTime time = DateTime.Now;
                unit.QuantityHistory.Add($"час: {time}:\t{quantity};");
            }
        }
        public void ChangeUnitInfo()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());
            Unit unit = catalog.GetUnitById(id);

            if (unit == null)
            {
                Console.WriteLine("товар не знайдено\n");
                return;
            }
            else
            {
                Console.WriteLine("введіть нове ім'я або enter щоб продовжити: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {

                    if (catalog.Units.Any(u => u.Name == name))
                    {
                        Console.WriteLine("товар з таким ім'ям вже існує. введіть інше ім'я або enter щоб продовжити\n");
                        return;
                    }
                    else
                    {
                        unit.Name = name;
                    }
                }


                Console.WriteLine("змініть ціну або натисніть enter щоб продовжити: ");
                if (double.TryParse(Console.ReadLine(), out double price))
                {
                    unit.Price = price;
                }

                Console.WriteLine("введіть новий опис або enter щоб продовжити без змін: ");
                string description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                {
                    unit.Description = description;
                }
            }
        }
        public void UnitInfo(Unit unit)
        {
            Console.WriteLine($"артикул: \t{unit.Id}");
            Console.WriteLine($"назва:\t\t{unit.Name}");
            Console.WriteLine($"кількість: \t{unit.Quantity}");
            Console.WriteLine($"опис: \t\t{unit.Description}");
            Console.WriteLine($"ціна: \t\t{unit.Price}\n");
        }
        public void ShowUnitInfo()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());            
            Unit unit = catalog.GetUnitById(id);
            if (unit == null)
            {
                Console.WriteLine("товар не знайдено\n");
            }

            UnitInfo(unit);
            

        }
        public void ShowAllUnitsInfo(IEnumerable <Unit> Units)
        {
            if (catalog.Units.Count == 0)
            {
                Console.WriteLine("в каталозі ще немає доданих товарів. натисніть 'enter' для продовження\n");
            }
            else
            {
                Console.WriteLine("ваш каталог: ");
                foreach (Unit unit in Units)
                {
                    UnitInfo(unit);
                }
            }
        }
        public void ShowUnitQuantityHistory()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());
            Unit unit = catalog.GetUnitById(id);

            foreach (var quantity in unit.QuantityHistory)
            {
                Console.WriteLine($"{quantity}\nнатисніть 'enter' для продовження\n");                
            }
        }
        public void FindUnitByName()
        {
            Console.WriteLine("введіть запит:");
            string query = Console.ReadLine();
            List<Unit> found = catalog.FindUnit(query);

            if (found.Count > 0)
            {
                foreach (var unit in found)
                {
                    UnitInfo(unit);
                }
            }
            else
            {
                Console.WriteLine("товар за запитом відсутній");
            }
        }
        public void RunMainMenu()
        {
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
                        CreateNewUnit();
                        break;
                    case "2":
                        RemoveUnit();
                        break;
                    case "3":
                        ChangeQuantity();
                        break;
                    case "4":
                        ChangeUnitInfo();
                        break;
                    case "5":
                        ShowUnitInfo();
                        break;
                    case "6":
                        ShowAllUnitsInfo(catalog.Units);
                        break;
                    case "7":
                        ShowUnitQuantityHistory();
                        break;
                    case "8":
                        FindUnitByName();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("невірний вибір. спробуйте ще раз\n");
                        break;
                }
            }
        }

    }
}
