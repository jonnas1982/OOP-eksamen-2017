using System;
using System.Collections.Generic;
using System.IO;

namespace Eksamensopgave2017
{
    class FileWriters
    {
        public void WriteToProductCsv(string filepath, List<Product> list)//https://www.codeproject.com/Articles/415732/Reading-and-Writing-CSV-Files-in-Csharp
        {
            using (CsvFileWriter writer = new CsvFileWriter(filepath))
            {
                CsvRow FirstRow = new CsvRow();
                //First row will be the description row
                FirstRow.Add(String.Format("id;name;price;active"));
                writer.WriteRow(FirstRow);
                CsvRow row = new CsvRow();
                //For each row in the file we will now fill the file
                foreach (Product element in list)
                {
                    //We need to add it all as strings
                    string active;
                    if (element.Active == true)
                        active = "1";
                    else
                        active = "0";
                    row.Add(element.Id.ToString() + ";\"" + element.Name.ToString() + "\";" + element.Price.ToString() + ";" + active + "\n");
                }
                writer.WriteRow(row);
            }
        }
        public void WriteToUserCsv(string filepath, List<User> list)
        {
            using (CsvFileWriter writer = new CsvFileWriter(filepath))
            {
                CsvRow FirstRow = new CsvRow();
                //First row will be the description row
                FirstRow.Add(String.Format("id;firstname;lastname;username;email;balance"));
                writer.WriteRow(FirstRow);
                CsvRow row = new CsvRow();
                //For each row in the file we will now fill the file
                foreach (User element in list)
                {
                    row.Add($"{ element.Id.ToString() };\"{ element.Firstname.ToString() }\";\"{ element.Lastname.ToString() }\";\"{ element.Username.ToString() }\";\"{ element.Email.ToString() }\";{ element.Balance.ToString()} \n");
                }
                writer.WriteRow(row);
            }
        }
        public void WriteToTransactionCsv(string filepath, string transactioninfo)//https://social.msdn.microsoft.com/Forums/vstudio/en-US/0271c11d-4cf3-452b-af65-6c06473669fb/adding-row-into-existing-csv-file-using-c?forum=csharpgeneral
        {
            //We create a list, add a single string to it and them writes it to the file
            List<string> newLines = new List<string>();
            newLines.Add(transactioninfo.ToString());
            File.AppendAllLines(filepath, newLines);
        }
    }
}
