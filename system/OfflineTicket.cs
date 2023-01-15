using System;

public class OfflineTicket : Ticket
{
	public OfflineTicket(Employee owner, string id, Trip trip) : base(trip,id)
	{
	}

}
