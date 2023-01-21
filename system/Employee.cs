using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Employee : TicketOwner
    {
        public int salary { get; set; }
        public List<OfflineTicket> tickets { get; }


        protected Employee(int salary, int SSN, string username, string password) : base(SSN, username, password) 
        { 
            this.salary = salary;
            tickets = new List<OfflineTicket>();
        }

        public override bool bookTicket(Trip trip, string cardNumber = "", string threeDigitsSecurity = "")
        {
            if (auth && trip.hasEmptySeats())
            {
                int ticketId = Int32.Parse(trip.date.ToString("yyyyMMddHHmm") + trip.tickets.Count.ToString() + trip.id.ToString());
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
