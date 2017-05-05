using System;

namespace Eksamensopgave2017
{
    public class Transaction
    {
        public Transaction(User user, DateTime date, decimal amount)
        {
            if (user == null)
                throw new ArgumentNullException();
            else
                this.user = user;
            this.Date = date;
            this.Amount = amount;
        }

        public readonly int Id = _nextID++;
        private static int _nextID = 1;
        public User user;
        public DateTime Date;
        public decimal Amount;

        public override string ToString()
        {
            return $"New transaction ({this.Id}): {this.user.Firstname} {this.user.Lastname} - DKK {this.Amount} - [{this.Date}]";
        }

        public virtual void Execute()
        {
        }
    }
}
