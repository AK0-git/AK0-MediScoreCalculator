using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediScoreCalculation
{
    public class Observations
    {
        public AirOrOxygenEnum AirOrOxygen
        {
            get;
            set;
        }
        public ConsciousnessEnum Consciousness
        {
            get; 
            set;
        }
        public CapillaryBloodGlucoseEnum CapillaryBloodGlucoseType
        {
            get;
            set;
        }
        public float CapillaryBloodGlucose
        {
            get;
            set;
        }
        public int Respiration
        {
            get; 
            set;
        }
        public int SpO2
        {
            get; 
            set;
        }
        public float Temperature
        {
            get; 
            set;
        } 
    }
}
