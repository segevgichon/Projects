using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_CooledCargo;
        private float m_CargoTankVolume;

        public Truck(bool i_CooledCargo, float i_CargoTankVolume, string i_ModelName,
            string i_LicenseNumber, float i_RemainingEnergyPercentage, EnergySource i_energySource, List<Wheel> i_wheels)
            : base(i_ModelName, i_LicenseNumber, i_RemainingEnergyPercentage, i_energySource, i_wheels)
        {
            m_CooledCargo = i_CooledCargo;
            m_CargoTankVolume = i_CargoTankVolume;
        }

        public bool cooledCargo
        {
            get { return m_CooledCargo; }
            set { m_CooledCargo = value; }
        }

        public float cargoTankVolume
        {
            get { return m_CargoTankVolume; }
            set { m_CargoTankVolume = value; }
        }

        public override string GetVehicleDetails()
        {
            StringBuilder truckDetails = new StringBuilder();

            truckDetails.AppendFormat("{0}", base.GetVehicleDetails());
            truckDetails.AppendFormat("Contains cool cargo: {0}\n", m_CooledCargo.ToString());
            truckDetails.AppendFormat("Tank volume is: {0}\n", m_CargoTankVolume.ToString());

            return truckDetails.ToString();
        }
    }
}
