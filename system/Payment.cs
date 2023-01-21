using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Payment
    {
        public long id { get; }
        public string cardNumber { get; }
        public double paymentAmount { get; set; }
        public bool paid { get; private set; }
        public Payment(long _id, string _cardNumber)
        {
            id = _id;
            cardNumber = _cardNumber;
            paymentAmount = 0;
            paid = false;
        }

        public bool payTicket(double price, string threeDigitsSecurity)
        {
            paymentAmount = price;
            return (paid = true);
        }

        public bool reversePayment()
        {
            paid = false;
            return true;
        }
    }
}
