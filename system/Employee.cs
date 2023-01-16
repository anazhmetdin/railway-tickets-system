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
        List<OfflineTicket> tickets;


        protected Employee(int salary, int SSN, string username, string password) : base(SSN, username, password) 
        { 
            this.salary = salary; 
            this.tickets = new List<OfflineTicket>();
            if (auth)
                Admin.addToEmployee(this);
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

        public override Employee? login(string username, string password)
        {
            Employee? employee = Admin.getEmployee(username);
            if (employee != null && employee.authenticated(password))
            {
                return employee;
            }
            else
                return null;
        }
    }
}
