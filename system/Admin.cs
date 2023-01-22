using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace system
{
    public class Admin : User
    {
        static List<Admin> admins = new()
        {
            createAdmin(12345, "admin", "admin")!
        };
        static List<Train> trains = new List<Train>();
        static List<Station> stations = new();
        static List<OnlinePassenger> onlinePassengers = new List<OnlinePassenger>();
        static List<Trip> trips = new List<Trip>();
        static List<Employee> employees = new List<Employee>();

        private Admin(int SSN, string username, string password) : base(SSN, username, password) { }

        public static bool addOnlinePassenger(OnlinePassenger passenger)
        {
            if (getOnlinePassenger(passenger.username) == null)
            {
                onlinePassengers.Add(passenger);
                return true;
            }
            return false;
        }

        private static Admin? getAdmin(string username)
        {
            foreach (var admin in admins)
            {
                if (admin.username == username)
                    return admin;
            }
            return null;
        }
        private static Employee? getEmployee(string username)
        {
            foreach (var employee in employees)
            {
                if (employee.username == username)
                    return employee;
            }
            return null;
        }
        private static OnlinePassenger? getOnlinePassenger(string username)
        {
            foreach (var onlinePassenger in onlinePassengers)
            {
                if (onlinePassenger.username== username)
                    return onlinePassenger;
            }
            return null;
        }
        public static Train? getTrain(int id)
        {
            foreach (Train train in trains)
            {
                if (train.id == id) return train;
            }
            return null;
        }
        public static Station? getStation(string name)
        {
            foreach (Station station in stations)
            {
                if (station.name == name) return station;
            }
            return null;
        }
        public static Trip? getTrip(int id)
        {
            foreach (Trip trip in trips)
            {
                if (trip.id == id) return trip;
            }
            return null;
        }

        public List<Train>? getTrain()
        {
            if (!auth) { return null; }
            return trains.ConvertAll(train => new ConstructableTrain(train.id, train.seatsCount) as Train);
        }
        public static List<Station> getStation()
        {
            return stations.ConvertAll(station => new ConstructableStation(station.name, station.location) as Station);

        }
        public static List<Trip> getTrip()
        {
            return trips.ConvertAll(trip => new ConstructableTrip(trip.id, trip.price, trip.date, trip.from, trip.to, trip.train) as Trip);
        }
        public List<Employee>? getEmployee()
        {
            if (!auth) { return null; }
            return employees.ConvertAll(employee => new ConstructableEmployee(employee.salary, employee.SSN, employee.username) as Employee);
        }

        private class ConstructableEmployee : Employee
        {
            public ConstructableEmployee(double salary, int SSN, string username, string password = "") : base(salary, SSN, username, password) {}

        }
        private class ConstructableTrip : Trip
        {
            public ConstructableTrip(int id, double price, DateTime date, Station from, Station to, Train train) : base(id, price, date, from, to, train) {}
        }
        private class ConstructableStation : Station
        {
            public ConstructableStation(string name, string location) : base(name, location) {}
        }
        private class ConstructableTrain : Train
        {
            public ConstructableTrain(int seats, int id) : base(seats, id) { }
        }

        public Employee? createEmployee(double salary, int SSN, string username, string password)
        {
            if (!auth || getEmployee(username) != null) return null;
            ConstructableEmployee employee = new(salary, SSN, username, password);
            employees.Add(employee);
            return employee;
        }
        public Train? createTrain(int seats, int id)
        {
            if (!auth || getTrain(id) != null) return null;
            ConstructableTrain train = new(seats, id);
            trains.Add(train);
            return train;
        }
        public Station? createStaion(string name, string location)
        {
            if (!auth || getStation(name) != null) return null;
            ConstructableStation station =  new(name, location);
            stations.Add(station);
            return station;
        }
        public Trip? createTrip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            if (!auth || getTrip(id) != null) return null;
            ConstructableTrip trip = new(id, price, date, from, to, train);
            trips.Add(trip);
            return trip;
        }
        public static Admin? createAdmin(int SSN, string username, string password, Admin? admin = null)
        {
            if (admins == null || (admin != null && admin.auth && getAdmin(username) == null ))
            {
                Admin newAdmin = new (SSN, username, password);
                admins?.Add(newAdmin);
                return newAdmin;
            }
            return null;
        }

        public static OnlinePassenger? loginPassenger(string username, string password)
        {
            OnlinePassenger? onlinePassenger = getOnlinePassenger(username);
            if (onlinePassenger != null && onlinePassenger.authenticate(password))
            {
                return onlinePassenger;
            }
            return null;
        }
        public static Employee? loginEmployee(string username, string password)
        {
            Employee? employee = getEmployee(username);
            if (employee != null && employee.authenticate(password))
            {
                return employee;
            }
            return null;
        }
        public static Admin? loginAdmin(string username, string password)
        {
            Admin? admin = getAdmin(username);
            if (admin != null && admin.authenticate(password))
            {
                return admin;
            }
            return null;
        }

        public void ticketsDateReport(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!auth) { return; }

            String? report = "";
            String? ticketDetails = "";
            int ticketCounter = 0;
            double totalPrice = 0;
            for (int i = 0; i < trips.Count; i++)
            {
                if ((dateFrom == null || dateFrom <= trips[i].date) && (dateTo == null || trips[i].date <= dateTo))
                {
                    for (int j = 0; j < trips[i].tickets.Count; j++)
                    {
                        totalPrice += trips[i].price;
                        ticketCounter++;
                        ticketDetails +=
                        $"Ticket ( {ticketCounter} )" +
                        $"ID: {trips[i].tickets[j].id}\n" +
                        $"Booking Date: {trips[i].tickets[j].BookingDate}\n" +
                        $"From: {trips[i].from.name}\n" +
                        $"To: {trips[i].to.name}\n" +
                        $"Price: {trips[i].price}" +
                        "\n\n********************************\n\n\n";
                    }
                }

            }
            report = $"Tickets Report Using Date ::\n\nTickets Number = {ticketCounter}\nTotal Tickets Price = {totalPrice}\n" + ticketDetails;
            Console.WriteLine(report);
        }
        public void ticketsFromReport(string stationName)
        {
            if (!auth) { return; }

            Station? station = getStation(stationName);
            int totalCount = 0;
            double totalPrice = 0;

            if (station != null)
            {
                Console.WriteLine($"Tickets booked from station {stationName}:");
                Console.WriteLine("----------------------------------------------");
                foreach (Trip trip in trips)
                {
                    if (trip.from.name == stationName)
                    {
                        totalCount += trip.tickets.Count;
                        totalPrice += trip.price * trip.tickets.Count;

                        Console.WriteLine($"\tTrip ID: {trip.id}:");
                        Console.WriteLine($"\tAvailable Seats: {trip.train.seatsCount}:");
                        Console.WriteLine($"\tBooked tickets count: {trip.tickets.Count}:");
                        Console.WriteLine($"\tTicket price: {trip.price}:");
                        Console.WriteLine($"\tBooked tickets price: {trip.price * trip.tickets.Count}:");

                        Console.WriteLine($"\n\tTickets:");
                        Console.WriteLine("----------------------------------------------");
                        foreach (Ticket ticket in trip.tickets)
                        {
                            Console.WriteLine("\t\tTicket ID: " + ticket);
                            Console.WriteLine("\t\tTrip ID: " + ticket.trip.id);
                            Console.WriteLine("\t\tTrip To: " + ticket.trip.to.location);
                            Console.WriteLine("\t\tTrip Date: " + ticket.trip.date);
                            Console.WriteLine("\t\tBooking Date: " + ticket.BookingDate);
                            Console.WriteLine("----------------------------------------------");
                        }
                    }
                }

            }
                Console.WriteLine($"\n\nTotal tickets count: {totalCount}");
                Console.WriteLine($"Total tickets price: {totalPrice}");
        }        
        public void ticketsToReport(string stationName)
        {
            if (!auth) { return; }

            Station? station = getStation(stationName);
            int totalCount = 0;
            double totalPrice = 0;

            if (station != null)
            {
                Console.WriteLine($"Tickets booked to station {stationName}:");
                Console.WriteLine("----------------------------------------------");
                foreach (Trip trip in trips)
                {
                    if (trip.to.name == stationName)
                    {
                        totalCount += trip.tickets.Count;
                        totalPrice += trip.price * trip.tickets.Count;

                        Console.WriteLine($"\tTrip ID: {trip.id}:");
                        Console.WriteLine($"\tAvailable Seats: {trip.train.seatsCount}:");
                        Console.WriteLine($"\tBooked tickets count: {trip.tickets.Count}:");
                        Console.WriteLine($"\tTicket price: {trip.price}:");
                        Console.WriteLine($"\tBooked tickets price: {trip.price * trip.tickets.Count}:");

                        Console.WriteLine($"\n\tTickets:");
                        Console.WriteLine("----------------------------------------------");
                        foreach (Ticket ticket in trip.tickets)
                        {
                            Console.WriteLine("\t\tTicket ID: " + ticket);
                            Console.WriteLine("\t\tTrip ID: " + ticket.trip.id);
                            Console.WriteLine("\t\tTrip To: " + ticket.trip.to.location);
                            Console.WriteLine("\t\tTrip Date: " + ticket.trip.date);
                            Console.WriteLine("\t\tBooking Date: " + ticket.BookingDate);
                            Console.WriteLine("----------------------------------------------");
                        }
                    }

                    Console.WriteLine($"\n\nTotal tickets count: {totalCount}");
                    Console.WriteLine($"Total tickets price: {totalPrice}");
                }
            }
        }
        public void ticketsEmployeeReport(string username, DateTime? dateFrom, DateTime? dateTo)
        {
            if (!auth) { return; }

            Employee? employee = getEmployee(username);
            double totalPrice = 0;

            if (employees == null)
                Console.WriteLine("Employee Doesn't Exist!");
            else
            {
                String? ticketShow = "";

                foreach (Ticket ticket in employee!.getTicket())
                {
                    if ((dateFrom == null || ticket.BookingDate >= dateFrom) && (dateTo == null || ticket.BookingDate <= dateTo))
                    {
                        ticketShow +=
                                $"ID: {ticket.id}\n" +
                                $"Booking Date: {ticket.BookingDate}\n" +
                                $"From: {ticket.trip.from.name}\n" +
                                $"To: {ticket.trip.to.name}\n" +
                                $"Price: {ticket.trip.price}" +
                                "\n\n********************************\n\n\n";

                        totalPrice += ticket.trip.price;
                    }
                }

                String report =
                    "Employee::\n" +
                    $"ID: {employee.SSN}\n" +
                    $"Username: {employee.username}\n" +
                    $"Salary: {employee.salary}\n************\n" +
                    "Tickets::\n" +
                    $"Total Number: {employee.getTicket().Count}\n" +
                    $"{ticketShow}\n"+
                    "***************\n"+
                    $"Total Price: {totalPrice}";
                Console.WriteLine(report);
            }
        }
    }
}
