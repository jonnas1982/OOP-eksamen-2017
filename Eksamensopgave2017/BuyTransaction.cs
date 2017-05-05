using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave2017
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, Product product, DateTime date, decimal amount, List<User> userlist) : base(user, date, amount)
        {
            if (user == null)
                throw new ArgumentNullException();
            else
                this.user = user;

            this.product = product;
            this.Date = date;
            this.Amount = amount;
            this.UserList = userlist;
        }

        User user;
        Product product;
        DateTime Date;
        decimal Amount;
        List<User> UserList;

    public override string ToString()
    {
        return $"New purchase  ({this.Id}): {this.user.Firstname} {this.user.Lastname} - {this.Amount} - {this.product.Name} - [{this.Date}]";
    }

    public override void Execute()
        {
            if (this.user.Balance >= this.product.Price || this.product.CanBeBoughtOnCredit)
                {
                    decimal NewBalance = this.user.Balance - Amount;
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
            else
            {
                return;
            }    
        }
    }
}
