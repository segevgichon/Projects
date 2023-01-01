using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class UI
    {

        public enum eGarageCommands
        {
            Insert = 1,
            DisplayVehiclesInGarage,
            ChangeStatus,
            InflateTires,
            Refuel,
            ChargeBattery,
            DisplayVehicleFile,
            Quit
        }

        public static void PrintString(string i_StringToPrint)
        {
            Console.WriteLine(i_StringToPrint);
        }

        public static void PrintListOfStrings(List<string> i_ListOfStrings)
        {
            foreach (string str in i_ListOfStrings)
            {
                Console.WriteLine(str);
            }
        }

        public static string ReadString()
        {
            string userInput = Console.ReadLine();

            return userInput;
        }

        public static void InvalidInput()
        {
            Console.WriteLine("Invalid Input, try again:");
        }

        public static string ValidatePhoneNumber()
        {
            string phoneNumber;
            string headline = "Enter a 10 digit phone numeber";
            bool firstTry = true;
            do
            {
                if (!firstTry)
                {
                    InvalidInput();
                }
                UI.PrintString(headline);
                phoneNumber = Console.ReadLine();
                firstTry = false;
            }
            while (phoneNumber.Length != 10 || !phoneNumber.All(char.IsNumber));

            return phoneNumber;
        }

        public static string ValidateLicenseNum()
        {
            string licenseNumber;
            string headline = "Enter an 8 digit license plate numeber";
            bool firstTry = true;
            do
            {
                if (!firstTry)
                {
                    InvalidInput();
                }
                UI.PrintString(headline);
                licenseNumber = Console.ReadLine();
                firstTry = false;
            }
            while (licenseNumber.Length != 8 || !licenseNumber.All(char.IsNumber));

            return licenseNumber;
        }

        public static eGarageCommands GetGarageCommand()
        {
            eGarageCommands command = new eGarageCommands();
            int choice = DisplayEnumAndGetInputFromUser<eGarageCommands>(command);

            return (eGarageCommands)choice;
        }

        public static int DisplayEnumAndGetInputFromUser<T>(T i_Enum)
        {
            string[] options = Enum.GetNames(typeof(T));
            int index = 1, userChoice;
            foreach (string name in options)
            {
                Console.WriteLine("{0} - {1}", index++, name);
            }

            userChoice = GetEnumInputFromUser<T>(i_Enum);
            return userChoice;
        }

        public static int GetEnumInputFromUser<T>(T i_Enum)
        {
            int choice = 0;
            bool validChoice = false;

            do
            {
                try
                {
                    choice = StringToInt();
                    validChoice = Enum.IsDefined(typeof(T), choice);
                    if (!validChoice)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (Exception)
                {

                    InvalidInput();
                }
            }
            while (!validChoice);

            return choice;
        }

        public static int StringToInt()
        {
            bool isInt;
            string userInput;
            int choice = 0;

            do
            {
                isInt = true;
                try
                {
                    userInput = Console.ReadLine();
                    choice = int.Parse(userInput);
                }

                catch (FormatException)
                {
                    InvalidInput();
                    isInt = false;
                }
            }
            while (!isInt);

            return choice;
        }

        public static float StringToFloat()
        {
            bool isFloat;
            string userInput;
            float choice = 0;

            do
            {
                isFloat = true;
                try
                {
                    userInput = Console.ReadLine();
                    choice = float.Parse(userInput);
                }

                catch (FormatException)
                {
                    InvalidInput();
                    isFloat = false;
                }
            }
            while (!isFloat);

            return choice;
        }

        public static bool StringToBool()
        {
            string userChoice;
            string headline = "Please enter ( yes/no ):";
            bool firstTry = true;
            do
            {
                if (!firstTry)
                {
                    InvalidInput();
                    UI.PrintString(headline);
                }
                userChoice = Console.ReadLine();
                firstTry = false;
            }
            while (!(userChoice.Equals("yes") || userChoice.Equals("no")));

            if (userChoice.Equals("yes"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }
    }
}
