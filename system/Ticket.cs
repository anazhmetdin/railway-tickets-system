using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    abstract class Ticket
    {
        public int id { get; }
        public Trip trip { get; }
        public DateTime BookingDate { get; }


        public Ticket(int id, Trip trip)
        {
            this.id = id;
            this.trip = trip;
        }

        public bool bookTicket()
        {
            if (trip.hasEmptySeats())
            {
                trip.addTicket(this);
                return true;
            }else
                return false;
        }

        public abstract User getOwner();
    }
}
