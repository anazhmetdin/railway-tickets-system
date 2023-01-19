using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class OnlinePassenger : User
    {
        List<OnlineTicket> onlineTicketList;

        public OnlinePassenger(int SSN, string username, string password) : base(SSN, username, password) 
        {
            onlineTicketList = new();
            if (auth)
                Admin.addToOnlinePassenger(this);

        }

        public bool addOnlineTicket(OnlineTicket ticket)
        {
            onlineTicketList.Add(ticket);
            return (onlineTicketList.Contains(ticket));
        }

        public List<OnlineTicket> getOnlineTicket()
        {
            return onlineTicketList;
        }

        public OnlineTicket getOnlineTicket (int id)
        {
           return onlineTicketList.Find(x => x.id == id);
           
        }

        public  OnlinePassenger login(string username, string password)
        {
            OnlinePassenger oPassenger = Admin.getOnlinePassenger(username);
            if (oPassenger != null && oPassenger.authenticate(password))
            {
                return oPassenger;
            }
            else
                return null;
        }
    }
}
