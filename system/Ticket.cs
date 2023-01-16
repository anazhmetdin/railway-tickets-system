using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace system
{
    public abstract class Ticket 
    {
        public int id { get; }
        public Trip trip { get; }
        public DateTime BookingDate { get; }


        public Ticket(int id, Trip trip)
        {
            this.id = id;
            this.trip = trip;
        }



        public abstract bool bookTicket();


        public abstract User getOwner();
    }
}
