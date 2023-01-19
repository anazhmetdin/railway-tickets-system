using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Employee : User, TicketOwner
    {
        public int salary { get; set; }
        List<OfflineTicket> tickets = new();


        protected Employee(int salary, int SSN, string username, string password) : base(SSN, username, password) 
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

        internal void bookTicket(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
