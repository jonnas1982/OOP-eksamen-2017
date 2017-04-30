using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave2017
{
    class User : IComparable<User>
    {
        Regex UsernameValidator = new Regex(@"^[A-Za-z\d_-]+$"); //https://msdn.microsoft.com/en-us/library/twcw2f1c(v=vs.110).aspx
        public User(string firstname, string lastname, string username, string email, double balance)
        {
            Match mUsername = UsernameValidator.Match(username);
            if (mUsername.Success) this.Username = username;
            else throw new InvalidUsernameException("Invalid username");

            this.Email = email; //TODO: Set settings

            this.Balance = balance; //TODO: Set settigns

            if (firstname == null || firstname == "") { throw new ArgumentNullException("This is not good"); }//TODO: Make real error! 
            else { this.Firstname = firstname; }

            if (lastname == null || lastname == "") { throw new ArgumentNullException("This is not good"); }//TODO: Make real error! 
            else { this.Lastname = lastname; }

        }

        public readonly int Id = _nextID++;
        private static int _nextID = 1;
        public string Firstname;
        public string Lastname;
        public string Username;
        public string Email;
        public double Balance;

        //https://msdn.microsoft.com/en-us/library/system.object.tostring(v=vs.110).aspx
        public override string ToString()
        {
            return this.Firstname.ToString() + " " + this.Lastname.ToString();
        }

        public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null) return false;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode() //TODO: Check up on GetHashCode
        {
            return base.GetHashCode();
        }

        public int CompareTo(User other)
        {
            return this.Id.CompareTo(other.Id);
        }


    }
}
