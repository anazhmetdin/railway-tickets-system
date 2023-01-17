using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlinePassenger: User,TicketOwner
    {
        List<OnlineTicket> onlineTicketList = new();

        protected OnlinePassenger(int SSN, string username, string password) : base(SSN, username, password) {}

        public bool addTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> getTicket()
        {
            throw new NotImplementedException();
        }

        public Ticket getTicket(int id)
        {
            throw new NotImplementedException();
        }

        public static OnlinePassenger? signup(int SSN, string username, string password)
        {
            OnlinePassenger op = new(SSN, username, password);
            if (Admin.addOnlinePassenger(op))
            {
                return op;
            }
            return null;
        }
    }
}
