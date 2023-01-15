using system;
using System;

public class OfflineTicket : Ticket
{
    private Employee owner;
    public OfflineTicket(Employee owner, Trip trip, string id) : base(trip,id)
	{
        this.owner = owner;
    }

}
