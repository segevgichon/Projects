using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFile
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_PhoneNumber; 
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InRepair;

        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Payed,
        }

        public VehicleFile(Vehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_PhoneNumber = i_PhoneNumber;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string PhoneNumber
        {
            get { return m_PhoneNumber; }
            set { m_PhoneNumber = value; }
        }

        public eVehicleStatus Status
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public string GetFileDetails()
        {
            StringBuilder FileDetails = new StringBuilder();

            FileDetails.AppendFormat("Vehicle type is: {0}\n", m_Vehicle.GetType().Name);
            FileDetails.AppendFormat("Owner name is: {0}\n", m_OwnerName);
            FileDetails.AppendFormat("Owner phone number is: {0}\n", m_PhoneNumber);
            FileDetails.AppendFormat("Vehicle status is: {0}\n", m_VehicleStatus.ToString());
            FileDetails.AppendFormat(m_Vehicle.GetVehicleDetails());
            FileDetails.AppendFormat(m_Vehicle.EnergySource.GetEnergySourceDetails());

            return FileDetails.ToString();
        }

    }
}
