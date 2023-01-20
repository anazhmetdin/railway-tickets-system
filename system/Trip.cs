using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public List<Ticket> tickets = new();

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

/*        public List<Ticket> getTicket()
        {
            return tickets;
        }

        public Ticket? getTicket(int id)
        {

            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].id == id) return tickets[i];
            }
            return null;
        }*/

        public bool addTicket(Ticket ticket)
        {
            tickets.Add(ticket);
            if (tickets.Contains(ticket))
                return true;
            return false;
        }

        public static List<Trip> getTrips(string? from, string? to, DateTime? fromDate, DateTime? toDate)
        {
            
            List<Trip> allTrips = Admin.getTrips();
            List<Trip> result = new();
            if (from != null || to != null || fromDate != null || toDate != null)
            {
                for (int i = 0; i < allTrips.Count; i++)
                {
                    if (from != null && to != null && allTrips[i].from.ToString() == from && allTrips[i].to.ToString() == to)
                        result.Add(allTrips[i]);
                    else if (fromDate != null && toDate != null && fromDate <= allTrips[i].date && allTrips[i].date <= toDate)
                        result.Add(allTrips[i]);
                    else if
                        (
                        allTrips[i].from.ToString() == from && allTrips[i].to.ToString() == to
                        &&
                        fromDate <= allTrips[i].date && allTrips[i].date <= toDate
                        )
                        result.Add(allTrips[i]);
                    else
                        result = null;
                }   
            }
            else if (from == null && to == null && fromDate == null && toDate == null)
                        result = allTrips;

            return result;
        }
    }
}
