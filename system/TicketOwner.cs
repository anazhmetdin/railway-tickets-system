<<<<<<< HEAD
<<<<<<< HEAD
﻿using System;
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
=======
﻿using System;
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
>>>>>>> 62234a7e48ea547fbf1df70fb630dac5064bfa99
=======
﻿using System;
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
>>>>>>> 62234a7e48ea547fbf1df70fb630dac5064bfa99
