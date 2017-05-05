using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, DateTime date, decimal amount, List<User> userlist) : base(user, date, amount)
        {
            if (user == null)
                throw new ArgumentNullException();
            else
                this.user = user;

            this.Date = date;
            this.Amount = amount;
            this.UserList = userlist;
        }

        User user;
        DateTime Date = DateTime.Now;
        decimal Amount;
        List<User> UserList;

        public override string ToString()
        {
            return $"Cash insertet ({this.Id}): {this.user.Firstname} {this.user.Lastname} - DKK {this.Amount} - [{this.Date}]";
        }

        public override void Execute()
        {
            decimal NewBalance = this.user.Balance + Amount;
            foreach (var Item in this.UserList.Where(x => x.Id == user.Id))
            {
                Item.Balance = NewBalance;
            }
            FileWriters writer = new FileWriters();
            //The new information which have been written to the list will over write the old file
            writer.WriteToUserCsv("../../user.csv", UserList);
            //The transaction is written to the transaction file
            writer.WriteToTransactionCsv("../../transactions.csv", this.ToString());
        }
    }
}
