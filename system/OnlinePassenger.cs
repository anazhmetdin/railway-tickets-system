using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlinePassenger : User,TicketOwner
    {
        List<OlineTicket> onlineTicketList;

        public OnlinePassenger(int SSN, string username, string password, bool auth) : base(SSN, username, password, auth) {}

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

        public override bool login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
