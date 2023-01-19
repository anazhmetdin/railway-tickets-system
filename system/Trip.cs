namespace system
{
    public class Trip : TicketOwner
    {
        public int id { get; }
        public double price { get; protected set; }
        public DateTime date { get; protected set; }
        public Station from { get; protected set; }
        public Station to { get; protected set; }
        public Train train { get; protected set; }
        public List<Ticket> tickets = new();

        protected Trip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            this.id = id;
            this.price = price;
            this.date = date;
            this.from = from;
            this.to = to;
            this.train = train;
        }
        public bool hasEmptySeats() { return (train.seatsCount > tickets.Count); }

        public List<Ticket> getTicket()
        {
            throw new NotImplementedException();
        }

        public Ticket getTicket(int id)
        {
            throw new NotImplementedException();
        }

        public bool addTicket(Ticket ticket)
        {
            tickets.Add(ticket);

            return true;

        }

        public static List<Trip> getTrips(string from, string to, DateTime fromDate, DateTime toDate)
        {
            return Admin.getTrip(from, to, fromDate, toDate);
        }
    }
}
