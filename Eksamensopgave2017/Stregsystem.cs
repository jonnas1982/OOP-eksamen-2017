using System;
using System.Collections.Generic;
using System.Linq;

namespace Eksamensopgave2017
{
    public class Stregsystem : IStregsystem
    {
        StregsystemCLI CLI = new StregsystemCLI();

        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                //Combine the to product lists after having checked if the season products should be activated or deactivated
                //It runs after every command, so this will keep them up to date
                List<Product> productList = new List<Product>();
                foreach (SeasonalProduct element in ReadFromCsv.SeasonalProductList)
                {
                    if(element.SeasonStartDate < DateTime.Now && element.SeasonEndDate > DateTime.Now)
                    {
                        element.Active = true;
                    }
                    else
                    {
                        element.Active = false;
                    }
                }

                foreach(var element in ReadFromCsv.SeasonalProductList.FindAll(x => x.Active == true)) productList.Add(element);
                foreach(var element in ReadFromCsv.ProductList.FindAll(x => x.Active == true)) productList.Add(element);
                //http://stackoverflow.com/questions/3386767/linq-orderby-query
                IEnumerable<Product> AktiveProductList = productList.OrderBy(x => x.ToString()).ToList();
                return AktiveProductList;
            }
        }

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount, List<User> UserList)
        {
            InsertCashTransaction Transaction = new InsertCashTransaction(user, DateTime.Now, amount, UserList);
            return Transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product, List<User> UserList, List<Product> ProductList)
        {
            BuyTransaction Transaction = new BuyTransaction(user, product, DateTime.Now, product.Price, UserList);
            return Transaction;
        }

        public Product GetProductByID(int productID, List<Product> ProductList)
        {
            //Check if the product exists. If it does check if it is active
            if(ProductList.Exists(x => x.Id == productID))
            {
                Product product = ProductList.Find(x => x.Id == productID);
                if (product.Active)
                {
                    return product;
                }
                else
                {
                    throw new InactiveProductException();
                }

            }
            else
            {
                throw new ProductDoesNotExsistException();
            }

        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            IEnumerable<Transaction> transactions = ReadFromCsv.TransactionList.Where(x => x.user == user).OrderByDescending(x => x.Date).Take(count);
            return transactions;
        }

        public User GetUserByUsername(string username, List<User> UserList)
        {

            if (UserList.Exists(x => x.Username == username))
            {
                User user = UserList.Find(x => x.Username == username);
                return user;
            }
            else
            {
                throw new InvalidUsernameException();
            }
        }

        public User GetUser(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        } //Not Done Yet
    }
}
        