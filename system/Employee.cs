using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Employee : TicketOwner
    {
        public double salary { get; protected set; }
        private List<OfflineTicket> tickets { get; }


        protected Employee(double salary, int SSN, string username, string password) : base(SSN, username, password) 
        { 
            this.salary = salary;
            tickets = new List<OfflineTicket>();
        }

        public bool bookTicket(Trip trip)
        {
            if (auth && trip.hasEmptySeats())
            {
                long ticketId = long.Parse(trip.date.ToString("yyyyMMddHHmm") + trip.tickets.Count.ToString() + trip.id.ToString());
                var ticket = new OfflineTicket(this, trip, ticketId);
                tickets.Add(ticket);
                trip.addTicket(ticket);
                return true;
            }
            else
                return false;
        }

        public override List<Ticket> getTicket()
        {
            return tickets.Cast<Ticket>().ToList();
        }
    }
}
