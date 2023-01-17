using System;

namespace system
{
    internal class Program
    {
        private static readonly Dictionary<string, string> menues = new()
        {
            {"OnlinePassengerLanding",
                "1- Login\n"+
                "2- Signup\n"+
                "3- View Trips\n"+
                "0- Exit"},

            {"login",
                "usernaem:" }
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
                PrintMenu(menuName);
                Console.WriteLine("\nPlease enter a value between {0} and {1}", low, high);
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch { }
            

            while (option < low || option > high)
            {
                try
                {
                    PrintMenu(menuName);
                    Console.WriteLine("\nInvalid option, please enter a value between {0} and {1}", low, high);
                    option = Convert.ToInt32(Console.ReadLine());
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

        private static void LandPassenger()
        {
            int userOption;
            OnlinePassenger? passenger = null;

            do
            {
                userOption = GetUserOption("OnlinePassengerLanding", 0, 3);

                if (userOption == 1)
                {
                    passenger = loginPassenger();
                    if (passenger != null)
                    {

                    }
                }
                else if (userOption == 2)
                {
                    passenger = signupPassenger();
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