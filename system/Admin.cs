using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace system
{
    public class Admin : User
    {
        public Admin(int SSN, string username, string password) :base(SSN, username, password) 
        {
            if (auth)
                Admin.admins.Add(this);
        }

        static List<Admin> admins = new List<Admin>();
        static List<Train> trains = new List<Train>();
        static List<Station> staions = new();
        static List<OnlinePassenger> onlinePassengers = new List<OnlinePassenger>();
        static List<Trip> trips = new List<Trip>();
        static List<Employee> employees = new List<Employee>();

        public static void addToOnlinePassenger(OnlinePassenger passenger)
        {
            onlinePassengers.Add(passenger);
        }
        public static void addToEmployee(Employee employee)
        {
            employees.Add(employee);
        }
        public static Admin? getAdmin(string username)
        {
            foreach (var admin in admins)
            {
                if (admin.username == username)
                    return admin;
            }
            return null;
        }
        public static Employee? getEmployee(string username)
        {
            foreach (var employee in employees)
            {
                if (employee.username == username)
                    return employee;
            }
            return null;
        }
        public static OnlinePassenger? getOnlinePassenger(string username)
        {
            foreach (var onlinePassenger in onlinePassengers)
            {
                if (onlinePassenger.username== username)
                    return onlinePassenger;
            }
            return null;
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
            public ConstructableTrain(int seats, int id) : base(seats, id){}
        }
        public Employee createEmployee(int salary, int SSN, string username, string password)
        {
            return new ConstructableEmployee(salary, SSN, username, password);
        }
        public Train createTrain(int seats, int id)
        {
            return new ConstructableTrain(seats, id);
        }
        public Station createStaion(string name, string location)
        {
            return new ConstructableStation(name, location);
        }
        public Trip createTrip(int id, double price, DateTime date, Station from, Station to, Train train)
        {
            return new ConstructableTrip( id, price, date, from, to, train);
        }

        public override Admin? login(string username, string password)
        {
            Admin? admin = getAdmin(username);
            if (admin != null && admin.authenticated(password))
            {
                return admin;
            }
            else
                return null;
        }
    }
}
