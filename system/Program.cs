using System;
using System.Text.Json;

namespace system
{
    internal class Program
    {
        private static int uniqueID = 1;
        private static int getID() { return uniqueID++; }

        private static readonly Dictionary<string, string> menues = new()
        {
            {"MainMenu",
                "1- Passenger\n"+
                "2- Employee\n"+
                "3- Admin\n"+
                "0- Exit"},

            {"OnlinePassengerLanding",
                "1- Login\n"+
                "2- Signup\n"+
                "3- View Trips\n"+
                "0- Return"},

            {"PassengerMenu",
                "1- Manage Tickets\n"+
                "2- Book a Ticket\n"+
                "0- Logout"},

            {"ManageOnlineTicket",
                "1- Cancel a Ticket\n"+
                "2- Book a New Ticket\n"+
                "3- Search in Your Tickets\n"+
                "0- Return"},

            {"EmployeeMenu",
                "1- Book a Ticket\n"+
                "0- Logout"},

            {"AdminMenu",
                "1- Add Station\n"+
                "2- Add Train\n"+
                "3- Add Trip\n"+
                "4- Add Employee\n"+
                "5- View Dashboard\n"+
                "0- Logout"},

            {"AdminDashboard",
                "1- Tickets per date\n"+
                "2- Tickets per source station\n"+
                "3- Tickets per destination station\n"+
                "4- Tickets per employee\n"+
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

        private static string GetPassword()
        {
            string pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        private static string[] GetUserNamePasswor()
        {
            string? username, password;

            Console.Clear();
            Console.WriteLine("Login\n\n");

            Console.WriteLine("Username: ");
            username = Console.ReadLine();

            Console.WriteLine("\nPassword: ");
            password = GetPassword();

            return new string[] { username!, password };
        }

        private static OnlinePassenger? loginPassenger()
        {
            string[] username_password;
            char key = '\0';
            OnlinePassenger? passenger = null;

            while (passenger == null && key != '0')
            {
                username_password = GetUserNamePasswor();

                passenger = Admin.loginPassenger(username_password[0], username_password[1]);

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
            string? username = "", password = "";
            char key = '\0';
            int SSN = 0;
            OnlinePassenger? passenger = null;

            while (passenger == null && key != '0')
            {
                Console.Clear();
                Console.WriteLine("Sign up\n\n");

                Console.WriteLine("Username:");
                if (username == "")
                {
                    username = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(username);
                }
                
                Console.WriteLine("\nPassword:");
                password = GetPassword();

                Console.WriteLine("\nConfirm Password:");
                if (password != GetPassword())
                {
                    Console.WriteLine("\nPasswords don't match, enter 0 to go back, press any key to try again");
                    password = "";
                    key = Console.ReadKey().KeyChar;
                    continue;
                }

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
                    Console.WriteLine("\nUsername not available, enter 0 to go back, or any key to try again");
                    username = "";
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

        private static void PrintTickets(List<Ticket> tickets)
        {
            Console.WriteLine("Tickets:\n");
            Console.WriteLine("----------------------------------------------");

            foreach (var ticket in tickets)
            {
                Console.WriteLine("Ticket ID: " + ticket.id);
                Console.WriteLine("Trip ID: " + ticket.trip.id);
                Console.WriteLine("Trip From: " + ticket.trip.from.location);
                Console.WriteLine("Trip To: " + ticket.trip.to.location);
                Console.WriteLine("Trip Date: " + ticket.trip.date);
                Console.WriteLine("Booking Date: " + ticket.BookingDate);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static void CancelOnlineTicket(OnlinePassenger passenger)
        {
            int ticketID = 0;

            do
            {
                Console.Clear();
                PrintTickets(passenger.getTicket());

                Console.WriteLine("\nEnter ticket ID, or press enter to cancel");
                try { ticketID = Int32.Parse(Console.ReadLine()!); }
                catch {
                    Console.WriteLine("\nInvalid ID, enter 0 to go back, or any key to try again");
                    ticketID = Int32.Parse(""+Console.ReadKey().KeyChar);
                    continue;
                }

                if (ticketID != 0)
                {
                    Console.WriteLine("\n Are you sure you want to delete this ticket: " + ticketID);
                    Console.WriteLine("press y to confirm, any other key to cancel");
                    if (Console.ReadKey().KeyChar == 'y')
                    {
                        if (passenger.cancelTicket(ticketID))
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
            while (ticketID != 0);
        }

        private static void PrintStations()
        {
            List<Station> stations = Admin.getStation();

            Console.WriteLine("\nStations:");
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
            while (tripID != 0 && trip == null);

            return trip;
        }

        private static List<Trip> FilterTrips()
        {
            Console.Clear();

            int? id = null;
            string? from = null, to = null;
            DateTime? fromDate = null, toDate = null;

            PrintStations();

            Console.WriteLine("\nTrip ID (empty string to ignore):");
            try
            {
                id = Int32.Parse(Console.ReadLine());
            }
            catch { }

            if (id == null)
            {
                Console.WriteLine("\nFrom which station? (name) (empty string to ignore)");
                from = Console.ReadLine();
                from = from == "" ? null : from;

                Console.WriteLine("\nTo which station? (name)");
                to = Console.ReadLine();
                to = to == "" ? null : to;

                Console.WriteLine("\nFrom which date? (example: 4/10/2009 13:00:00)");
                try { fromDate = Convert.ToDateTime(Console.ReadLine()); } catch { }

                Console.WriteLine("\nTo which date? (example: 4/10/2009 13:00:00)");
                try { toDate = Convert.ToDateTime(Console.ReadLine()); } catch { }
            }

            List<Trip> trips = Trip.getTrip(id, from, to, fromDate, toDate);
            return trips;
        }

        private static void BookOnlineTicket(OnlinePassenger passenger)
        {
            Trip? trip;
            char key = '\0';
            do
            {
                trip = PickTrip(FilterTrips());
                Console.Clear();

                if (trip != null)
                {
                    string? cardNumber, threeDigits;
                    do
                    {
                        Console.WriteLine("\nEnter Your Card Number to pay " + trip.price + " L.E");
                    } while ((cardNumber = Console.ReadLine()) == null);

                    do
                    {
                        Console.WriteLine("\nEnter the 3-digits security number");
                    }
                    while ((threeDigits = Console.ReadLine()) == null);

                    if (passenger.bookTicket(trip, cardNumber!, threeDigits!))
                    {
                        Console.WriteLine("\nTicket was booked successfully, press any key to go back");
                        
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\nSomething went wrong, press 0 to go back, or any key to try again");
                        key = Console.ReadKey().KeyChar;
                    }
                }
                else
                {
                    Console.WriteLine("\nNo trip was selected, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }
            while (trip == null && key != '0');
        }

        private static void SearchTicket(OnlinePassenger passenger)
        {
            char key = '\0';
            List<Ticket>? tickets = null;

            do
            {
                Console.Clear();

                string? from = null, to = null;
                DateTime? fromDate = null, toDate = null;
                int? id = null;

                PrintTickets(passenger.getTicket());

                try
                {
                    Console.WriteLine("\nTicket Number");
                    id = Int32.Parse(Console.ReadLine()!);
                }
                catch {}

                if (id == null)
                {
                    Console.WriteLine("\nFrom which station? (name)");
                    from = Console.ReadLine();
                    from = from == "" ? null : from;

                    Console.WriteLine("\nTo which station? (name)");
                    to = Console.ReadLine();
                    to = to == "" ? null : to;

                    Console.WriteLine("\nFrom which date? (example: 4/10/2009 13:00:00)");
                    try { fromDate = Convert.ToDateTime(Console.ReadLine()); } catch { }

                    Console.WriteLine("\nTo which date? (example: 4/10/2009 13:00:00)");
                    try { toDate = Convert.ToDateTime(Console.ReadLine()); } catch { }
                }

                tickets = passenger.getTicket(id, from, to, fromDate, toDate);

            }
            while (tickets == null && key != '0');

            if (tickets != null)
            {
                Console.Clear();
                PrintTickets(tickets);
            }
            else
            {
                Console.WriteLine("\nNo tickets were found");
            }

            Console.WriteLine("\nPress any key to go back");
            Console.ReadKey();
        }

        private static void ManageOnlineTickets(OnlinePassenger passenger)
        {
            int userOption;

            do
            {
                PrintMenu("ManageOnlineTicket");
                PrintTickets(passenger.getTicket());

                userOption = GetUserOption("", 0, 3);

                if (userOption == 1)
                {
                    CancelOnlineTicket(passenger);
                }
                else if (userOption == 2)
                {
                    BookOnlineTicket(passenger);
                }
                else if (userOption == 3)
                {
                    SearchTicket(passenger);
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

            passenger.logout();
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
                    if (passenger != null)
                    {
                        PassengerMenu(passenger);
                    }
                }
                else if (userOption == 3)
                {
                    printTrips(FilterTrips());                    

                    Console.WriteLine("\nPress any key to go back");
                    Console.ReadKey();
                }
            }
            while (userOption != 0);
        }

        private static Employee? LoginEmployee()
        {
            string[] username_password;
            char key = '\0';
            Employee? employee = null;

            while (employee == null && key != '0')
            {
                username_password = GetUserNamePasswor();

                employee = Admin.loginEmployee(username_password[0], username_password[1]);

                if (employee == null)
                {
                    Console.WriteLine("\nWrong username or password, enter 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }

            return employee;
        }

        private static void BookOfflineTicket(Employee employee)
        {
            Trip? trip;
            char key = '\0';
            do
            {
                trip = PickTrip(FilterTrips());
                Console.Clear();

                if (trip != null)
                {
                    if (employee.bookTicket(trip))
                    {
                        Console.WriteLine("\nTrip was booked successfully, press any key to continue");
                        
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("\nNo trip was selected, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }
            while (trip == null && key != '0');
        }

        private static void EmployeeMenu(Employee employee)
        {
            int userOption;

            do
            {
                userOption = GetUserOption("EmployeeMenu", 0, 3);

                if (userOption == 1)
                {
                    BookOfflineTicket(employee);
                }
            }
            while (userOption != 0);

            employee.logout();
        }

        private static void LandEmployee()
        {
            char userOption = '\0';
            Employee? employee;

            do
            {
                employee = LoginEmployee();

                if (employee == null)
                {
                    Console.WriteLine("Wrong username or password, enter 0 to Exit, or any key to continue");
                    userOption = Console.ReadKey().KeyChar;
                }
                else
                {
                    EmployeeMenu(employee);
                }
            }
            while (employee == null && userOption != '0');
        }

        private static Admin? LoginAdmin()
        {
            string[] username_password;
            char key = '\0';
            Admin? admin = null;

            while (admin == null && key != '0')
            {
                username_password = GetUserNamePasswor();

                admin = Admin.loginAdmin(username_password[0], username_password[1]);
            }

            return admin;
        }

        private static Station? AddStation(Admin admin)
        {
            string? name, location;
            Station? station;
            char key = '\0';
            do
            {
                Console.Clear();
                PrintStations();

                Console.WriteLine("\n Add a New Station");

                Console.WriteLine("\nStation Name:");
                name = Console.ReadLine();

                Console.WriteLine("\nStation Location");
                location = Console.ReadLine();

                if ((station = admin.createStaion(name!, location!)) != null)
                {
                    Console.WriteLine("\nThis name alread exists, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }
            while (key != '0' && station == null);

            return station;
        }

        private static void PrintTrains(Admin admin)
        {
            Console.WriteLine("\nTrains:\n");
            Console.WriteLine("----------------------------------------------");
            foreach (var train in admin.getTrain()!)
            {
                Console.WriteLine("Train ID: " + train.id);
                Console.WriteLine("Trip Seats Count: " + train.seatsCount);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static Train? AddTrain(Admin admin)
        {
            int seatsCount;
            Train? train;
            char key = '\0';
            do
            {
                Console.Clear();
                PrintTrains(admin);

                Console.WriteLine("\n Add a New Train");

                Console.WriteLine("\nSeats Count:");
                seatsCount = Int32.Parse(Console.ReadLine()!);

                if ((train = admin.createTrain(seatsCount, getID())) != null)
                {
                    Console.WriteLine("\nCouldn't add train, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }
            while (key != '0' && train == null);

            return train;
        }

        private static Trip? AddTrip(Admin admin)
        {
            Console.Clear();

            char key = '\0';

            string? from, to;
            DateTime date;
            float price;

            Station? fromStation, toStation;

            Trip? trip = null;

            do
            {
                PrintStations();

                do
                {
                    Console.WriteLine("\nFrom which station? (name)");
                    from = Console.ReadLine();
                } while (from != null);

                do
                {
                    Console.WriteLine("\nTo which station? (name)");
                    to = Console.ReadLine();
                } while (to != null);

                do
                {
                    Console.WriteLine("\nWhich date time? (example: 4/10/2009 13:00:00)");
                
                } while(!DateTime.TryParse(Console.ReadLine(), out date));

                do
                {
                    Console.WriteLine("\nWhat is the price");

                } while (!float.TryParse(Console.ReadLine(), out price));


                fromStation = Admin.getStation(from!);
                toStation = Admin.getStation(to!);

                if (fromStation == null || toStation == null)
                {
                    Console.WriteLine("Couldn't find these stations, press 0 to go back or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
            }
            while (fromStation != null && toStation != null && key != '0');

            if (key != '0')
            {
                Train? train;
                int trainID;

                do
                {
                    Console.Clear();
                    PrintTrains(admin);

                    Console.WriteLine($"\nFrom: {from}");
                    Console.WriteLine($"To: {to}");
                    Console.WriteLine($"On: {date}");

                    do
                    {
                        Console.WriteLine("Which train:");
                    } while (!Int32.TryParse(Console.ReadLine(), out trainID));

                    train = Admin.getTrain(trainID);

                    if (train == null)
                    {
                        Console.WriteLine("Couldn't find this train, press 0 to go back, or any key to try again");
                        key = Console.ReadKey().KeyChar;
                    }

                } while (train == null && key != '0');

                if (train != null)
                {
                    trip = admin.createTrip(getID(), price, date, fromStation!, toStation!, train);
                }
            }

            return trip;
        }

        private static Employee? AddEmployee(Admin admin)
        {
            Employee? employee = null;
            string? username = "", password = "";
            char key = '\0';
            int SSN = 0, salary = 0;

            while (employee == null && key != '0')
            {
                Console.Clear();
                Console.WriteLine("Sign up\n\n");

                Console.WriteLine("Username:");
                if (username == "")
                {
                    username = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(username);
                }

                Console.WriteLine("\nPassword:");
                password = GetPassword();

                Console.WriteLine("\nConfirm Password:");
                if (password != GetPassword())
                {
                    Console.WriteLine("\nPasswords don't match, enter 0 to go back, press any key to try again");
                    password = "";
                    key = Console.ReadKey().KeyChar;
                    continue;
                }

                try
                {
                    Console.WriteLine("\nSSN:");
                    SSN = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nInvalid SSN, enter 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                    continue;
                }

                do
                {
                    Console.WriteLine("\nSalary:");
                } while (int.TryParse(Console.ReadLine(), out salary));

                employee = admin.createEmployee(salary, SSN, username!, password);

                if (employee == null)
                {
                    Console.WriteLine("\nUsername not available, enter 0 to go back, or any key to try again");
                    username = "";
                    key = Console.ReadKey().KeyChar;
                }
            }

            return employee;
        }

        private static void ticketsDateReport(Admin admin)
        {

            Console.Clear();
            Console.WriteLine("Tickets per date\n");

            DateTime fromDateTime, toDateTime;
            do
            {
                Console.WriteLine("\nStart date (example: 4/10/2009 13:00:00)");
            }
            while (DateTime.TryParse(Console.ReadLine(), out fromDateTime));

            do
            {
                Console.WriteLine("\nEnd date (example: 4/10/2009 13:00:00)");
            }
            while (DateTime.TryParse(Console.ReadLine(), out toDateTime));

            Console.WriteLine();
            admin.ticketsDateReport(fromDateTime, toDateTime);
        }

        private static void ticketsFromReport(Admin admin)
        {
            Station? station;
            string? stationName;
            char key = '\0';

            do
            {
                Console.Clear();


                Console.WriteLine("Tickets per source station\n");
                PrintStations();

                do
                {
                    Console.WriteLine("\nStation Name (from):");
                } while((stationName = Console.ReadLine()) == null);

                station = Admin.getStation(stationName!);

                if (station == null)
                {
                    Console.WriteLine("\nWrong station name, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
                else
                {
                    admin.ticketsFromReport(stationName!);
                }
            } while(station == null && key != '0');
        }

        private static void ticketsToReport(Admin admin)
        {
            Station? station;
            string? stationName;
            char key = '\0';

            do
            {
                Console.Clear();
                Console.WriteLine("Tickets per destination station\n");
                PrintStations();

                do
                {
                    Console.WriteLine("\nStation Name (to):");
                } while ((stationName = Console.ReadLine()) == null);

                station = Admin.getStation(stationName!);

                if (station == null)
                {
                    Console.WriteLine("\nWrong station name, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
                else
                {
                    admin.ticketsToReport(stationName!);
                }
            } while (station == null && key != '0');
        }

        private static void PrintEmployees(Admin admin)
        {
            Console.Clear();

            Console.WriteLine("Employees usernames:");
            Console.WriteLine("--------------------------------------------");
            foreach (Employee employee in admin.getEmployee()!)
            {
                Console.WriteLine($"- {employee.username}");
            }
        }

        private static void ticketsEmployeeReport(Admin admin)
        {
            Employee? employee;
            string? username;
            char key = '\0';
            DateTime? fromDateTime = null, toDateTime = null;

            do
            {
                Console.Clear();
                Console.WriteLine("Tickets per employee");

                Console.WriteLine("\nEmployees usernames:");

                PrintEmployees(admin);

                do
                {
                    Console.WriteLine("\nEmployee username:");
                } while ((username = Console.ReadLine()) == null);

                Console.WriteLine("\nStart date (example: 4/10/2009 13:00:00)");
                try {fromDateTime = DateTime.Parse(Console.ReadLine());} catch {  }

                Console.WriteLine("\nEnd date (example: 4/10/2009 13:00:00)");
                try { toDateTime = DateTime.Parse(Console.ReadLine()); } catch { }

                employee = admin.getEmployee()!.Find(employee => employee.username == username);

                if (employee == null)
                {
                    Console.WriteLine("\nWrong username, press 0 to go back, or any key to try again");
                    key = Console.ReadKey().KeyChar;
                }
                else
                {
                    admin.ticketsEmployeeReport(username!, fromDateTime, toDateTime);
                }
            } while (employee == null && key != '0');
        }

        private static void ViewDashboard(Admin admin)
        {
            int userOption;

            do
            {
                userOption = GetUserOption("AdminDashboard", 0, 4);

                if (userOption == 1)
                {
                    ticketsDateReport(admin);
                }
                else if (userOption == 2)
                {
                    ticketsFromReport(admin);
                }
                else if (userOption == 3)
                {
                    ticketsToReport(admin);
                }
                else if (userOption == 4)
                {
                    ticketsEmployeeReport(admin);
                }


                Console.ReadKey();
            }
            while (userOption != 0);
        }

        private static void AdminMenu(Admin admin)
        {
            int userOption;

            do
            {
                userOption = GetUserOption("AdminMenu", 0, 5);

                if (userOption == 1)
                {
                    if (AddStation(admin) != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Station was added successfully, press any key to go back");
                        Console.ReadKey();
                    }
                }
                else if (userOption == 2)
                {
                    if (AddTrain(admin) != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Train was added successfully, press any key to go back");
                        Console.ReadKey();
                    }
                }
                else if (userOption == 3)
                {
                    if (AddTrip(admin) != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Trip was added successfully, press any key to go back");
                        Console.ReadKey();
                    }
                }
                else if (userOption == 4)
                {
                    if (AddEmployee(admin) != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Employee was added successfully, press any key to go back");
                        Console.ReadKey();
                    }
                }
                else if (userOption == 5)
                {
                    ViewDashboard(admin);
                }

                
            }
            while (userOption != 0);

            admin.logout();
        }

        private static void LandAdmin()
        {
            char userOption = '\0';
            Admin? admin;

            do
            {
                admin = LoginAdmin();

                if (admin == null)
                {
                    Console.WriteLine("Wrong username or password, enter 0 to Exit, or any key to continue");
                    userOption = Console.ReadKey().KeyChar;
                }
                else
                {
                    AdminMenu(admin);
                }
            }
            while (admin == null && userOption != '0');
        }

        private static void Main(string[] args)
        {
            Admin systemAdmin = Admin.loginAdmin("admin", "admin")!;
            systemAdmin.createTrain(200, 1);
            systemAdmin.createTrain(400, 2);
            systemAdmin.createTrain(400, 3);
            systemAdmin.createStaion("Cairo", "Cairo, Ramsis");
            systemAdmin.createStaion("Giza", "Giza");
            systemAdmin.createStaion("Alex", "Alexanderia, Mahatet Alraml");
            systemAdmin.createStaion("Fayoum", "Fayoum, Alsawaqi");
            systemAdmin.createEmployee(5600, 12345, "employee", "employee");
            systemAdmin.createTrip(getID(), 50, new DateTime(2023, 1, 24, 9, 30, 0), Admin.getStation("Cairo")!, Admin.getStation("Alex")!, Admin.getTrain(1)!);
            systemAdmin.createTrip(getID(), 50, new DateTime(2023, 1, 28, 13, 0, 0), Admin.getStation("Fayoum")!, Admin.getStation("Giza")!, Admin.getTrain(2)!);
            systemAdmin.createTrip(getID(), 50, new DateTime(2023, 1, 21, 8, 0, 0), Admin.getStation("Alex")!, Admin.getStation("Fayoum")!, Admin.getTrain(1)!);
            systemAdmin.logout();

            int userOption;

            do
            {
                userOption = GetUserOption("MainMenu", 0, 3);

                if (userOption == 1)
                {
                    LandPassenger();
                    
                }
                else if (userOption == 2)
                {
                    LandEmployee();
                    
                }
                else if (userOption == 3)
                {
                    LandAdmin();
                    
                }

            } while (userOption != 0);

        }
    }
}