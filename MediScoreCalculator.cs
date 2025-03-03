using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediScoreCalculation
{
    /// <summary>
    /// This class provides functionality to calculate a Medi score.
    /// </summary>
    public class MediScoreCalculator
    {
        /// <summary>
        /// This method calculates and returns a Medi score based on the supplied observations.
        /// </summary>
        /// <param name="observations">The observations.</param>
        /// <returns>The Medi score.</returns>
        public int CalculateScore(Observations observations)
        {
            int score = 0;

            if (observations.AirOrOxygen == AirOrOxygenEnum.Oxygen)
            {
                score += 2;
            }

            if (observations.Consciousness == ConsciousnessEnum.CVPU)
            {
                score += 3;
            }

            score += RespirationScore(observations.Respiration);

            score += SpO2Score(observations.SpO2, observations.AirOrOxygen);

            score += TemperatureScore(observations.Temperature);

            score += CapillaryBloodGlucoseScore(observations.CapillaryBloodGlucoseType, observations.CapillaryBloodGlucose);

            return score;
        }


        private static int RespirationScore(int respiration)
        {
            int score = 0;

            if (respiration <= 8)
            {
                score += 3;
            }
            else if (respiration is >= 9 and <= 11)
            {
                score += 1;
            }
            else if (respiration is >= 21 and <= 24)
            {
                score += 2;
            }
            else if (respiration >= 25)
            {
                score += 3;
            }

            return score;
        }

        private static int SpO2Score(int spO2, AirOrOxygenEnum airOrOxygen)
        {
            int score = 0;

            if (spO2 <= 83)
            {
                score += 3;
            }
            else if (spO2 is >= 84 and <= 85)
            {
                score += 2;
            }
            else if (spO2 is >= 86 and <= 87)
            {
                score += 1;
            }
            else if (spO2 is >= 93 and <= 94 && airOrOxygen == AirOrOxygenEnum.Oxygen)
            {
                score += 1;
            }
            else if (spO2 is >= 95 and <= 96 && airOrOxygen == AirOrOxygenEnum.Oxygen)
            {
                score += 2;
            }
            else if (spO2 >= 97 && airOrOxygen == AirOrOxygenEnum.Oxygen)
            {
                score += 3;
            }

            return score;
        }

        private static int TemperatureScore(double temperature)
        {
            int score = 0;

            if (temperature <= 35)
            {
                score += 3;
            }
            else if (temperature is >= 35.1 and <= 36.0)
            {
                score += 1;
            }
            else if (temperature is >= 38.1 and <= 39.0)
            {
                score += 1;
            }
            else if (temperature >= 39.1)
            {
                score += 2;
            }

            return score;
        }

        private static int CapillaryBloodGlucoseScore(CapillaryBloodGlucoseEnum capillaryBloodGlucoseType, float capillaryBloodGlucose)
        {
            int score = 0;

            if (capillaryBloodGlucoseType == CapillaryBloodGlucoseEnum.Fasting)
            {
                if (capillaryBloodGlucose <= 3.4)
                {
                    score += 3;
                }
                else if (capillaryBloodGlucose is >= 3.5f and <= 3.9f)
                {
                    score += 2;
                }
                else if (capillaryBloodGlucose is >= 5.5f and <= 5.9f)
                {
                    score += 2;
                }
                else if (capillaryBloodGlucose >= 6.0f)
                {
                    score += 3;
                }
            }

            else if (capillaryBloodGlucoseType == CapillaryBloodGlucoseEnum.AfterEating)
            {
                if (capillaryBloodGlucose <= 4.5f)
                {
                    score += 3;
                }
                else if (capillaryBloodGlucose is >= 4.5f and <= 5.8f)
                {
                    score += 2;
                }
                else if (capillaryBloodGlucose is >= 7.9f and <= 8.9f)
                {
                    score += 2;
                }
                else if (capillaryBloodGlucose >= 9.0)
                {
                    score += 3;
                }

            }

            return score;
        }
    }
}
