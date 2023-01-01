using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricBased : EnergySource
    {
        public ElectricBased(float i_engineTimeRemaing, float i_maxEngineRunTime)
            : base(i_engineTimeRemaing, i_maxEngineRunTime)
        {

        }

        public override float FillEnergySource(float i_EnergyToAdd, FuelBased.eFuelType? i_fuelType)
        {
            i_EnergyToAdd = i_EnergyToAdd / 60; //Turn from minutes to hours
            if ((base.m_energyRemaining + i_EnergyToAdd) > base.m_maxEnergy || i_EnergyToAdd < 0)
            {
                throw new ValueOutOfRangeException(0f, (MaxEnergy - EnergyRemaining) * 60);
            }
            else if (EnergyRemaining == MaxEnergy)
            {
                throw new ArgumentException("Battery already full");
            }
            else
            {
                base.m_energyRemaining += i_EnergyToAdd;
            }

            return EnergyRemaining / MaxEnergy * 100;
        }

        public override string GetEnergySourceDetails()
        {
            StringBuilder fuelStatus = new StringBuilder();

            fuelStatus.AppendFormat("Enery Source Type is: Battery\n");
            fuelStatus.AppendFormat("Max amount of energy is: {0}\n", base.MaxEnergy);
            fuelStatus.AppendFormat("Current amount of energy is: {0}\n", base.EnergyRemaining);

            return fuelStatus.ToString();
        }
    }
}
