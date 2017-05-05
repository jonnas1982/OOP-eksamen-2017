using System;
using System.Collections.Generic;
using System.IO;

namespace Eksamensopgave2017
{
    class ReadFromCsv
    {
        //https://www.codeproject.com/Answers/384264/How-to-read-from-csv-file-using-csharp#answer2
        //https://msdn.microsoft.com/da-dk/library/98f28cdx.aspx
        //Static lists is puplicated so they can be used in all the classes
        //They will work as a "database"
        public static List<Product> ProductList = new List<Product>();
        public static List<SeasonalProduct> SeasonalProductList = new List<SeasonalProduct>();
        public static List<User> UserList = new List<User>();
        public static List<Transaction> TransactionList = new List<Transaction>();
        Stregsystem sSystem = new Stregsystem();
        public void CreateUserList(string fileLocation)
        {
            StreamReader sr = new StreamReader(String.Format(@"{0}", fileLocation));

            string strline = "";
            string[] _values = null;
            int i = 0;
            while (!sr.EndOfStream)
            {
                i++;
                //Remove all " signes
                strline = sr.ReadLine().Replace("\"", "");
                _values = strline.Split(';');
                if (_values.Length == 6 && _values[0].Trim().Length > 0)
                {
                    if (_values[0] == "id")
                    {
                        //Remove the desription line
                    }
                    else
                    {
                        //Add to list and create an instance of user
                        string firstname = _values[1];
                        string lastname = _values[2];
                        string username = _values[3];
                        string email = _values[4];
                        decimal balance = Convert.ToDecimal(_values[5]);

                        UserList.Add(new User(firstname, lastname, username, email, balance));
                    }
                }
            }
            sr.Close();
        }

        public void CreateProductList(string fileLocation)
        {
            StreamReader sr = new StreamReader(String.Format(@"{0}", fileLocation));

            string strline = "";
            string[] _values = null;
            int i = 0;
            while (!sr.EndOfStream)
            {
                i++;
                strline = sr.ReadLine();
                //Remove " and HTML tags 
                strline = strline.Replace("\"", "").Replace("<h2>", "").Replace("</h2>", "").Replace("<b>", "").Replace("</b>", "").Replace("<h1>", "").Replace("</h1>", "").Replace("<h3>", "").Replace("<h3>", "").Replace("<blink>", "").Replace("</blink>", "");
                _values = strline.Split(';');
                if (_values.Length == 4 && _values[0].Trim().Length > 0)
                {
                    if (_values[0] == "id")
                    {
                        //Remove the desription line
                    }
                    else
                    {
                        string name = _values[1];
                        decimal price = Convert.ToDecimal(_values[2]);
                        int active = 0;
                        if (_values[3] == "1")
                            active = 1;
                        //We need active to be integer, so we change it
                        ProductList.Add(new Product(name, active, 0, price));
                    }
                }
            }
            sr.Close();
            //Pupulate Seasonal ProductList and create instances
            SeasonalProductList.Add(new SeasonalProduct("Coffe (Black/Latte)", 1, 0, 23, 2016, 01, 01, 2017, 09, 01));
            SeasonalProductList.Add(new SeasonalProduct("Wine (Red)", 1, 0, 456, 2016, 01, 01, 2017, 09, 01));
            SeasonalProductList.Add(new SeasonalProduct("Wine (Wite)", 1, 1, 234, 2016, 01, 01, 2017, 01, 01));
        }

        public void CreateTransactionList()
        {
            ////Pupulate transaction list and create instances of both types
            TransactionList.Add(new BuyTransaction(sSystem.GetUserByUsername("fmikkelsen", ReadFromCsv.UserList), sSystem.GetProductByID(11, ReadFromCsv.ProductList), new DateTime(2012,1,18), sSystem.GetProductByID(11, ReadFromCsv.ProductList).Price, ReadFromCsv.UserList));
            TransactionList.Add(new InsertCashTransaction(sSystem.GetUserByUsername("bsmith", ReadFromCsv.UserList), new DateTime(2014, 1, 18), 4004, ReadFromCsv.UserList));
            TransactionList.Add(new BuyTransaction(sSystem.GetUserByUsername("bsmith", ReadFromCsv.UserList), sSystem.GetProductByID(13, ReadFromCsv.ProductList), new DateTime(2016, 6, 18), sSystem.GetProductByID(11, ReadFromCsv.ProductList).Price, ReadFromCsv.UserList));
            TransactionList.Add(new InsertCashTransaction(sSystem.GetUserByUsername("inielsen", ReadFromCsv.UserList), new DateTime(2016, 8, 18), 4004, ReadFromCsv.UserList));
            TransactionList.Add(new BuyTransaction(sSystem.GetUserByUsername("inielsen", ReadFromCsv.UserList), sSystem.GetProductByID(15, ReadFromCsv.ProductList), DateTime.Now, sSystem.GetProductByID(11, ReadFromCsv.ProductList).Price, ReadFromCsv.UserList));
            
            // Execute them to create a transaction file
            foreach (var element in ReadFromCsv.TransactionList)
                element.Execute();
        }
    }
}
