//20143928_Jonas_Rechnitzer_Eriksen

using System.IO;

namespace Eksamensopgave2017
{
    class Program
    {
        static void Main(string[] args)
        {
            // In the start all files will be loaded and "database" lists will be pupulated
            File.WriteAllText(@"../../transactions.csv", string.Empty);
            ReadFromCsv csvReader = new ReadFromCsv();
            StregsystemCLI ui = new StregsystemCLI();
            csvReader.CreateUserList("../../users.csv");
            csvReader.CreateProductList("../../products.csv");
            csvReader.CreateTransactionList();
            //We will now start the interface
            ui.Start();
        }
    }
}
