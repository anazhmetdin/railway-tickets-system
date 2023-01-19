using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Payment
    {
        public int id { get; }
        public int cardNumber { get; }
        public OnlineTicket onlineTcket { get; }
        public double paymentAmount { get; set; }
        public bool paid { get { return paid; } set { paid = false; } }
        public Payment(int _id, int _cardNumber, OnlineTicket _onlineTicket, double _paymentAmount)
        {
            id= _id;
            cardNumber= _cardNumber;
            onlineTcket= _onlineTicket;
            paymentAmount= _paymentAmount;
            paid= (onlineTcket.trip.price >= paymentAmount) ? true : false;
        }
        public bool payTicket(double _paymentAmount)
        {
                return paid = (onlineTcket.trip.price >= _paymentAmount) ? true : false;
        }
        public bool reversePayment()
        {
            paid = false;
            return true;
        }
    }
}
