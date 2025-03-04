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
        public void ShowMainManu ()
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
            catalog.ChangeQuantity(id, quantity);
        }
        public void ChangeUnitInfo()
        {
            Console.WriteLine("введіть артикул: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("введіть нове ім'я або enter щоб продовжити: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name))
            {
                catalog.ChangeName(id, name);
            }

            Console.WriteLine("змініть ціну або натисніть enter щоб продовжити: ");            
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                catalog.ChangePrice(id, price);
            }

            Console.WriteLine("введіть новий опис або enter щоб продовжити без змін: ");
            string description = Console.ReadLine();
            if (!string.IsNullOrEmpty(description))
            {
                catalog.ChangeDiscription(id, description);
            }
        }


                    
                   

                   /* case "5":
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

            }*/
    }
}
