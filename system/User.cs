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
        public string username { get; set; }
        public string password { get; set; }
        public bool auth { get; set; }

        public User(int SSN, string username, string password) 
        {
            this.SSN = SSN;
            this.username = username;
            this.password = password;
            this.auth = (login(username,password) != null);
        }

        public bool authenticated(string password)
        {
            auth = (this.password == password);
            return auth;
        }
        public abstract User? login(string username, string password);
        



    }
}
