using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace system
{
    public class Trip : TicketOwner
    {
        public int id { get; }
        public double price { get; protected set; }
        public DateTime date { get; protected set; }
        public Station from { get; protected set; }
        public Station to { get; protected set; }
        public Train train { get; protected set; }
        public List<Ticket> tickets;

        protected Trip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            this.id = id;
            this.price = price;
            this.date = date;
            this.from = from;
            this.to = to;
            this.train = train;
        }
        public bool hasEmptySeats() { return (train.seatsCount > tickets.Count); }

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
            tickets.Add(ticket);
            if (tickets.Contains(ticket))
                return true;
            return false;
        }

    }
}
