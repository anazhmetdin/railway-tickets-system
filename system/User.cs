using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public abstract class User
    {
        public int SSN { get; }
        public string username { get; }
        private string password;
        public bool auth { get; protected set; }

        public User(int SSN, string username, string password) 
        {
            this.SSN = SSN;
            this.username = username;
            this.password = password;
            this.auth = false;
        }

        public bool authenticate(string password)
        {
            auth = password == this.password;
            return auth;
        }

        public void logout()
        {
            auth = false;
        }
    }
}
