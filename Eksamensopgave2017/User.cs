using System;
using System.Text.RegularExpressions;

namespace Eksamensopgave2017
{
    public class User : IComparable<User>
    {
        //https://msdn.microsoft.com/en-us/library/twcw2f1c(v=vs.110).aspx
        Regex UsernameValidator = new Regex(@"^[a-z\d_-]+$"); 
        Regex EmailLocalValidator = new Regex(@"^[a-zA-Z\d_\-.]+$");
        Regex EmailDomainValidator = new Regex(@"^[a-zA-Z\d\-.]+$");
        public User(string firstname, string lastname, string username, string email, decimal balance)
        {
            string ConvertedUsername = username.Replace("\"", ""); //http://stackoverflow.com/a/1177897
            Match mUsername = UsernameValidator.Match(ConvertedUsername);
            if (mUsername.Success)
                this.Username = ConvertedUsername;
            else
                throw new InvalidUsernameException();

            string[] SplittedEmail = email.Split('@'); // Split it to find local and domain
            Match mLocal = EmailLocalValidator.Match(SplittedEmail[0]);
            Match mDomain = EmailDomainValidator.Match(SplittedEmail[1]);
            int DomainLength = SplittedEmail[1].Length - 1;
            //If both the Regex validators is okay and the char check does not match we asign email
            if (mLocal.Success && mDomain.Success && SplittedEmail[1][0] != Convert.ToChar(".") && SplittedEmail[1][0] != Convert.ToChar("-") && SplittedEmail[1][DomainLength] != Convert.ToChar(".") && SplittedEmail[1][DomainLength] != Convert.ToChar("-"))
                this.Email = email;
            else
                throw new InvalidEmailException();

            this.Balance = balance;

            if (firstname == null || firstname == "" || lastname == null || lastname == "")
                throw new ArgumentNullException();
            else
            {
                this.Firstname = firstname;
                this.Lastname = lastname;
            }

        }

        public readonly int Id = _nextID++;
        private static int _nextID = 1;
        public string Firstname;
        public string Lastname;
        public string Username;
        public string Email;
        public decimal Balance;

        //https://msdn.microsoft.com/en-us/library/system.object.tostring(v=vs.110).aspx
        public override string ToString()
        {
            return $"{this.Firstname.ToString()} {this.Lastname.ToString()}\n";
        }
        
        public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(User other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}
