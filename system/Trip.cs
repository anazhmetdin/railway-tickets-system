using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace system
{
    public class Trip
    {
        public int id { get; }
        public double price { get; protected set; }
        public DateTime date { get; protected set; }
        public Station from { get; protected set; }
        public Station to { get; protected set; }
        public Train train { get; protected set; }
        public List<Ticket> tickets { get; protected set; }

        protected Trip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            this.id = id;
            this.price = price;
            this.date = date;
            this.from = from;
            this.to = to;
            this.train = train;
            tickets = new List<Ticket>();
        }
        public bool hasEmptySeats() { return (train.seatsCount > tickets.Count); }

        public Ticket? getTicket(long id)
        {
            foreach (Ticket ticket in tickets)
            {
                if (ticket.id == id) return ticket;
            }

            return null;
        }

        public bool addTicket(Ticket ticket)
        {
            if (getTicket(ticket.id) != null)
                return false;
            
            tickets.Add(ticket);
            
            return true;
        }

        public static List<Trip> getTrip(int? id = null, string? from = null, string? to = null,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<Trip> allTrips = Admin.getTrip();

            List<Trip> result = new();

            foreach (Trip trip in allTrips)
            {
                if ((id == null || trip.id == id) && (from == null || trip.from.name == from) &&
                    (to == null || trip.to.name == to) && (fromDate == null || trip.date >= fromDate) &&
                    (toDate == null || trip.date == toDate))
                {
                    result.Add(trip);
                }
            }

            return result;
        }
    }
}
