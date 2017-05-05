using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
    public class Product
    {
        public List<Product> ProductList = new List<Product>();
        public Product(string name, int active, int canbeboughtoncredit, decimal price)
        {
            if (name == null || name == "")
                throw new ArgumentNullException();
            else
                this.Name = name;

            if (active == 1)
                this.Active = true;
            if (active == 0)
                this.Active = false;

            if (canbeboughtoncredit == 1)
                this.CanBeBoughtOnCredit = true;

            this.Price = price;
        }

        public readonly int Id = _nextID++;
        private static int _nextID = 1;
        public string Name;
        public decimal Price;
        public bool Active;
        public bool CanBeBoughtOnCredit = false;

        public override string ToString() //Make other return type
        {
            return $"{this.Name.ToString()} {this.Price.ToString()}  ({this.Id.ToString()}) \n";
        }

        public int CompareTo(Product other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}