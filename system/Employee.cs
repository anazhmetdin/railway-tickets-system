using System;

interface TicketUser
{
	List<Ticket> getTicket();
	Ticket getTicket(int id);
	bool addTicket(Ticket ticket);

}

public class Employee : TicketUser
{
	private int salary;
	private OfflineTicket tickets ;


	private Employee(int salary,string SSN, string username, bool auth, string password)
    {

    }

    public int Salary { get; set; }
    
	public getTicket()
    {

    }
	public getTicket(int id) 
	{
	}
	public addTicket(Ticket t)
    {

    }

}



