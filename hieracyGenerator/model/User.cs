using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hieracyGenerator.model
{
    class User : IComparable<User>
    {
        private string username;
        private string password;

        public string Username { get => this.username; set => this.username = value; }
        public string Password { get => this.password; set => this.password = value; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public override string ToString()
        {
            return username + "|" + password;
        }
        public override bool Equals(object obj)
        {
            User user = (User)obj;
            return this.username == user.username;
        }
        public int CompareTo(User user)
        {
            return 1;
        }
    }
}
