using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private int m_NumOfDoors;

        public enum eCarColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        public Car(eCarColor i_CarColor, int i_NumOfDoors, string i_ModelName,
            string i_LicenseNumber, float i_RemainingEnergyPercentage, EnergySource i_energySource, List<Wheel> i_wheels)
            : base(i_ModelName, i_LicenseNumber, i_RemainingEnergyPercentage, i_energySource, i_wheels)
        {
            m_CarColor = i_CarColor;
            m_NumOfDoors = i_NumOfDoors;
        }
        public Car()
        {

        }

        public eCarColor carColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public int numOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public override string GetVehicleDetails()
        {
            StringBuilder carDetails = new StringBuilder();

            carDetails.AppendFormat("{0}", base.GetVehicleDetails());
            carDetails.AppendFormat("Car color is: {0}\n", m_CarColor.ToString());
            carDetails.AppendFormat("Number of doors in car is: {0}\n", m_NumOfDoors.ToString());

            return carDetails.ToString();
        }
    }
}
