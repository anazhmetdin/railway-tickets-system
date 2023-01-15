using System;

public class Ticket
{
	private string id;
	private Trip trip;
	private DateTime bookingDate;

	public Ticket(Trip trip, string id)
	{

	}
	public bool bookTicket() 
	{
	}
	public User getOwner()
    {

    }
	public string getId()
    {
		return id;
    }
    public Trip trip { get; set; }
}
