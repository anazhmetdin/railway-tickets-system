using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlinePassenger: TicketOwner
    {
        private List<OnlineTicket> tickets { get; }

        protected OnlinePassenger(int SSN, string username, string password) : base(SSN, username, password) {
            tickets = new List<OnlineTicket>();
        }

        public bool bookTicket(Trip trip, string cardNumber, string threeDigitsSecurity)
        {
            if (auth && trip.hasEmptySeats())
            {
                long ticketPaymentId = long.Parse(trip.date.ToString("yyyyMMddHHmm") + trip.tickets.Count.ToString() + trip.id.ToString());
                
                Payment payment = new(ticketPaymentId, cardNumber);

                if (payment.payTicket(trip.price, threeDigitsSecurity))
                {
                    var ticket = new OnlineTicket(payment, this, ticketPaymentId, trip);
                    tickets.Add(ticket);
                    trip.addTicket(ticket);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        public bool cancelTicket(long ticketId)
        {
            if (!auth) { return false; }

            int tripTicketFlag = 0;
            for (int i = 0; i < tickets.Count; i++)
            {               
                if (tickets[i].id == ticketId)
                {
                    if (tickets[i].trip.date > DateTime.Now && tickets[i].cancelTicket(this))
                    {
                        for (int j = 0; j < tickets[i].trip.tickets.Count; j++)
                        {
                            if (tickets[i].trip.tickets[j].id == ticketId)
                            {
                                tickets[i].trip.tickets.Remove(tickets[i].trip.tickets[j]);
                                tripTicketFlag = 1;
                                break;
                            }
                        }

                        if (tripTicketFlag != 0)
                        {
                            tickets.Remove(tickets[i]);
                        }

                    }

                    break;
                }
            }
            return false;
        }

        public static OnlinePassenger? signup(int SSN, string username, string password)
        {
            OnlinePassenger op = new(SSN, username, password);
            if (Admin.addOnlinePassenger(op))
            {
                op.authenticate(password);
                return op;
            }
            return null;
        }

        public override List<Ticket> getTicket()
        {
            return tickets.Cast<Ticket>().ToList();
        }
    }
}
