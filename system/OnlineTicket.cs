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
        OnlinePassenger owner;

        public OnlineTicket(Payment payment, OnlinePassenger owner, long id, Trip trip) : base(id, trip)
		{
            this.payment = payment;
            this.owner = owner;
        }


        public bool cancelTicket()
        {            
            return payment.reversePayment();
        }

        public override bool bookTicket()
        {
            if (trip.hasEmptySeats())
            {
                trip.addTicket(this);
                return true;
            }
            else
                return false;
        }
        public override OnlinePassenger getOwner()
        {
            return owner;
        }
    }
}
