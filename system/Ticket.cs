using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace system
{
    public abstract class Ticket 
    {
        public long id { get; }
        public Trip trip { get; }
        public DateTime BookingDate { get; }


        public Ticket(long id, Trip trip)
        {
            this.id = id;
            this.trip = trip;
            BookingDate= DateTime.Now;
        }

        public abstract bool bookTicket();

        public abstract User getOwner();
    }
}
