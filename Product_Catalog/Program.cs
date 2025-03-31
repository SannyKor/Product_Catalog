using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;


using ClassCatalog;

//using System.Text.Json;


namespace Product_Catalog
{
   

    


    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("windows-1251");
            Console.InputEncoding = Encoding.GetEncoding("windows-1251");

            Storage storage = new StorageFromFile();
            Catalog catalog = new Catalog(storage);
            ConsoleUI consoleUI = new ConsoleUI(catalog);

            consoleUI.RunMainMenu();

            
            
        }
    }
}