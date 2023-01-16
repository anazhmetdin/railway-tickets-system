using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    internal class Admin : User
    {
        public Admin(int SSN, string username, string password, bool auth) :base(SSN, username, password, auth) {}


        static List<Train> trains = new List<Train>();
        static List<Station> staions = new List<Staion>();
        static List<OlinePassenger> olinePassengers = new List<OlinePassenger>();
        static List<Trip> trips = new List<Trip>();

        static List<Employee> employees = new List<Employee>();

        public override bool login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
