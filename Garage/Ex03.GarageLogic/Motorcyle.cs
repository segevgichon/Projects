using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ex03.GarageLogic
{
    public class Motorcyle : Vehicle
    {
        private eLicenseType m_LicenseType { get; set; }
        private int m_engineVolume { get; set; }

        public enum eLicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        public Motorcyle(eLicenseType i_licenseType, int i_engineVolume, string i_ModelName,
            string i_LicenseNumber, float i_RemainingEnergyPercentage, EnergySource i_energySource, List<Wheel> i_wheels)
            : base(i_ModelName, i_LicenseNumber, i_RemainingEnergyPercentage, i_energySource, i_wheels)
        {
            m_LicenseType = i_licenseType;
            m_engineVolume = i_engineVolume;
        }

        public override string GetVehicleDetails()
        {
            StringBuilder motorcycleDetails = new StringBuilder();

            motorcycleDetails.AppendFormat("{0}", base.GetVehicleDetails());
            motorcycleDetails.AppendFormat("License type is: {0}\n", m_LicenseType.ToString());
            motorcycleDetails.AppendFormat("Engine volume is: {0}\n", m_engineVolume.ToString());

            return motorcycleDetails.ToString();
        }
    }

}
