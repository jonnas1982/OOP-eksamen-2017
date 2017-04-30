using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class Product
    {

        public Product(string name, int active, int canbeboughtoncredit, double price)
        {
            if (name == null) { throw new ArgumentNullException("This is not good"); }//TODO: Make real error! 
            else { this.Name = name; }

            if (active == 1) this.Active = true;
            if (active == 0) this.Active = false;

            if (canbeboughtoncredit == 1) this.CanBeBoughtOnCredit = true;

            this.Price = price;
        }
        public readonly int Id = _nextID++;
        private static int _nextID = 1;
        public string Name;
        public double Price;
        public bool Active;
        public bool CanBeBoughtOnCredit = false;

        public override string ToString()
        {
            return this.Id.ToString() + this.Name.ToString() + this.Price.ToString();
        }

    }
}