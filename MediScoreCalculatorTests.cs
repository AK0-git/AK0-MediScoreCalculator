using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MediScoreCalculation
{
    [TestFixture]
    public class MediScoreCalculatorTests
    {
        [Test]
        public void CalculateScore_ForPatient1()
        {
            //Arrange
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

            MediScoreCalculator calculator = new MediScoreCalculator();

            //Act
            int score = calculator.CalculateScore(observations);

            //Assert
            Assert.That(score, Is.EqualTo(0));
        }

        [Test]
        public void CalculateScore_ForPatient2()
        {
            //Arrange
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Oxygen,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 17,
                SpO2 = 95,
                Temperature = 37.1f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            MediScoreCalculator calculator = new MediScoreCalculator();


            //Act
            int score = calculator.CalculateScore(observations);

            //Assert
            Assert.That(score, Is.EqualTo(4));
        }

        [Test]
        public void CalculateScore_ForPatient3()
        {
            //Arrange
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Oxygen,
                Consciousness = ConsciousnessEnum.CVPU,
                Respiration = 23,
                SpO2 = 88,
                Temperature = 38.5f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.Fasting,
                CapillaryBloodGlucose = 4.2f
            };

            MediScoreCalculator calculator = new MediScoreCalculator();


            //Act
            int score = calculator.CalculateScore(observations);

            //Assert
            Assert.That(score, Is.EqualTo(8));
        }

        [Test]
        public void CalculateScore_ForPatient4()
        {
            //Arrange
            Observations observations = new Observations
            {
                AirOrOxygen = AirOrOxygenEnum.Oxygen,
                Consciousness = ConsciousnessEnum.Alert,
                Respiration = 23,
                SpO2 = 88,
                Temperature = 38.5f,
                CapillaryBloodGlucoseType = CapillaryBloodGlucoseEnum.AfterEating,
                CapillaryBloodGlucose = 8.4f
            };

            MediScoreCalculator calculator = new MediScoreCalculator();


            //Act
            int score = calculator.CalculateScore(observations);

            //Assert
            Assert.That(score, Is.EqualTo(7));
        }
    }

}
