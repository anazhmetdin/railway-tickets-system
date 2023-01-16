using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public interface TicketOwner
    {
        public List<Ticket> getTicket();
        public Ticket getTicket(int id);
        public bool addTicket(Ticket ticket);

    }
}
