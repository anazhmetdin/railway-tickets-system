using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Station
    {
        public string name { get; protected set; }
        public string location { get; protected set; }
        protected Station(string name, string location) 
        {
            this.name = name;
            this.location = location;
        }
    }
}
