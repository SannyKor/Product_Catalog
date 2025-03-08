using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassCatalog;
using ClassUnit;

namespace ClassConsoleUI
{
    internal class ConsoleUI 
    {
        private Catalog catalog;
        public ConsoleUI(Catalog catalog)
        {
            this.catalog = catalog;
        }
        public void ShowMainMenu ()
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
        }
        public string MakeChoise()
        {
            string choise = Console.ReadLine();
            return choise;
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
            catalog.RemoveUnit(id);
        }
        public void ChangeQuantity()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("введіть кількість: ");
            int quantity = int.Parse(Console.ReadLine());
            catalog.GetUnitById(id).Quantity = quantity;
        }
        public void ChangeUnitInfo()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());
            Unit unit = catalog.GetUnitById(id);

            Console.WriteLine("введіть нове ім'я або enter щоб продовжити: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                unit.Name = name;
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
        public void ShowUnitInfo()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());            
            Unit unit = catalog.GetUnitById(id);
            if (unit != null)
            {
                unit.Info();
            }
        }
        public void ShowAllUnitsInfo(List <Unit> Units)
        {
            if (Units.Count == 0)
            {
                Console.WriteLine("в каталозі ще немає доданих товарів. натисніть 'enter' для продовження\n");
            }
            else
            {
                Console.WriteLine("ваш каталог: ");
                foreach (Unit unit in Units)
                {
                    unit.Info();
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
            catalog.FindUnit(query);
        }
        public void RunMainMenu()
        {
            while (true)
            {
                ShowMainMenu();
                string choise = MakeChoise();
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
                        ShowAllUnitsInfo(catalog.units);
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
