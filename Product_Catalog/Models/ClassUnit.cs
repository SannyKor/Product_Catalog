using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassCatalog
{
    public class Unit
    {
        //це шось страшне)
        //по перше статичне свойство живе весь час пока програма працюе
        //по друге можуть буди баги коли ми загружем з файла 
        //краще логіку с ід винести в католок

       // private static int _id = 10001;

        //public Unit()
        //{
        //    Id = _id++;
        //}
        public Unit(int Id)
        {
            this.Id = Id;
            //if (Id >= _id)
            //{
            //    _id = Id + 1;
            //}
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
