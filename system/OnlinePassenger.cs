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

        public OnlinePassenger(int SSN, string username, string password) : base(SSN, username, password) 
        {
            onlineTicketList = new();
            if (auth)
                Admin.addToOnlinePassenger(this);

        }

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
