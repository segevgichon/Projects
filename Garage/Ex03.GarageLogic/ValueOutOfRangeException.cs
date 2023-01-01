using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public ValueOutOfRangeException(float i_minValue, float i_maxValue)
            : base($"Value not in range {i_minValue} to {i_maxValue}")
        {
            m_MinValue = i_minValue;
            m_MaxValue = i_maxValue;
        }

        public float MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }
    }
}
