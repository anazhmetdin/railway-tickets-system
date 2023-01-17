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
            Admin.createAdmin(12345, "admin", "admin")!
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

        public static List<Train> getTrain()
        {
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

        private class ConstructableEmployee : Employee
        {
            public ConstructableEmployee(int salary, int SSN, string username, string password) : base(salary, SSN, username, password) {}

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

        public Employee? createEmployee(int salary, int SSN, string username, string password)
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

        public static void ticketsDateReport(DateTime date)
        {

        }
        public static void ticketsFromReport(int stationID)
        {

        }
        
        public static void ticketsToReport(int stationID)
        {

        }
        public static void ticketsEmployeeReport(string username)
        {

        }
    }
}
