using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlineTicket : Ticket
    {
        public Payment payment { get; private set; }
        public OnlinePassenger owner { get; set; }

        public OnlineTicket(Payment payment, OnlinePassenger owner, int id, Trip trip) : base(id, trip)
		{
            this.payment = payment;
            this.owner = owner;
        }


        public bool cancelTicket()
        {
            return true;
        }

        public override bool bookTicket()
        {
            return (trip.hasEmptySeats() && (payment.paid)) ? trip.addTicket(this) : false;
        }
        public override OnlinePassenger getOwner()
        {
            return owner;
        }
    }
}
