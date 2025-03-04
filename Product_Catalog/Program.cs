using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using ClassStorage;
using ClassUnit;
using ClassCatalog;
using ClassConsoleUI;
//using System.Text.Json;


namespace Product_Catalog
{
   

    


    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("windows-1251");
            Console.InputEncoding = Encoding.GetEncoding("windows-1251");

            Catalog catalog = new Catalog();
            ConsoleUI consoleUI = new ConsoleUI(catalog);


            while (true)
            {
                consoleUI.ShowMainManu();
                    //1. додати новий товар;
                    //2. видалити товар;
                    //3. змінити кількість;
                    //4. змінити інформацію про товар;
                    //5. вивести інформацію про товар;
                    //6. показати весь каталог;
                    //7. показати рух кількості по товару;
                    //8. знайти по назві або частині назви;
                    //9. вийти;
                string choise = consoleUI.MakeChoise();

                switch (choise)
                {
                    case "1":
                        consoleUI.CreateNewUnit();
                        break;

                    case "2":
                        consoleUI.RemoveUnit();
                        break;

                    case "3":
                        consoleUI.ChangeQuantity();
                        break;

                    case "4":
                        consoleUI.ChangeUnitInfo();
                        break;

                    /*case "5":
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
                        break;*/
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