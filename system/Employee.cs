using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    internal class Employee : User, TicketOwner
    {
        public int salary { get; set; }
        List<OfflineTicket> tickets;


        protected Employee(int salary, int SSN, string username, string password, bool auth) : base(SSN, username, password, auth)
        {
            this.salary = salary;
        }

        public List<Ticket> getTicket()
        {
            throw new NotImplementedException();
        }

        public Ticket getTicket(int id)
        {
            throw new NotImplementedException();
        }

        public bool addTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
