using system;
using System;

public class OfflineTicket : Ticket
{
    private Employee owner;
    public OfflineTicket(Employee owner, Trip trip, int id) : base(id, trip) => this.owner = owner;

    public override bool bookTicket()
    {
        if (trip.hasEmptySeats())
        {
            trip.addTicket(this);
            return true;
        }
        else
            return false;
    }

    public override Employee getOwner()
    {
        return owner;
    }
}
