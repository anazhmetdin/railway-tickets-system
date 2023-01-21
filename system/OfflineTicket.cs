using system;
using System;

public class OfflineTicket : Ticket
{
    private Employee owner;
    public OfflineTicket(Employee owner, Trip trip, long id) : base(id, trip) => this.owner = owner;
}
