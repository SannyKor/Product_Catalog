using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassCatalog
{
    public class Unit
    {
        
        public Unit()
        {
             
        }
        public Unit(int Id)
        {
            this.Id = Id;             
        }
        public int Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public List<string> QuantityHistory { get; set; } = new List<string>();
        
    }
}
