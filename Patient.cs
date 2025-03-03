using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediScoreCalculation
{
    /// <summary>
    /// This class represents a very basic patient. It's used to store observations against a patient.
    /// </summary>
    public class Patient
    {
        private List<PatientObservation> HistoricalObservations = [];

        /// <summary>
        /// This method adds an observation for this patient at a particular time.
        /// </summary>
        /// <param name="date">The date/time the observations were made.</param>
        /// <param name="observations">The observations.</param>
        /// <returns>A risk flag based on if a Medi score has raised by more than 2 points within a 24-hour period for this patient.
        /// A value of true indicates a risk ha been flagged so could require an alert to be raised. A value of false indicates no risk.
        /// </returns>
        public bool AddObservations(DateTime date, Observations observations)
        {

            //Needed to get scores within last 24 hours
            DateTime startDate = date.AddDays(-1);

            MediScoreCalculator calculator = new MediScoreCalculator();
            int score = calculator.CalculateScore(observations);

            PatientObservation patientObservation = new PatientObservation
            {
                Observations = observations,
                Date = date,
                Score = score
            };

            HistoricalObservations.Add(patientObservation);
            
            var lowestScoredObservation = 
                HistoricalObservations
                    .Where(x => x.Date >= startDate)
                    .OrderBy(x => x.Score)
                    .First();

            var diff = score - lowestScoredObservation.Score;

            return diff > 1;
        }
    }
}
