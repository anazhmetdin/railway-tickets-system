using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public abstract class TicketOwner : User
    {
        protected TicketOwner(int SSN, string username, string password) : base(SSN, username, password)
        {
        }

        public List<Ticket> getTicket(int? id = null, string? from = null, string? to = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<Ticket> result = new();

            foreach (Ticket ticket in getTicket())
            {
                if ((id == null || ticket.id == id) && (from == null || ticket.trip.from.name == from) &&
                    (to == null || ticket.trip.to.name == to) || (fromDate == null || ticket.trip.date >= fromDate) &&
                    (toDate == null || ticket.trip.date == toDate))
                {
                    result.Add(ticket);
                }
            }

            return result;
        }

        public abstract List<Ticket> getTicket();
    }
}
