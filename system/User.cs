using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    internal abstract class User
    {
        public int SSN { get; }
        public string username { get; set; }
        public string password { get; set; }
        public bool auth { get; set; }

        public User(int SSN, string username, string password, bool auth) 
        {
            this.SSN = SSN;
            this.username = username;
            this.password = password;
            this.auth = auth;
        }

        public bool login(string username, string password) 
        {
            /*
                        if ( username) { return true; }
                        if() { return false; }
                        return auth; */
            return true;
        }

    }
}
