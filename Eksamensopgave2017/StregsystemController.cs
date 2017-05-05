using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017
{
    class StregsystemController
    {
        //http://stackoverflow.com/questions/2829873/how-can-i-detect-if-this-dictionary-key-exists-in-c
        //https://www.tutorialspoint.com/csharp/csharp_delegates.htm
        //https://msdn.microsoft.com/en-us/library/bb882516.aspx
        //http://stackoverflow.com/questions/4233536/c-sharp-store-functions-in-a-dictionary
        //http://stackoverflow.com/questions/2896715/dictionary-with-delegate-or-switch
        //http://stackoverflow.com/a/21099511

        delegate List<Product> StatusCommands(int productId);
        delegate void QuitCommand();
        delegate List<Product> CreditCommands(int productId);
        delegate InsertCashTransaction AddCreditsCommands(User user, decimal amount, List<User> UserList);

        StregsystemCLI CLI = new StregsystemCLI();
        Stregsystem sSystem = new Stregsystem();
        
        public Dictionary<string, Delegate> _adminCommands = new Dictionary<string, Delegate>();

        public static List<Product> Aktivate(int productId)
        {
            StregsystemCLI CLI = new StregsystemCLI();
            FileWriters writer = new FileWriters();
            ReadFromCsv.ProductList.Where(x => x.Id == productId).ToList().ForEach(x => x.Active = true);
            writer.WriteToProductCsv("../../products.csv", ReadFromCsv.ProductList);
            CLI.DisplayAktiveProducts();
            return null;
        }

        public static List<Product> Deactivate(int productId)
        {
            StregsystemCLI CLI = new StregsystemCLI();
            FileWriters writer = new FileWriters();
            ReadFromCsv.ProductList.Where(x => x.Id == productId).ToList().ForEach(x => x.Active = false);
            writer.WriteToProductCsv("../../products.csv", ReadFromCsv.ProductList);
            CLI.DisplayAktiveProducts();
            return null;
        }

        public static List<Product> CreaditOn(int productId)
        {
            FileWriters writer = new FileWriters();
            ReadFromCsv.ProductList.Where(x => x.Id == productId).ToList().ForEach(x => x.CanBeBoughtOnCredit = true);
            writer.WriteToProductCsv("../../products.csv", ReadFromCsv.ProductList);
            return null;
        }

        public static List<Product> CreditOff(int productId)
        {
            FileWriters writer = new FileWriters();
            ReadFromCsv.ProductList.Where(x => x.Id == productId).ToList().ForEach(x => x.CanBeBoughtOnCredit = false);
            writer.WriteToProductCsv("../../products.csv", ReadFromCsv.ProductList);
            return null;
        }

        public StregsystemController()
        {
            StatusCommands MakeAktive = new StatusCommands(Aktivate);
            StatusCommands MakeDeactivacted = new StatusCommands(Deactivate);
            QuitCommand Quit = new QuitCommand(CLI.Close);
            CreditCommands MakeCreditOn = new CreditCommands(CreaditOn);
            CreditCommands MakeCreditOff = new CreditCommands(CreditOff);
            AddCreditsCommands AddCredits = new AddCreditsCommands(sSystem.AddCreditsToAccount);

            _adminCommands.Add(":activate", MakeAktive);
            _adminCommands.Add(":deactivate", MakeDeactivacted);
            _adminCommands.Add(":quit", Quit);
            _adminCommands.Add(":q", Quit);
            _adminCommands.Add(":crediton", MakeCreditOn);
            _adminCommands.Add(":creditoff", MakeCreditOff);
            _adminCommands.Add(":addcredits", AddCredits);
        }

        public void RunCommand(string command)
        {
            if (command == "")
                CLI.DisplayTooFewArgumentsError();
            else if (Convert.ToString(command[0]) == ":") //If it starts with : we know it is a admin command
            {
                string[] SplittedString = command.Split(' '); //Split it so we can see which command and use the info
                string AdminCommand = Convert.ToString(SplittedString[0]);
                if (_adminCommands.ContainsKey(AdminCommand));
                {
                    if (AdminCommand == ":q" || AdminCommand == ":quit")
                    {
                        _adminCommands[":q"].DynamicInvoke();
                    }
                    else if (AdminCommand == ":activate" || AdminCommand == ":deactivate" || AdminCommand == ":crediton" || AdminCommand == ":creditoff")
                    {
                        int productId = Convert.ToInt32(SplittedString[1]);
                        _adminCommands[AdminCommand].DynamicInvoke(productId);
                    }
                    else if (AdminCommand == ":addcredits")
                    {
                        string username = Convert.ToString(SplittedString[1]);
                        User user = sSystem.GetUserByUsername(username, ReadFromCsv.UserList);
                        decimal amount = Convert.ToDecimal(SplittedString[2]);
                        sSystem.AddCreditsToAccount(user, amount, ReadFromCsv.UserList).Execute();
                    }
                    else
                    {
                        CLI.DisplayAdminCommandNotFoundMessage(command);
                    }
                }
            }
            else
            {
                //Setup Regex to notice the tree different kindes if commands
                Match username = Regex.Match(command, @"^[a-z\d_-]+$"); //Only username
                Match buy = Regex.Match(command, @"^[a-z\d_-]+ \d+$"); //username numbers
                Match multibuy = Regex.Match(command, @"^[a-z\d_-]+ \d+ \d+$"); //username numbers numbers

                if (username.Success)
                {
                    try
                    {
                        User user = sSystem.GetUserByUsername(command, ReadFromCsv.UserList);
                        CLI.DisplayUserInfo(user);
                    }
                    catch (InvalidUsernameException)
                    {
                        CLI.DisplayUserNotFound(command);
                    }
                }
                else if (buy.Success)
                {
                    string[] buyArray = command.Split(' '); //Split the command to  get the information
                    try
                    {
                        User user = sSystem.GetUserByUsername(Convert.ToString(buyArray[0]), ReadFromCsv.UserList);
                        Product product = sSystem.GetProductByID(Convert.ToInt32(buyArray[1]), ReadFromCsv.ProductList);
                        if (user.Balance >= product.Price || product.CanBeBoughtOnCredit)
                        {
                            sSystem.BuyProduct(user, product, ReadFromCsv.UserList, ReadFromCsv.ProductList).Execute();
                            CLI.DisplayUserBuysProduct(sSystem.BuyProduct(user, product, ReadFromCsv.UserList, ReadFromCsv.ProductList));
                            CLI.LowBalanceWarning(user, 50); // Check for low balance
                        }
                        else
                        {
                            CLI.DisplayInsufficientCash(user, product);
                        }
                    }
                    catch (InvalidUsernameException) { CLI.DisplayUserNotFound(Convert.ToString(buyArray[0])); }
                    catch (InactiveProductException)
                    {
                        //Create a list of all inactive products to finde the specifc product by id
                        List<Product> productList = new List<Product>();
                        foreach (var element in ReadFromCsv.SeasonalProductList.FindAll(x => x.Active == false)) productList.Add(element);
                        foreach (var element in ReadFromCsv.ProductList.FindAll(x => x.Active == false)) productList.Add(element);
                        Product product = productList.Find(x => x.Id == Convert.ToInt32(buyArray[1]));
                        CLI.DisplayInactiveProduct(product);
                    }
                    catch (ProductDoesNotExsistException) { CLI.DisplayProductNotFound(null); }
                }
                else if (multibuy.Success)
                {
                    string[] buyArray = command.Split(' ');
                    try
                    {
                        User user = sSystem.GetUserByUsername(Convert.ToString(buyArray[0]), ReadFromCsv.UserList);
                        int productCount = Convert.ToInt32(buyArray[1]);
                        Product product = sSystem.GetProductByID(Convert.ToInt32(buyArray[2]), ReadFromCsv.ProductList);
                        decimal totalPrice = productCount * product.Price;
                        if ((user.Balance >= totalPrice || product.CanBeBoughtOnCredit))
                        {
                            for (int i = 0; i < productCount; i++) // Run the command once for each bought product
                                sSystem.BuyProduct(user, product, ReadFromCsv.UserList, ReadFromCsv.ProductList).Execute();
                            CLI.DisplayUserBuysProduct(productCount, sSystem.BuyProduct(user, product, ReadFromCsv.UserList, ReadFromCsv.ProductList));
                            CLI.LowBalanceWarning(user, 50);
                        }
                        else
                        {
                            CLI.DisplayInsufficientCash(user, product);
                        }
                    }
                    //http://stackoverflow.com/questions/136035/catch-multiple-exceptions-at-once
                    catch (InvalidUsernameException) { CLI.DisplayUserNotFound(Convert.ToString(buyArray[0])); }
                    catch (InactiveProductException)
                    {
                        List<Product> productList = new List<Product>();
                        foreach (var element in ReadFromCsv.SeasonalProductList.FindAll(x => x.Active == false)) productList.Add(element);
                        foreach (var element in ReadFromCsv.ProductList.FindAll(x => x.Active == false)) productList.Add(element);
                        Product product = productList.Find(x => x.Id == Convert.ToInt32(buyArray[2]));
                        CLI.DisplayInactiveProduct(product);
                    }
                    catch (ProductDoesNotExsistException) { CLI.DisplayProductNotFound(null); }

                }
                else
                {
                     CLI.DisplayTooManyArgumentsError(command);
                }
            }
        }
    }
}
