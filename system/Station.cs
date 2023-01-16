using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Station
    {
        protected string name { get; set; }
        protected string location { get; set; }
        protected Station(string name, string location) 
        {
            this.name = name;
            this.location = location;
        }
    }
}
