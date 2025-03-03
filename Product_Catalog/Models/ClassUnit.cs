using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog;

namespace ClassUnit
{
    internal class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddeDate { get; set; } = DateTime.Now;
        public List<string> QuantityHistory { get; set; } = new List<string>();
    }
}
