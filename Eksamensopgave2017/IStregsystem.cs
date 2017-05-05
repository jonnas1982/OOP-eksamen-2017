using System;
using System.Collections.Generic;

namespace Eksamensopgave2017
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount, List<User> UserList);
        BuyTransaction BuyProduct(User user, Product product, List<User> UserList, List<Product> ProductList);
        Product GetProductByID(int productID, List<Product> ProductList);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Func<User, bool> predicate);
        User GetUserByUsername(string username, List<User> UserList);
    }
}