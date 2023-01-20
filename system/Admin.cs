﻿using System;
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
        public static List<Trip>? getTrips()
        {
            return trips;
        }
        #region Construcoting Classes
        private class ConstructableEmployee : Employee
        {
            public ConstructableEmployee(int salary, int SSN, string username, string password = "") : base(salary, SSN, username, password) {}

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
        #endregion
        #region Creation Methods
        public Employee createEmployee(int salary, int SSN, string username, string password)
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
            return new ConstructableTrip( id, price, date, from, to, train);
        }
        #endregion
        #region Login
        public override Admin? login(string username, string password)
        {
            Admin? admin = getAdmin(username);
            if (admin != null && admin.authenticate(password))
            {
                return admin;
            }
            return null;
        }
        #endregion

        #region Reports

        #region Report 1
        public void ticketsDateReport(DateTime dateFrom, DateTime dateTo)
        {
            String? report = default(String?);
            String? ticketDetails = default(String?);
            int ticketCounter = 0;
            double totalPrice = default;
            for (int i = 0; i < trips.Count;i++)
            {
                if (dateFrom <= trips[i].date && trips[i].date <= dateTo)
                {
                    for (int j = 0; j < trips[i].tickets.Count; j++)
                    {
                        totalPrice += trips[i].price;
                        ticketCounter++;
                        ticketDetails +=
                        $"Ticket ( {ticketCounter} )" +
                        $"ID: {trips[i].tickets[j].id}\n" +
                        $"Booking Date: {trips[i].tickets[j].BookingDate}\n" +
                        $"From: {trips[i].from}\n" +
                        $"To: {trips[i].to}\n" +
                        $"Price: {trips[i].price}" +
                        "********************************\n";
                    }
                }

            }
            report = $"Tickets Report Using Date ::\n\nTickets Number = {ticketCounter}\nTotal Tickets Price = {totalPrice}\n"  + ticketDetails;
            Console.WriteLine(report);
        }
        #endregion

        #region Report 2
        public void ticketsFromReport(string stationName)
        {

        }

        #endregion
        #region Report 3
        public void ticketsToReport(string stationName)
        {
        }
        #endregion
        #region Report 4
        public void ticketsEmployeeReport(string username, DateTime dateFrom, DateTime dateTo)
        {
            Employee? employee = getEmployee(username);
            if (employees == null)
                Console.WriteLine("Employee Doesn't Exist!");
            else
            {
                String? ticketShow = default(String?);
                
                foreach(Ticket ticket in employee.getTicket())
                {
                    ticketShow =
                            $"ID: {ticket.id}\n" +
                            $"Booking Date: {ticket.BookingDate}\n" +
                            $"From: {ticket.trip.from}\n" +
                            $"To: {ticket.trip.to}\n" +
                            $"Price: {ticket.trip.price}" +
                            "********************************\n";
                }
                
                String report =
                    "Employee::\n" +
                    $"ID: {employee.SSN}\n" +
                    $"Username: {employee.username}\n" +
                    $"Salary: {employee.salary}\n************\n" +
                    "Tickets::\n" +
                    $"Total Number: {employee.getTicket().Count}\n" +
                    $"{ticketShow}";
                Console.WriteLine(report);
            }
        }
        #endregion
        #endregion

    }
}
