using System;

namespace system
{
    internal class Program
    {
        private static int uniqueID = 0;
        private static int getID() { return uniqueID++; }

        private static readonly Dictionary<string, string> menues = new()
        {
            {"OnlinePassengerLanding",
                "1- Login\n"+
                "2- Signup\n"+
                "3- View Trips\n"+
                "0- Exit"},

            {"PassengerMenu",
                "1- Manage Tickets\n"+
                "2- Book Ticket\n"+
                "0- Logout"},

            {"ManageOnlineTicket",
                "1- Cancel Ticket\n"+
                "2- Book New Ticket\n"+
                "0- Return"}
        };

        private static void PrintMenu(string menuName)
        {
            Console.Clear();
            Console.WriteLine(menues[menuName]);
        }

        private static int GetUserOption(string menuName, int low, int high)
        {
            int option = -1;

            try
            {
                if (menuName != "")
                    PrintMenu(menuName);
                Console.WriteLine("\nPlease enter a value between {0} and {1}", low, high);
                option = Int32.Parse(""+Console.ReadKey().KeyChar);
            }
            catch { }
            

            while (option < low || option > high)
            {
                try
                {
                    if (menuName != "")
                        PrintMenu(menuName);
                    Console.WriteLine("\nInvalid option, please enter a value between {0} and {1}", low, high);
                    option = Int32.Parse("" + Console.ReadKey().KeyChar);
                }
                catch {}
            }

            return option;
        }

        private static OnlinePassenger? loginPassenger()
        {
            string? username, password;
            char key = '\0';
            OnlinePassenger? passenger = null;

            while (passenger == null && key != '0')
            {
                Console.Clear();
                Console.WriteLine("Login\n\n");

                Console.WriteLine("Username: ");
                username = Console.ReadLine();

                Console.WriteLine("\nPassword: ");
                password = Console.ReadLine();

                passenger = Admin.loginPassenger(username!, password!);

                if (passenger == null)
                {
                    Console.WriteLine("\nWrong username or password, enter 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }

            return passenger;
        }

        private static OnlinePassenger? signupPassenger()
        {
            string? username, password;
            char key = '\0';
            int SSN = 0;
            OnlinePassenger? passenger = null;

            while (passenger == null && key != '0')
            {
                Console.Clear();
                Console.WriteLine("Sign up\n\n");

                Console.WriteLine("Username:");
                username = Console.ReadLine();

                Console.WriteLine("\nPassword:");
                password = Console.ReadLine();

                try
                {
                    Console.WriteLine("\nSSN:");
                    SSN = Convert.ToInt32(Console.ReadLine());
                } catch {
                    Console.WriteLine("\nInvalid SSN, enter 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                    continue;
                }

                passenger = OnlinePassenger.signup(SSN, username!, password!);

                if (passenger == null)
                {
                    Console.WriteLine("\nWrong username or password, enter 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }

            return passenger;
        }

        private static void printTrips(List<Trip> trips)
        {
            Console.Clear();

            Console.WriteLine("Trips:\n");
            Console.WriteLine("----------------------------------------------");
            foreach (var trip in trips)
            {
                Console.WriteLine("Trip ID: " + trip.id);
                Console.WriteLine("Trip From: " + trip.from.location);
                Console.WriteLine("Trip To: " + trip.to.location);
                Console.WriteLine("Trip Date: " + trip.date);
                Console.WriteLine("Trip Price: " + trip.price + " L.E");
                Console.WriteLine("Has empty seats: " + trip.hasEmptySeats());
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static void PrintTickets(TicketOwner ticketOwner)
        {
            List<Ticket> tickets = ticketOwner.getTicket();

            Console.WriteLine("Tickets:\n");
            Console.WriteLine("----------------------------------------------");

            foreach (var ticket in tickets)
            {
                Console.WriteLine("Ticket ID: " + ticket);
                Console.WriteLine("Trip ID: " + ticket.trip.id);
                Console.WriteLine("Trip To: " + ticket.trip.to.location);
                Console.WriteLine("Trip Date: " + ticket.trip.date);
                Console.WriteLine("Booking Date: " + ticket.BookingDate);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static void CancelOnlineTicket(OnlinePassenger passenger)
        {
            int ticketID = 0;
            OnlineTicket? onlineTicket = null;

            do
            {
                Console.Clear();
                PrintTickets(passenger);

                Console.WriteLine("\nEnter ticket ID, or press enter to cancel");
                try { ticketID = Int32.Parse(Console.ReadLine()!); }
                catch {
                    Console.WriteLine("\nInvalid ID, enter 0 to go back, or any key to try again");
                    ticketID = Int32.Parse(""+Console.ReadKey().KeyChar);
                    continue;
                }

                onlineTicket = (OnlineTicket) passenger.getTicket(ticketID);

                if (onlineTicket != null)
                {
                    Console.WriteLine("\n Are you sure you want to delete this ticket: " + ticketID);
                    Console.WriteLine("press y to confirm, any other key to cancel");
                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        if (onlineTicket.cancelTicket())
                        {
                            Console.WriteLine("\nYour ticket has been cancelled");
                        }
                        else
                        {
                            Console.WriteLine("\nSomething went wrong, please try again later");
                        }

                        Console.WriteLine("Press any key to go back");
                        Console.ReadKey();
                    }
                }
            }
            while (onlineTicket == null || ticketID != 0);
        }

        private static void PrintStations()
        {
            List<Station> stations = Admin.getStation();

            Console.WriteLine("----------------------------------------------");
            foreach (Station station in stations)
            {
                Console.WriteLine("Name: " + station.name);
                Console.WriteLine("Location: " + station.location);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static Trip? PickTrip(List<Trip> trips)
        {
            int tripID;
            Trip? trip = null;

            do
            {
                printTrips(trips);

                Console.WriteLine("\nEnter trip ID to be booked:");
                try { tripID = Int32.Parse(Console.ReadLine()!); }
                catch
                {
                    Console.WriteLine("\nInvalid ID, enter 0 to go back, or any key to try again");
                    tripID = Int32.Parse("" + Console.ReadKey().KeyChar);
                    continue;
                }

                trip = Admin.getTrip(tripID);

                if (trip == null)
                {
                    Console.WriteLine("Wrong trip ID, enter 0 to go back or any key to try again");
                }
            }
            while (tripID != 0 || trip != null);

            return trip;
        }

        private static Trip? PickTrip()
        {
            Console.Clear();

            string? from = null, to = null;
            DateTime? fromDate = null, toDate = null;

            Console.WriteLine("Stations:");
            PrintStations();

            Console.WriteLine("From which station? (name)");
            from = Console.ReadLine();

            Console.WriteLine("To which station? (name)");
            to = Console.ReadLine();

            Console.WriteLine("From which date? (example: 4/10/2009 13:00:00)");
            try { fromDate = Convert.ToDateTime(Console.ReadLine()); } catch { }

            Console.WriteLine("To which date? (example: 4/10/2009 13:00:00)");
            try { toDate = Convert.ToDateTime(Console.ReadLine()); } catch { }

            List<Trip> trips = Trip.getTrips(from, to, fromDate, toDate);

            return PickTrip(trips);
        }

        private static void BookOnlineTicket(OnlinePassenger passenger)
        {
            Trip? trip = PickTrip();
            if (trip != null)
            {
                Console.Clear();

                Console.WriteLine("Enter Your Card Number to pay " + trip.price + " L.E");
                string? cardNumber = Console.ReadLine();
                Console.WriteLine("Enter the 3-digits security number");
                Console.ReadLine();

                Payment payment = new Payment(getID(), cardNumber!);

                if (payment.payTicket(trip.price))
                {
                    Ticket ticket = new OnlineTicket(payment, passenger, getID(), trip);
                    passenger.addTicket(ticket);
                }
            }
        }

        private static void ManageOnlineTickets(OnlinePassenger passenger)
        {
            int userOption;

            do
            {
                PrintMenu("ManageOnlineTicket");
                PrintTickets(passenger);

                userOption = GetUserOption("", 0, 2);

                if (userOption == 1)
                {
                    CancelOnlineTicket(passenger);
                }
                else if (userOption == 2)
                {
                    BookOnlineTicket(passenger);
                }
            }
            while (userOption != 0);
        }

        private static void PassengerMenu(OnlinePassenger passenger)
        {
            int userOption;

            do
            {
                userOption = GetUserOption("PassengerMenu", 0, 2);

                if (userOption == 1)
                {
                    ManageOnlineTickets(passenger);
                } 
                else if (userOption == 2)
                {
                    BookOnlineTicket(passenger);
                }
            }
            while (userOption != 0);
        }

        private static void LandPassenger()
        {
            int userOption;
            OnlinePassenger? passenger;

            do
            {
                userOption = GetUserOption("OnlinePassengerLanding", 0, 3);

                if (userOption == 1)
                {
                    passenger = loginPassenger();
                    if (passenger != null)
                    {
                        PassengerMenu(passenger);
                    }
                }
                else if (userOption == 2)
                {
                    passenger = signupPassenger();
                }
                else if (userOption == 3)
                {
                    printTrips(Admin.getTrip());                    

                    Console.WriteLine("\nPress any key to go back");
                    Console.ReadKey();
                }
            }
            while (userOption != 0);
        }

        private static void LandEmployee()
        {

        }

        private static void LandAdmin()
        {

        }

        private static void Main(string[] args)
        {
            Admin testAdmin = Admin.loginAdmin("admin", "admin")!;
            testAdmin.createTrain(200, 1);
            testAdmin.createTrain(400, 2);
            testAdmin.createTrain(400, 3);
            testAdmin.createStaion("Cairo", "Cairo, Ramsis");
            testAdmin.createStaion("Giza", "Giza");
            testAdmin.createStaion("Alex", "Alexanderia, Mahatet Alraml");
            testAdmin.createStaion("Fayoum", "Fayoum, Alsawaqi");
            testAdmin.createEmployee(5600, 12345, "employee", "employee");
            testAdmin.createTrip(1, 50, new DateTime(2023, 1, 24, 9, 30, 0), Admin.getStation("Cairo")!, Admin.getStation("Alex")!, Admin.getTrain(1)!);
            testAdmin.createTrip(2, 50, new DateTime(2023, 1, 28, 13, 0, 0), Admin.getStation("Fayoum")!, Admin.getStation("Giza")!, Admin.getTrain(2)!);
            testAdmin.createTrip(3, 50, new DateTime(2023, 1, 21, 8, 0, 0), Admin.getStation("Alex")!, Admin.getStation("Fayoum")!, Admin.getTrain(1)!);

            if (args.Length != 1)
            {
                throw new Exception("Specify user type only: passenger, employee, or admin");
            }

            if (args[0] == "passenger")
            {
                LandPassenger();
            }
            else if (args[0] == "employee")
            {
                LandEmployee();
            }
            else if (args[0] == "admin")
            {
                LandAdmin();
            }
        }
    }
}