using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleFile> r_VehicleDictionary;

        public Garage()
        {
            r_VehicleDictionary = new Dictionary<string, VehicleFile>();
        }

        public Dictionary<string, VehicleFile> VehicleDictionary
        {
            get { return r_VehicleDictionary; }
        }

        public bool IsInGarage(string i_LicenseNum)
        {
            return r_VehicleDictionary.ContainsKey(i_LicenseNum);
        }

        public void AddVehicleToGarage(string i_OwnerName, VehicleFile i_VehicleFile)
        {
            r_VehicleDictionary.Add(i_OwnerName, i_VehicleFile);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleFile.eVehicleStatus i_VehicleStatus)
        {
            bool vehicleInGarage = IsInGarage(i_LicenseNumber);

            if (!vehicleInGarage)
            {
                throw new ArgumentException("Vehicle with license number {0} is not in garage", i_LicenseNumber);
            }
            r_VehicleDictionary[i_LicenseNumber].Status = i_VehicleStatus;
        }

        public void Refuel(string i_LicenseNumber, float i_fuelToAdd, FuelBased.eFuelType? i_fuelType)
        {
            bool vehicleInGarage = IsInGarage(i_LicenseNumber);

            if (!vehicleInGarage)
            {
                throw new ArgumentException("Vehicle with license number {0} is not in garage", i_LicenseNumber);
            }
            float newPercentage = r_VehicleDictionary[i_LicenseNumber].Vehicle.EnergySource.FillEnergySource(i_fuelToAdd, i_fuelType);
            r_VehicleDictionary[i_LicenseNumber].Vehicle.RemainingEnergyPercentage = newPercentage;
        }

        public void InflateWheel(string i_LicenseNumber, float i_fuelToAdd, FuelBased.eFuelType? i_fuelType)
        {
            bool vehicleInGarage = IsInGarage(i_LicenseNumber);

            if (!vehicleInGarage)
            {
                throw new ArgumentException("Vehicle with license number {0} is not in garage", i_LicenseNumber);
            }
            float newPercentage = r_VehicleDictionary[i_LicenseNumber].Vehicle.EnergySource.FillEnergySource(i_fuelToAdd, i_fuelType);
            r_VehicleDictionary[i_LicenseNumber].Vehicle.RemainingEnergyPercentage = newPercentage;
        }

        public List<string> DisplayVehicelLicensesInGarage(VehicleFile.eVehicleStatus? i_VehicleStatus)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (VehicleFile vehicleFile in r_VehicleDictionary.Values)
            {
                if (i_VehicleStatus.HasValue)
                {
                    if (vehicleFile.Status == i_VehicleStatus)
                    {
                        licenseNumbers.Add(vehicleFile.Vehicle.LicenseNumber);
                    }
                }
                else
                {
                    licenseNumbers.Add(vehicleFile.Vehicle.LicenseNumber);
                }
            }

            return licenseNumbers;
        }

        public string DisplayFilesOfVehicle(string i_LicenseNumber)
        {
            IsInGarage(i_LicenseNumber);

            return r_VehicleDictionary[i_LicenseNumber].GetFileDetails();
        }
    }
}
