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

        public double price { get;}
        public int cardNumber { get; }
        public Payment(int id, int cardNumber, double price)
        {
            this.id = id;
            this.price = price;
            this.cardNumber = cardNumber;
        }


      

        public bool payTicket(double price)
        {

            return true;

        }
        public bool reverseTicket(double price)
        {

            return true;

        }

    }
}



