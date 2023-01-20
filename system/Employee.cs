using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Employee : TicketsOwner
    {
        public int salary { get; set; }
        new List<OfflineTicket> tickets = new();

        protected Employee(int salary, int SSN, string username, string password) : base(SSN, username, password) => this.salary = salary;

        public override bool bookTicket(Trip trip, int _cardnumber)
        {
            if (trip.hasEmptySeats())
            {
                int ticketId = Int32.Parse(trip.date.ToString("yyyyMMddHHmm")+ trip.tickets.Count.ToString()+trip.id.ToString());
                var ticket = new OfflineTicket(this, trip, ticketId);
                tickets.Add(ticket);
                trip.addTicket(ticket);
                return true;
            }
            else
                return false;
        }
    }

/*        public bool addTicket(OfflineTicket ticket)
        {
            tickets.Add(ticket);
            return (tickets.Contains(ticket));

        }
*/
}

