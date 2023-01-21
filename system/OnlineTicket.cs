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
        public OnlinePassenger owner { get; }

        public OnlineTicket(Payment payment, OnlinePassenger owner, long id, Trip trip) : base(id, trip)
		{
            this.payment = payment;
            this.owner = owner;
        }


        public bool cancelTicket(OnlinePassenger passenger)
        {            
            if (passenger.SSN == owner.SSN && passenger.auth) 
            {
                return payment.reversePayment();
            }

            return false;
        }
    }
}
