using System;

namespace Eksamensopgave2017
{

    public class StregsystemCLI : IStregsystemUI
    {
        public void Close()
        {
            _running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The admin command '{adminCommand}' could not be found");
        }

        public void DisplayGeneralError(string errorString)
        {
            throw new NotImplementedException();
        }

        public void DisplayInactiveProduct(Product product)
        {
            Console.WriteLine($"'{product.Name}' is inactive (ID: {product.Id})");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"{user.Firstname} {user.Lastname} does not have sufficient cash to buy {product.Name} \n{user.Firstname} have a balance of {user.Balance}");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Product not found");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"The following command contans to many arguments: {command}");
        }

        public void DisplayTooFewArgumentsError()
        {
            Console.WriteLine($"To few arguments");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.user.Firstname} bought a product\nUser balance: {transaction.user.Balance}");
        }

        public void DisplayUserInfo(User user)
        {
            Stregsystem sSystem = new Stregsystem();
            //http://stackoverflow.com/a/5344836
            //http://stackoverflow.com/a/10883477
            Console.WriteLine($"{user.Username}, {user.Firstname} {user.Firstname}, balance: {user.Balance}");
            foreach (var element in sSystem.GetTransactions(user, 10))
            {
                Console.WriteLine(element);
            }
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User {username} does not exist");
        }

        public void DisplayAktiveProducts()
        {
            Console.Clear();
            Stregsystem ssystem = new Stregsystem();
            foreach (var Item in ssystem.ActiveProducts)
            {
                Console.Write(Item.ToString());
            }
            Console.WriteLine();
        }
        private bool _running;
        public void Start()
        {

            StregsystemController ssc = new StregsystemController();
            _running = true;
            DisplayAktiveProducts();
            Console.Write("\nQuickbuy: ");
            do
            {
                string command = Console.ReadLine();
                DisplayAktiveProducts();
                if (command == ":q" || command == ":quit")
                    Close();
                ssc.RunCommand(command);
                Console.Write("\n\nQuickbuy: ");

            } while (_running);
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.user.Firstname} bought {count} products \nUser balance: {transaction.user.Balance}");
        }

        public void LowBalanceWarning(User user, decimal amount)
        {
            if (user.Balance < amount) Console.WriteLine("!!---> YOUR BALANCE IS UNDER 50 <---!!");
        }
    }
}