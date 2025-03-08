using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassUnit
{
    internal class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public List<string> QuantityHistory { get; set; } = new List<string>();
        public void Info()
        {
            Console.WriteLine($"артикул: \t{Id}");
            Console.WriteLine($"назва:\t\t{Name}");
            Console.WriteLine($"кількість: \t{Quantity}");
            Console.WriteLine($"опис: \t\t{Description}");
            Console.WriteLine($"ціна: \t\t{Price}\n");
        }
    }
}
