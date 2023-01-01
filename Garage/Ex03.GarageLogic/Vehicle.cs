using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_RemainingEnergyPercentage;
        private EnergySource m_energySource;
        private List<Wheel> m_Wheels = new List<Wheel>();

        public enum eVehicleType
        {
            FueledCar = 1,
            ElectricCar,
            FueledMotorCycle,         
            ElectricMotorCycle,
            FueledTruck
        }

        public Vehicle()
        {

        }


        public Vehicle(string i_ModelName, string i_LicenseNumber, float i_RemainingEnergyPercentage,
            EnergySource i_energySource, List<Wheel> i_wheels)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_RemainingEnergyPercentage = i_RemainingEnergyPercentage;
            m_energySource = i_energySource;
            foreach (Wheel wheel in i_wheels)
            {
                m_Wheels.Add(wheel);
            }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public float RemainingEnergyPercentage
        {
            get { return m_RemainingEnergyPercentage; }
            set { m_RemainingEnergyPercentage = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_energySource; }
            set { m_energySource = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public virtual string GetVehicleDetails()
        {
            StringBuilder VehicleDetails = new StringBuilder();

            VehicleDetails.AppendFormat(GetModelName());
            VehicleDetails.AppendFormat(GetLicenseNumber());
            VehicleDetails.AppendFormat(GetRemainingEnergyPercentage());
            VehicleDetails.AppendFormat(GetWheelDetails());

            return VehicleDetails.ToString();
        }

        public string GetLicenseNumber()
        {
            StringBuilder licenseDetails = new StringBuilder();

            licenseDetails.AppendFormat("License number is: {0}\n", LicenseNumber);

            return licenseDetails.ToString();
        }

        public string GetModelName()
        {
            StringBuilder modelNameDetails = new StringBuilder();

            modelNameDetails.AppendFormat("Model name is: {0}\n", ModelName);

            return modelNameDetails.ToString();
        }

        public string GetRemainingEnergyPercentage()
        {
            StringBuilder modelNameDetails = new StringBuilder();

            modelNameDetails.AppendFormat("Remaining energy percentage is: {0}\n", RemainingEnergyPercentage);

            return modelNameDetails.ToString();
        }

        public virtual string GetWheelDetails()
        {
            int wheelNumber = 1;
            StringBuilder wheelDetails = new StringBuilder();

            wheelDetails.AppendFormat("\t - Wheel Details - \n");
            foreach (Wheel wheel in m_Wheels)
            {
                wheelDetails.AppendFormat("wheel {0} -\n", wheelNumber++);
                wheelDetails.AppendFormat(wheel.GetWheelDetails());
            }

            return wheelDetails.ToString();
        }

    }
}
