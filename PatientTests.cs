using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MediScoreCalculation
{
    [TestFixture]
    public class PatientTests
    {
        [Test]
        public void DoesNotFlagARiskForASingleObservation()
        {
            //Arrange
            Patient patient = new Patient();

            DateTime date = new DateTime(2025, 03, 02, 0, 0, 0);

            //Act
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Air,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 15,
                SpO2 = 95,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            bool riskFlagged = patient.AddObservations(date, observations);

            //Assert
            Assert.That(riskFlagged, Is.EqualTo(false));
        }

        [Test]
        public void DoesFlagARiskIfMediScoreDoesGoUpBy2Within24HourPeriod()
        {
            //Arrange
            Patient patient = new Patient();

            DateTime date = new DateTime(2025, 03, 01, 2, 0, 1);
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Air,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 15,
                SpO2 = 95,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            DateTime date2 = date.AddDays(1);
            Observations observations2 = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Oxygen,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 15,
                SpO2 = 92,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            //Act
            bool riskFlagged = patient.AddObservations(date, observations);
            bool riskFlagged2 = patient.AddObservations(date2, observations2);

            //Assert
            Assert.That(riskFlagged, Is.EqualTo(false));
            Assert.That(riskFlagged2, Is.EqualTo(true));
        }

        [Test]
        public void DoesNotFlagARiskIfMediScoreGoesUpBy2Outside24HourPeriod()
        {
            //Arrange
            Patient patient = new Patient();

            DateTime date = new DateTime(2025, 03, 02, 0, 0, 0);
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Air,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 15,
                SpO2 = 95,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            //add a day and 1 second.. so now no longer within 24 hours
            DateTime date2 = date.AddDays(1).AddSeconds(1);
            Observations observations2 = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Oxygen,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 15,
                SpO2 = 92,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            //Act
            bool riskFlagged = patient.AddObservations(date, observations);
            bool riskFlagged2 = patient.AddObservations(date2, observations2);

            //Assert
            Assert.That(riskFlagged, Is.EqualTo(false));
            Assert.That(riskFlagged2, Is.EqualTo(false));
        }

        [Test]
        public void DoesFlagARiskIfMediScoreDoesGoUpBy2Within24HourPeriod_HandlesMultipleObservations()
        {
            //Arrange
            Patient patient = new Patient();
            DateTime date = new DateTime(2025, 01, 10, 1, 0, 0);

            float capillaryBloodGlucose = 4.0f;
            List<bool> risksFlagged = [];
            for (int i = 0; i < 20; i++)
            {
                //blood glucose will increase by 0.1 mmol/L every hour. After 15 hours they will trigger a risk flag all else being equal
                capillaryBloodGlucose += 0.1f;
                Observations observations = new Observations
                {
                    AirOrOxygen = AirOrOxygenEnum.Air,
                    Consciousness = ConsciousnessEnum.Alert,
                    Respiration = 15,
                    SpO2 = 95,
                    Temperature = 37.1f,
                    CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                    CapillaryBloodGlucose = capillaryBloodGlucose
                };

                date = date.AddHours(1);

                //Act
                bool riskFlagged = patient.AddObservations(date, observations);
                risksFlagged.Add(riskFlagged);
            }

            //Assert
            Assert.That(risksFlagged.Any(x => x), Is.EqualTo(true));
        }
    }
}
