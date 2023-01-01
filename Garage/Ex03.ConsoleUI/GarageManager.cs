using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private Garage m_Garage;

        public GarageManager()
        {
            m_Garage = new Garage();
        }

        public void OpenGarage()
        {
            UI.eGarageCommands command;
            string headLine = "Please choose what you would like to do in the garage:";
            bool finished;
            do
            {
                UI.PrintString(headLine);
                command = UI.GetGarageCommand();
                UI.ClearScreen();
                finished = false;
                switch (command)
                {
                    case UI.eGarageCommands.Insert:
                        InsertVehicle();
                        break;
                    case UI.eGarageCommands.DisplayVehiclesInGarage:
                        DisplayVehiclesInGarage();
                        break;
                    case UI.eGarageCommands.ChangeStatus:
                        ChangeStatus();
                        break;
                    case UI.eGarageCommands.InflateTires:
                        InflateTires();
                        break;
                    case UI.eGarageCommands.Refuel:
                        RefuelVehicle();
                        break;
                    case UI.eGarageCommands.ChargeBattery:
                        ChargeBattery();
                        break;
                    case UI.eGarageCommands.DisplayVehicleFile:
                        DisplayVehicleFiles();
                        break;
                    case UI.eGarageCommands.Quit:
                        finished = true;
                        break;
                    default:
                        UI.InvalidInput();
                        break;
                }
                UI.PrintString("Press Enter to continue");
                UI.ReadString();
                UI.ClearScreen();
            }
            while (!finished);

        }

        private void InsertVehicle()
        {
            string licenseNumber = GetLicenseNumInput();
            if (m_Garage.IsInGarage(licenseNumber))
            {
                UI.PrintString("Vehicle already in garage. Status has changed to - 'InRepair'");
                m_Garage.ChangeVehicleStatus(licenseNumber, VehicleFile.eVehicleStatus.InRepair);
            }
            else
            {
                Vehicle newVehicle = SetNewVehicle(licenseNumber);
                VehicleFile vehicleFile = getVehicleFile(newVehicle);
                m_Garage.AddVehicleToGarage(licenseNumber, vehicleFile);
                UI.PrintString("Vehicele entered garage!");
            }
        }

        private void DisplayVehiclesInGarage()
        {
            bool validInput;
            string headLine;
            int choice;
            List<string> vehiclesToDisplay = null;

            headLine = "Press 0 to see all vehicle license numbers in garage OR Press 1 to filter them according to status:";
            UI.PrintString(headLine);
            do
            {
                validInput = true;
                choice = UI.StringToInt();
                if (choice == 0)
                {
                    vehiclesToDisplay = m_Garage.DisplayVehicelLicensesInGarage(null);
                }
                else if (choice == 1)
                {
                    VehicleFile.eVehicleStatus statusesInGarage = new VehicleFile.eVehicleStatus();
                    choice = UI.DisplayEnumAndGetInputFromUser(statusesInGarage);
                    VehicleFile.eVehicleStatus statusFilter = (VehicleFile.eVehicleStatus)choice;
                    vehiclesToDisplay = m_Garage.DisplayVehicelLicensesInGarage(statusFilter);
                }
                else
                {
                    UI.InvalidInput();
                    validInput = false;
                }
            }
            while (!validInput);

            UI.PrintListOfStrings(vehiclesToDisplay);
        }

        private void ChangeStatus()
        {
            string licenseNumberToChange;
            int StatusToChangeTo;
            VehicleFile.eVehicleStatus vehicleStatus = new VehicleFile.eVehicleStatus();
            string licenseNumberHeadline = "What is the license number of your vehicle?";
            string statusChangeHeadline = "Please choose desired status:";

            UI.PrintString(licenseNumberHeadline);
            licenseNumberToChange = GetLicenseNumInput();
            UI.PrintString(statusChangeHeadline);
            StatusToChangeTo = UI.DisplayEnumAndGetInputFromUser(vehicleStatus);

            try
            {
                m_Garage.ChangeVehicleStatus(licenseNumberToChange, (VehicleFile.eVehicleStatus)StatusToChangeTo);
            }
            catch (ArgumentException exception)
            {
                UI.PrintString(exception.Message);
                UI.InvalidInput();
            }

        }

        private void InflateTires()
        {
            string licenseNumber;
            int wheelNumber = 1;
            float airToInflate;
            bool validAirAmount;
            licenseNumber = GetLicenseNumInput();
            try
            {
                if (!m_Garage.IsInGarage(licenseNumber))
                {
                    throw new ArgumentException();
                }
                else
                {
                    foreach (Wheel wheel in m_Garage.VehicleDictionary[licenseNumber].Vehicle.Wheels)
                    {
                        do
                        {
                            validAirAmount = true;
                            try
                            {
                                UI.PrintString($"Please enter air to fill in wheel {wheelNumber++}");
                                airToInflate = UI.StringToFloat();
                                wheel.Inflate(airToInflate);
                            }
                            catch (Exception e)
                            {
                                UI.PrintString(e.Message);
                                validAirAmount = false;
                                wheelNumber--;
                            }
                        }
                        while (!validAirAmount);
                    }
                }
            }
            catch (ArgumentException)
            {
                UI.PrintString("The license plate you entered is not in the garage");
            }

        }

        private void RefuelVehicle()
        {
            string licenseNumber;
            float fuelToAdd;
            int choice;
            bool validFuelAmount;
            FuelBased.eFuelType fuelType = new FuelBased.eFuelType();
            licenseNumber = GetLicenseNumInput();
            try
            {
                if (!m_Garage.IsInGarage(licenseNumber))
                {
                    throw new ArgumentException();
                }
                else
                {
                    do
                    {
                        try
                        {
                            validFuelAmount = true;
                            UI.PrintString("Please enter fuel type");
                            choice = UI.DisplayEnumAndGetInputFromUser(fuelType);
                            UI.PrintString("Please enter amount of fuel to add");
                            fuelToAdd = UI.StringToFloat();
                            m_Garage.Refuel(licenseNumber, fuelToAdd, (FuelBased.eFuelType)choice);
                        }
                        catch (Exception e)
                        {
                            UI.PrintString(e.Message);
                            validFuelAmount = false;
                        }
                    }
                    while (!validFuelAmount);
                }
            }
            catch (ArgumentException)
            {
                UI.PrintString("The license plate you entered is not in the garage");
            }

        }

        private void ChargeBattery()
        {
            string licenseNumber;
            float minutesToAdd;
            bool validChargeAmount;
            licenseNumber = GetLicenseNumInput();
            try
            {
                if (!m_Garage.IsInGarage(licenseNumber))
                {
                    throw new ArgumentException();
                }
                else
                {
                    do
                    {
                        try
                        {
                            validChargeAmount = true;
                            UI.PrintString("Please enter amount of minutes to charge");
                            minutesToAdd = UI.StringToFloat();
                            m_Garage.Refuel(licenseNumber, minutesToAdd, null);
                        }
                        catch (Exception e)
                        {
                            UI.PrintString(e.Message);
                            validChargeAmount = false;
                        }
                    }
                    while (!validChargeAmount);
                }
            }
            catch (ArgumentException)
            {
                UI.PrintString("The license plate you entered is not in the garage");
            }

        }
        
        private void DisplayVehicleFiles()
        {
            string licenseNumber, vehicleFiles;
            UI.PrintString("Please enter your license Number:");
            licenseNumber = UI.ReadString();
            try
            {
                vehicleFiles = m_Garage.DisplayFilesOfVehicle(licenseNumber);
                UI.PrintString(vehicleFiles);
            }
            catch (Exception)
            {
                UI.PrintString("Vehicle not in garage");
            }


        }

        private static Vehicle SetNewVehicle(string i_licenseNumber)
        {
            bool validInput;

            //Vehicle
            Vehicle vehicle;
            Vehicle.eVehicleType eVehicleType = getVehicleTypeInput();
            string modelName = GetModelName();
            float remaingingEnergyPercentage = GetRemainingEnergyPercentage();
            List<Wheel> wheels;
            EnergySource energySource;

            //Car
            Car.eCarColor carColor;
            int numOfDoors;

            //Motorcyle
            int engineVolume;
            Motorcyle.eLicenseType elicenseType;

            //Truck
            bool cooledCargo;
            float cargoTankVolume;

            do
            {
                validInput = true;
                switch (eVehicleType)
                {
                    case Vehicle.eVehicleType.ElectricCar:
                        carColor = GetCarColorInput();
                        numOfDoors = GetNumberOfDoors();

                        wheels = GetWheelParams(4, 29f);
                        energySource = new ElectricBased((3.3f) * (remaingingEnergyPercentage / 100f), 3.3f);
                        vehicle = new Car(carColor, numOfDoors, modelName, i_licenseNumber, remaingingEnergyPercentage,
                            energySource, wheels);
                        break;

                    case Vehicle.eVehicleType.FueledCar:
                        carColor = GetCarColorInput();
                        numOfDoors = GetNumberOfDoors();

                        wheels = GetWheelParams(4, 29f);
                        energySource = new FuelBased(FuelBased.eFuelType.Octane_95, (38f * (remaingingEnergyPercentage / 100f)), 38f);
                        vehicle = new Car(carColor, numOfDoors, modelName, i_licenseNumber, remaingingEnergyPercentage,
                            energySource, wheels);
                        break;

                    case Vehicle.eVehicleType.ElectricMotorCycle:
                        elicenseType = GetLicenseTypeInput();
                        engineVolume = GetEngineVolumeInput();

                        wheels = GetWheelParams(2, 31f);
                        energySource = new ElectricBased((2.5f) * (remaingingEnergyPercentage / 100f), 2.5f);
                        vehicle = new Motorcyle(elicenseType, engineVolume, modelName, i_licenseNumber, remaingingEnergyPercentage,
                            energySource, wheels);
                        break;

                    case Vehicle.eVehicleType.FueledMotorCycle:
                        elicenseType = GetLicenseTypeInput();
                        engineVolume = GetEngineVolumeInput();

                        wheels = GetWheelParams(2, 31f);
                        energySource = new FuelBased(FuelBased.eFuelType.Octane_98, (6.2f) * (remaingingEnergyPercentage / 100f), 6.2f);
                        vehicle = new Motorcyle(elicenseType, engineVolume, modelName, i_licenseNumber, remaingingEnergyPercentage,
                            energySource, wheels);
                        break;

                    case Vehicle.eVehicleType.FueledTruck:
                        cooledCargo = GetIfCooledCargo();
                        cargoTankVolume = GetCargoTankVolume();

                        wheels = GetWheelParams(16, 24f);
                        energySource = new FuelBased(FuelBased.eFuelType.Soler, (120f) * (remaingingEnergyPercentage / 100f), 120f);
                        vehicle = new Truck(cooledCargo, cargoTankVolume, modelName, i_licenseNumber, remaingingEnergyPercentage,
                               energySource, wheels);
                        break;

                    default:
                        UI.InvalidInput();
                        validInput = false;
                        vehicle = new Car();
                        break;
                }
            }
            while (!validInput);
            return vehicle;
        }

        private static string GetLicenseNumInput()
        {
            string licenseNumber = UI.ValidateLicenseNum();
            
            return licenseNumber;
        }

        private static Vehicle.eVehicleType getVehicleTypeInput()
        {
            Vehicle.eVehicleType vehicleType = new Vehicle.eVehicleType();
            string headline = "Please choose your vehicle type:";
            UI.PrintString(headline);
            int choice = UI.DisplayEnumAndGetInputFromUser(vehicleType);

            return (Vehicle.eVehicleType)choice;
        }

        private static Motorcyle.eLicenseType GetLicenseTypeInput()
        {
            Motorcyle.eLicenseType licenseType = new Motorcyle.eLicenseType();
            string headline = "Please choose your license type";
            UI.PrintString(headline);
            int choice = UI.DisplayEnumAndGetInputFromUser(licenseType);

            return (Motorcyle.eLicenseType)choice;
        }

        private static Car.eCarColor GetCarColorInput()
        {
            Car.eCarColor color = new Car.eCarColor();
            string headline = "Please give your car color";
            UI.PrintString(headline);
            int choice = UI.DisplayEnumAndGetInputFromUser(color);
           
            return (Car.eCarColor)choice;
        }

        private static int GetEngineVolumeInput()
        {
            bool validVolume;
            int volume;
            do
            {
                validVolume = true;
                UI.PrintString("Please enter engine volume");
                volume = UI.StringToInt();
                if (volume < 0)
                {
                    validVolume = false;
                    UI.InvalidInput();
                }
            }
            while (!validVolume);
            return volume;
        }

        private static bool GetIfCooledCargo()
        {
            UI.PrintString("Does your truck contain cooled cargo");
            return UI.StringToBool();
        }

        private static float GetCargoTankVolume()
        {
            bool validVolume;
            float volume;
            do
            {
                validVolume = true;
                UI.PrintString("Please enter the trucks cargo tank volume");
                volume = UI.StringToFloat();
                if (volume < 0)
                {
                    validVolume = false;
                    UI.InvalidInput();
                }
            }
            while (!validVolume);
            return volume;
        }

        private static int GetNumberOfDoors()
        {
            bool validNumDoors;
            int numDoors;
            do
            {
                validNumDoors = true;
                UI.PrintString("Please enter the number of doors your car has (2, 3, 4, or 5)");
                numDoors = UI.StringToInt();
                if (numDoors < 2 || numDoors > 5)
                {
                    validNumDoors = false;
                    UI.InvalidInput();
                }
            }
            while (!validNumDoors);

            return numDoors;

        }

        private static VehicleFile getVehicleFile(Vehicle i_Vehicle)
        {
            UI.PrintString("Enter owner name");
            string ownerName = UI.ReadString();
            UI.PrintString("Enter phone number");
            string phoneNumber = UI.ValidatePhoneNumber();

            return new VehicleFile(i_Vehicle, ownerName, phoneNumber);
        }

        private static string GetModelName()
        {
            UI.PrintString("Enter your car model");

            return UI.ReadString();
        }

        private static float GetRemainingEnergyPercentage()
        {
            float percentage;
            bool validPercentage = true;
            UI.PrintString("Enter your vehicles remaining energy percentage, value from 0-100");

            do
            {
                percentage = UI.StringToFloat();

                try
                {
                    if (percentage < 0 || percentage > 100)
                    {
                        validPercentage = false;
                        throw new ValueOutOfRangeException(0f, 100f);
                    }
                }
                catch (Exception e)
                {
                    UI.PrintString(e.Message);
                }
            }
            while (!validPercentage);

            return percentage;
        }

        public static List<Wheel> GetWheelParams(int i_NumOfWheels, float i_MaxAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>(i_NumOfWheels);
            Wheel wheel;
            string manufacturerName;
            float currentAirPressure;
            for (int i = 1; i <= i_NumOfWheels; i++)
            {
                UI.PrintString($"Enter wheel {i} manufacturer name");
                manufacturerName = UI.ReadString();
                UI.PrintString($"Enter wheel {i} current air pressure from 0 to {i_MaxAirPressure}");
                currentAirPressure = GetCurrentAirPressure(i_MaxAirPressure);
                wheel = new Wheel(manufacturerName, i_MaxAirPressure, currentAirPressure);
                wheels.Add(wheel);
            }

            return wheels;
        }

        private static float GetCurrentAirPressure(float i_MaxAirPressure)
        {
            float CurrentAirPressure;
            bool validRange;

            do
            {
                validRange = true;
                CurrentAirPressure = UI.StringToFloat();
                try
                {
                    if (CurrentAirPressure < 0 || CurrentAirPressure > i_MaxAirPressure)
                    {
                        validRange = false;
                        throw new ValueOutOfRangeException(0f, i_MaxAirPressure);
                    }
                }
                catch (Exception e)
                {
                    UI.PrintString(e.Message);
                }
            }
            while (!validRange);
            return CurrentAirPressure;
        }
    }
}
