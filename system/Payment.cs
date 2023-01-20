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
        public double paymentAmount { get; set; }
        public Trip? trip { get; set; }
        public bool paid { get { return paid; } set { paid = false; } }
        public Payment(int _id, int _cardNumber,Trip _trip)
        {
            id= _id;
            cardNumber= _cardNumber;
            trip= _trip;
            paymentAmount= trip.price;
            paid= (trip.price >= paymentAmount);
        }
        public bool payTicket(double _paymentAmount)
        {
                return paid = (trip?.price >= _paymentAmount);
        }
        public bool reversePayment()
        {
            paid = false;
            return true;
        }
    }
}
