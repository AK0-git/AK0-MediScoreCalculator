using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediScoreCalculation
{
    public class PatientObservation
    {
        public DateTime Date
        {
            get;
            set;
        }

        public Observations Observations
        {
            get; 
            set;
        }

        public int Score
        {
            get;
            set;
        }

    }
}
