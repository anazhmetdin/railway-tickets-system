using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace system
{
    internal class Trip : TicketOwner
    {
        public int id { get; }
        public double price { get; protected set; }
        public DateTime date { get; protected set; }
        public Station from { get; protected set; }
        public Station to { get; protected set; }
        public Train train { get; protected set; }
        public List<Ticket> tickets;

        public Trip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            this.id = id;
            this.price = price;
            this.date = date;
            this.from = from;
            this.to = to;
            this.train = train;
        }
        public bool hasEmptySeats() { return (train.getSeatCount() > tickets.Count); }

        List<Ticket> TicketOwner.getTicket()
        {
            throw new NotImplementedException();
        }

        Ticket TicketOwner.getTicket(int id)
        {
            throw new NotImplementedException();
        }

        bool TicketOwner.addTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
