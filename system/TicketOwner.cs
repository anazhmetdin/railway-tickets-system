using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    interface TicketOwner
    {
        List<Ticket> getTicket();
        Ticket getTicket(int id);
        bool addTicket(Ticket ticket);

    }
}
