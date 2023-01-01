using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_energyRemaining { get; set; }
        protected readonly float m_maxEnergy;

        public EnergySource(float i_energyRemaining, float i_maxEnergy)
        {
            m_energyRemaining = i_energyRemaining;
            m_maxEnergy = i_maxEnergy;
        }

        public float EnergyRemaining
        {
            get { return m_energyRemaining; }
            set { m_energyRemaining = value; }
        }

        public float MaxEnergy
        {
            get { return m_maxEnergy; }
        }

        public abstract string GetEnergySourceDetails();

        public abstract float FillEnergySource(float i_EnergyToAdd, FuelBased.eFuelType? i_fuelType);

    }
}
