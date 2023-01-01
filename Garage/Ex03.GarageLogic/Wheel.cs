using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void Inflate(float airToInflate) 
        {
            
            if (airToInflate + CurrentAirPressure > MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0f, MaxAirPressure - CurrentAirPressure);
            }
            m_CurrentAirPressure += airToInflate;
        }

        public string GetWheelDetails()
        {
            StringBuilder wheelDetails = new StringBuilder();

            wheelDetails.AppendFormat("ManufacturerName is: {0}\n", m_ManufacturerName);
            wheelDetails.AppendFormat("CurrentAirPressure is: {0}\n", m_CurrentAirPressure);
            wheelDetails.AppendFormat("MaxAirPressure is: {0}\n", r_MaxAirPressure);

            return wheelDetails.ToString();
        }

    }
}
