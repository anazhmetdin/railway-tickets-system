using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace system
{

    public class Employee : User
    {


        public int salary { get; set; }
        List<OfflineTicket> tickets = new();


        protected Employee(int salary, int SSN, string username, string password) : base(SSN, username, password)
        {
            this.salary = salary;
        }

        public List<OfflineTicket> getOfflineTickets()
        {
            return tickets;
        }

        public OfflineTicket getTicket(int id)
        {
            OfflineTicket? t = default;
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].id == id)
                    t = tickets[i];
            }

            return t;
        }

        public bool addTicket(OfflineTicket ticket)
        {
            tickets.Add(ticket);
            return (tickets.Contains(ticket));

        }

        public bool bookTicket(Trip trip)
        {

            if (trip.hasEmptySeats())
            {
                trip.addTicket()     ;

                return true;
            }
            else
                return false;
        }
    }
}
