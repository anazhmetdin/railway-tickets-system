using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlinePassenger : TicketsOwner
    {
        List<OnlineTicket> tickets = new ();

        public OnlinePassenger(int SSN, string username, string password) : base(SSN, username, password) 
        {
            
       /*     if (auth)
                Admin.addToOnlinePassenger();*/

        }

        public override bool bookTicket(Trip trip, int _cardNumber)
        {
            //return (trip.hasEmptySeats() && (payment.paid)) ? trip.addTicket(this) : false;
            //int _id, int _cardNumber, OnlineTicket _onlineTicket, double _paymentAmount
            //Payment payment, OnlinePassenger owner, int id, Trip trip
            if (trip.hasEmptySeats())
            {
                int ticketPaymentId = Int32.Parse(trip.date.ToString("yyyyMMddHHmm") + trip.tickets.Count.ToString() + trip.id.ToString());
                Payment payment = new(ticketPaymentId,_cardNumber,trip);
                var ticket = new OnlineTicket(payment,this,ticketPaymentId, trip);
                tickets.Add(ticket);
                trip.addTicket(ticket);
                return true;
            }
            else
                return false;
        }

        public bool cancelTicket(int ticketId)
        {
            /*tickets.Remove(tickets.Where(ticket=>ticket.id==ticketId).FirstOrDefault());
            */
            int tripTicketFlag = 0;
            for (int i = 0; i < tickets.Count; i++)
            {
                for (int j = 0; j < tickets[i].trip.tickets.Count; j++)
                {
                    if (tickets[i].trip.tickets[j].id == ticketId)
                    {
                        tickets[i].trip.tickets.Remove(tickets[i].trip.tickets[j]);
                        tripTicketFlag = 1;
                    }
                }
                if (tripTicketFlag != 0)
                {
                    tickets.Remove(tickets[i]);
                    return true;
                }
            }
            return false;
        }
        public override OnlinePassenger? login(string username, string password)
        {
            OnlinePassenger? oPassenger = Admin.getOnlinePassenger(username);
            if (oPassenger != null && oPassenger.authenticated(password))
            {
                return oPassenger;
            }
            else
                return null;
        }
    }
}
