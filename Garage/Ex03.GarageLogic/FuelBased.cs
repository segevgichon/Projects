using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelBased : EnergySource
    {
        private readonly eFuelType m_fuelType;

        public enum eFuelType
        {
            Soler = 1,
            Octane_95,
            Octane_96,
            Octane_98,
        }
        public FuelBased(eFuelType i_fuelType, float i_fuelRemaining, float i_maxFuel) : base(i_fuelRemaining, i_maxFuel)
        {
            m_fuelType = i_fuelType;
        }

        public override float FillEnergySource(float i_fuelToAdd, eFuelType? i_fuelType)
        {
            if (m_fuelType != i_fuelType)
            {
                throw new ArgumentException($"{i_fuelType.ToString()} is not the correct fuel type.");
            }
            else if ((base.m_energyRemaining + i_fuelToAdd) > base.m_maxEnergy || i_fuelToAdd < 0)
            {
                throw new ValueOutOfRangeException(0f, MaxEnergy - EnergyRemaining);
            }
            else if (base.m_energyRemaining == base.m_maxEnergy)
            {
                throw new ArgumentException("Tank already full");
            }
            else
            {
                base.m_energyRemaining += i_fuelToAdd;
            }

            return EnergyRemaining / MaxEnergy * 100;
        }

        public override string GetEnergySourceDetails()
        {
            StringBuilder fuelStatus = new StringBuilder();

            fuelStatus.AppendFormat("Fuel Type is: {0}\n", m_fuelType);
            fuelStatus.AppendFormat("Max amount of fuel is: {0}\n", base.MaxEnergy);
            fuelStatus.AppendFormat("Current amount of fuel is: {0}\n", base.EnergyRemaining);

            return fuelStatus.ToString();
        }
    }
}
