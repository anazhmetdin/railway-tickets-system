using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public abstract class TicketsOwner : User
    {
        public List<Ticket> tickets = new();

        protected TicketsOwner(int SSN, string username, string password) : base(SSN, username, password)
        {
        }

        public List<Ticket> getTicket(int? id = null, Station? from = null, Station? to = null, DateTime? fromDate = null, DateTime? toDate = null) 
        {
            List<Ticket> result = new();
            if (id != null && from != null && to != null && fromDate != null && toDate != null)
            {
                if (id != null)
                /*tickets.Where(ticket => ticket.id == id);
                tickets.Where(ticket => ticket.trip.from == from);
                tickets.Where(ticket => ticket.trip.to == to);
                tickets.Where(ticket => ticket.trip.date == fromDate);
                tickets.Where(ticket => ticket.trip.date == toDate);*/
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (id != null && tickets[i].id == id)
                        result.Add(tickets[i]);
                    else if (from !=null && to != null && tickets[i].trip.from == from && tickets[i].trip.to == to)
                        result.Add(tickets[i]);
                    else if (fromDate != null && toDate != null && tickets[i].trip.date == fromDate && tickets[i].trip.date == toDate)
                        result.Add(tickets[i]);
                }
            }else
                result = tickets;
            return result;
        }
        public abstract bool bookTicket(Trip trip, int _cardNumber = default);
    }
}
