using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Payment
    {
        public Payment(int id, string cardNumber)
        {
            this.id = id;
            this.cardNumber = cardNumber;
        }

        public int id { get; }
        public string cardNumber { get; }
        public bool payTicket(double price)
        {
            return true;
        }
    }
}
