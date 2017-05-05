using System;

namespace Eksamensopgave2017
{
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate;
        public DateTime SeasonEndDate;

        public SeasonalProduct(string name, int active, int canbeboughtoncredit, decimal price, int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay) : base(name, active, canbeboughtoncredit, price)
        {
            if (name == null || name == "")
                throw new ArgumentNullException();
            else
                this.Name = name;

            this.SeasonStartDate = new DateTime(startYear,startMonth,startDay);
            this.SeasonEndDate = new DateTime(endYear, endMonth, endDay);
            //Check if the product should be active or not
            if (this.SeasonStartDate < DateTime.Now && this.SeasonEndDate > DateTime.Now)
                this.Active = true;
            else
                this.Active = false;

            if (canbeboughtoncredit == 1)
                this.CanBeBoughtOnCredit = true;

            this.Price = price;
        }
    }
}