using system;
using System;
using System.Runtime.Intrinsics.X86;

namespace system
{
    internal class Program
    {




        /*    private static Dictionary<string, string> menues = new()
            {
                {"main",
                    "1- login\n"+
                    "2- signup\n"+
                    "0- exit"},

                {"login",
                    "usernaem:" }
            };

            private static void PrintMenu(string menuName)
            {
                Console.WriteLine(menues[menuName]);
            }

            private static int GetUserOption(int low, int high)
            {
                int option;

                Console.WriteLine("Please enter a value between {0} and {1}", low, high);
                option = Console.ReadLine(); // convert to integer

                while (option < low || option < high)
                {
                    Console.WriteLine("Invalid option, please enter a value between {0} and {1}", low, high);
                }

                return option;
            }*/

        private static void Main(string[] args)
        {
            /*List<string> karim = new List<string>();
            karim.Add("help");
            karim.Add("jedl");

            bool v = karim.Any("help");
    */

            string? username = Console.ReadLine();
            string? password = Console.ReadLine();
            int SSN = Convert.ToInt32(Console.ReadLine());

            OnlinePassenger? op = Admin.loginPassenger(username!, password!);

            if (op != null)
            {
                Console.WriteLine(op.auth);
            }
            else
            {
                op = OnlinePassenger.signup(SSN, username!, password!);
                Console.WriteLine("Signed up");
            }

            Admin admin = Admin.loginAdmin("admin", "admin")!;
            Console.WriteLine(admin.auth);

/*
            Console.ReadLine($"Username: {0}\nPassword: {1}", username,password);*/





            /*List<int> numbers = new List<int> { 1, 2 };
            bool hasElements = numbers.Any();
*/

            /*Console.WriteLine("The list {0} empty.", hasElements ? "is not" : "is");
*/

            /*        int userOption = -1;

                    while (userOption != 0)
                    {
                        PrintMenu("main");

                        userOption = GetUserOption(0, 2);

                        Console.WriteLine(userOption);

                        if (userOption == 1)
                        {
                            PrintMenu("login");
                            Console.Read();
                        }
                    }
            */
        }
    }
}
